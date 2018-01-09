Shader "Self_Ill_RGB_AlphaCon" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_EmissionLM ("Emission", Range(0.1,1)) = 1
	_AlphaNum("Alpha",Range(0.1,1)) = 0.1
}
SubShader {
		Tags {"RenderType"="Transparent" }
		LOD 200
		Cull Off
		Alphatest Greater [_AlphaNum]
	CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _MainTex;
	fixed4 _Color;
	float _EmissionLM;
	struct Input {
		float2 uv_MainTex;
		float2 uv_Illum;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 c = tex * _Color;
		o.Albedo = c.rgb;
		o.Emission = c.rgb * _EmissionLM;
		o.Alpha = c.a;
	}
	ENDCG
} 
	FallBack "Self-Illumin/VertexLit"
}



