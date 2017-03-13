Shader "AQ_Sonor/ObjectSonor" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Glow("Glow", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		

		struct Input {
			float4 color : COLOR;
		};

	
		float _Glow;
		
		float3 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			o.Albedo = _Color.rgb;
			// Metallic and smoothness come from slider variables
			
			o.Alpha = _Glow;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
