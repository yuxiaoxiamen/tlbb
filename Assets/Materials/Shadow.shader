// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Shadow" {
	Properties{
	  _MainTex("Main Tex", 2D) = "white" {}
	   _AlphaTex("AlphaTex",2D) = "white"{}
	  _Offset("Offset", vector) = (0, 0, 0, 0)
	}
		CGINCLUDE
#include "UnityCG.cginc"
		  sampler2D _MainTex;
	  float4 _Offset;
	  sampler2D _AlphaTex;
	  float4 _MainTex_ST;

	  struct v2f {
		  float4 pos : POSITION;
		  float2 uv : TEXCOORD0;
	  };

	  v2f vert_normal(appdata_base v) {
		  v2f o;
		  o.pos = UnityObjectToClipPos(v.vertex);
		  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		  return o;
	  }

	  v2f vert_offset(appdata_base v) {
		  v2f o;
		  float4 pos = mul(unity_ObjectToWorld, v.vertex);
		  o.pos = mul(UNITY_MATRIX_VP, pos + _Offset);
		  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		  return o;
	  }

	  float4 frag_normal(v2f i) : COLOR{
		float4 texcol = tex2D(_MainTex, i.uv);
		texcol.w = tex2D(_AlphaTex,i.uv)*texcol.w;
		return texcol;
	  }

		  float4 frag_color(v2f i) : COLOR{
			float4 c;
			c = tex2D(_MainTex, i.uv);
			c.w = tex2D(_AlphaTex,i.uv)*c.w;
			if (c.w >= 0.5)
			{
			   c.r = 0;
			   c.g = 0;
			   c.b = 0;
			   c.w = 0.5f;
			}

			return c;
	  }
		  ENDCG
		  SubShader {
		  Tags{ "Queue" = "Transparent" }
			  Pass{
				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
				#pragma vertex vert_offset
				#pragma fragment frag_color
				ENDCG
		  }
			  Pass{
			  ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
				#pragma vertex vert_normal
				#pragma fragment frag_normal
				ENDCG
		  }
	  }
	  FallBack "Diffuse"
}