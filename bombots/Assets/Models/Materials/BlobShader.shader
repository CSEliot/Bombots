Shader "Custom/BlobShader" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,0.1)
	_MainTex ("Base (RGB), RefStrength (A)", 2D) = "white" {}
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
			Tags {"LightMode" = "Always"}
CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float2	uv		: TEXCOORD0;
	float2	uv2		: TEXCOORD1;
	float3	I		: TEXCOORD2;
	float3	TtoW0 	: TEXCOORD3;
	float3	TtoW1	: TEXCOORD4;
	float3	TtoW2	: TEXCOORD5;
};

uniform float4 _MainTex_ST, _BumpMap_ST;

v2f vert(appdata_tan v)
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
	o.uv2 = TRANSFORM_TEX(v.texcoord,_BumpMap);
	
	o.I = -WorldSpaceViewDir( v.vertex );
	
	TANGENT_SPACE_ROTATION;
	o.TtoW0 = mul(rotation, _Object2World[0].xyz * unity_Scale.w);
	o.TtoW1 = mul(rotation, _Object2World[1].xyz * unity_Scale.w);
	o.TtoW2 = mul(rotation, _Object2World[2].xyz * unity_Scale.w);
	
	return o; 
}

uniform sampler2D _BumpMap;
uniform sampler2D _MainTex;
uniform samplerCUBE _Cube;
uniform fixed4 _ReflectColor;
uniform fixed4 _Color;

fixed4 frag (v2f i) : SV_Target
{
	fixed4 texcol = tex2D(_MainTex,i.uv);
	texcol = fixed4(0,1,0,.2);
	
	// transform normal to world space (code to allow normal map later)
	half3 normal = half3(0.f,0.f,1.f);
	half3 wn;
	wn.x = dot(i.TtoW0, normal);
	wn.y = dot(i.TtoW1, normal);
	wn.z = dot(i.TtoW2, normal);
	
	// add diffuse to base layer
	fixed4 col = texcol;
	col.rgb *= saturate(dot(wn, normalize(_WorldSpaceLightPos0)));	
	
	// calculate reflection vectors in world space
	half r0 = 0.2;
	half fresnel = r0 + (1-r0)*pow(saturate(dot(i.I, wn)), 5);
	half3 refl = reflect(i.I, wn);
	
	// blend surface color with reflection
	fixed4 reflColor = texCUBE(_Cube, refl);
	return lerp(col, reflColor, saturate(fresnel));
}
ENDCG  
		} 
	}
	
}
	
FallBack "VertexLit", 1

}
