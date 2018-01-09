Shader "Custom/RimLight" {
	Properties {
      _MainTex ("Texture", 2D) = "white" {}					//宣告參數
      _RimColor ("Rim Color", Color) = (1,0,0,0.0)			//宣告發光顏色
      _RimPower ("Rim Power", Range(0.5,8.0)) = 7.0			//宣告發光範圍
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }						//不透明
      Cull Off
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {										//定義參數
          float2 uv_MainTex;
          float3 viewDir;
      };
      sampler2D _MainTex;
      float4 _RimColor;
      float _RimPower;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;		
          //依照uv將_MainTex的顏色取出          
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));  
          //攝影機角度做正規化再跟normal做分量 最後再做saturate讓數值介在0~1之間
          o.Emission = _RimColor.rgb * pow (rim, _RimPower);	
          //讓rim乘上RimPower次方再乘以RimColor做為發光
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }
