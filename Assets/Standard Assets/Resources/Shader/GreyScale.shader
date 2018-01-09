Shader "GreyScale" {
Properties
{
	_MainTex ("Diffuse Textures", 2D) = "white" {}
	_EmissionLM ("Emission", Range(0.1,1)) = 1
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200
	CGPROGRAM
	#pragma surface surf Lambert
	sampler2D _MainTex;
	float _EmissionLM;
	struct Input
	{
		float2 uv_MainTex;
		float2 uv_GreyMask;
	};
	void surf (Input IN, inout SurfaceOutput o)
	{
		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = (c.r + c.b + c.g) / 3;
		o.Alpha = c.a;
		o.Emission = (c.r + c.b + c.g) / 3 * _EmissionLM;
	}
	ENDCG
}
FallBack "Diffuse"
}