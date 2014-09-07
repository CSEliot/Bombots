Shader "Custom/BlobShader" {
Properties {
	_Color ("Color", Color) = (0,1,0,1)
	_Reflection ("Reflection", Float) = 0.3
	_Transparency ("Transparency", Float) = 0.2
	_TranspFalloff ("Transparency Falloff", Float) = 5
	_ViewLightCol ("Camera Light Color", Color) = (1,1,1,1)
	_ViewLight ("Camera Light Position", Vector) = (.5,.5,1)
	_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	_Cube ("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }
}

Category {
	Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
    Blend SrcAlpha OneMinusSrcAlpha
	LOD 250
	
	// ------------------------------------------------------------------
	// Shaders

	SubShader {
		// Always drawn reflective pass
		Pass {
			Name "BASE"
			Tags {"LightMode" = "ForwardBase"}
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float3  color   : COLOR;
	float2	uv		: TEXCOORD0;
	float3	normal	: TEXCOORD1;
	float3	view	: TEXCOORD2;
	float3	TtoW0 	: TEXCOORD3;
	float3	TtoW1	: TEXCOORD4;
	float3	TtoW2	: TEXCOORD5;
};

uniform float4 _MainTex_ST, _BumpMap_ST;
uniform float4 _ViewLight, _ViewLightCol;

v2f vert(appdata_tan v)
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
	
	o.view = WorldSpaceViewDir( v.vertex );
	
	// adapted from Unity ShadeVertexLights
	float3 viewpos = o.pos.xyz;
	float3 viewN = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	o.color = UNITY_LIGHTMODEL_AMBIENT.xyz;
	o.color += _ViewLightCol.rgb * max(0, dot(normalize(viewN), normalize(_ViewLight)));
	
	TANGENT_SPACE_ROTATION;
	o.TtoW0 = mul(rotation, _Object2World[0].xyz * unity_Scale.w);
	o.TtoW1 = mul(rotation, _Object2World[1].xyz * unity_Scale.w);
	o.TtoW2 = mul(rotation, _Object2World[2].xyz * unity_Scale.w);
	
	return o; 
}

uniform sampler2D _MainTex;
uniform samplerCUBE _Cube;
uniform float4 _Color;
uniform float _Reflection;
uniform float _Transparency;
uniform float _TranspFalloff;

fixed4 frag (v2f i) : SV_Target
{
	fixed4 texcol = tex2D(_MainTex,i.uv);
	
	// adjust color
	if (texcol.g > texcol.r && texcol.g > texcol.b)
		texcol.rgb = _Color.rgb * texcol.g;

	// lighting vectors
	half3 wv = normalize(i.view);
	half3 normal = half3(0.f,0.f,1.f);	// allow bump mapping later?
	half3 wn = half3(
		dot(i.TtoW0, normal),
		dot(i.TtoW1, normal),
		dot(i.TtoW2, normal));
	
	// add diffuse to base layer
	fixed4 col = texcol;
	col.rgb *= i.color;

	// pseudo-Fresnel for transparency to see bomb
	half r0 = half(_Transparency);
	half fresnel = lerp(r0,1, pow(saturate(dot(wv, wn)), _TranspFalloff));
	col.a = saturate(1-fresnel);
	
	// calculate reflection
	r0 = _Reflection;
	fresnel = lerp(r0, 1, pow(1 - saturate(dot(wv, wn)), 5));
	half3 refl = reflect(-i.view, wn);
	
	// blend surface color with reflection
	fixed4 reflColor = texCUBE(_Cube, refl);
	col = lerp(col, reflColor, saturate(fresnel));
	
	// fade out with base alpha
	col.a *= _Color.a;
	
	return col;
}
ENDCG  
		} 
	}
	
}
	
FallBack "VertexLit", 1

}
