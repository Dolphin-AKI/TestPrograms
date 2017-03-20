Shader "AQ_Sonor/ObjectSonor" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Glow("Glow", Float) = 0
		_RimColor("Rim Color",Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Rim amount",Range(0.2,10.0)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		Cull off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		

		struct Input {
			float2 uv_BumpMap;
			float4 color : COLOR;
			float3 viewDir;
			float3 worldNormal;
		};
	
		sampler2D _BumpMap;
		float _Glow;		
		float3 _Color;
		float4 _RimColor;
		float _RimPower;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			o.Albedo = _Color.rgb;
			//o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			half rim = 1 - saturate(abs(dot(normalize(IN.viewDir), IN.worldNormal)));
			//o.Emission = _RimColor.rgb * pow(rim, _RimPower);
			o.Alpha = rim;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
