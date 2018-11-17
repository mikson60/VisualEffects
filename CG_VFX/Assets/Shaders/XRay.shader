Shader "Custom/XRay" {
	Properties {
		_RimColor ("Rim Color", Color) = (1,1,1,1)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.5
		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }

		Pass {
			ZTest Greater
			ZWrite On
			ColorMask 0
		}

		Pass {
			ZTest Less
			Cull Off
			SetTexture[_MainTex] {combine texture}
		}


		CGPROGRAM

		#pragma surface surf Lambert alpha:fade

		struct Input {
			float3 viewDir;
		};

		float4 _RimColor;
		float _RimPower;

		void surf (Input IN, inout SurfaceOutput o) {
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower) * 10;
			o.Alpha = pow(rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
