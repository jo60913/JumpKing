Shader "CutoutTest" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
		_Emissioness ("Emissioness", Range(0.5,8.0)) = 0.5
	}

	SubShader {
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		LOD 200
		Lighting on
	CGPROGRAM
	#pragma surface surf Lambert alphatest:_Cutoff

	sampler2D _MainTex;
	fixed4 _Color;
	float _Emissioness;
	struct Input {
		float2 uv_MainTex;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Emission = c.rgb * pow (0.5, _Emissioness);
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}

	Fallback "Transparent/Cutout/VertexLit"
}
