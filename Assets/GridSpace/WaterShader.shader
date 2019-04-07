Shader "Terrain/WaterShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

		_NoiseTex("Extra Wave Noise", 2D) = "white" {}
        _Speed("Wave Speed", Range(0,1)) = 0.5
        _Amount("Wave Amount", Range(0,1)) = 0.5
        _Height("Wave Height", Range(0,1)) = 0.5

		_Fresnel ("Fresnel", Range (0.01, 3)) = 1
		_FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Transparent"}
		LOD 200
		
		CGPROGRAM

		#pragma surface surf Standard vertex:vert

		#pragma target 3.0

		struct Input {
            float2 uv_MainTex;
            float4 screenPos;
            float3 worldPos;
			float3 viewDir;
        };


		sampler2D _MainTex,  _NoiseTex, _FoamNoise;
		uniform sampler2D _CameraDepthTexture;

		fixed4 _Color, _FoamColor, _FresnelColor;
        float _Speed, _Amount, _Height, _Foam,  _Fresnel;

		void vert (inout appdata_full v)
		{
			float4 wVertex = mul( unity_ObjectToWorld, v.vertex );
			// wVertex.y += _Height * (sin(_Time.z * _Speed + ((wVertex.x + wVertex.z) * _Amount)) + 
			// 	0.7 * cos(2 * _Time.z * _Speed + ((wVertex.x - 2 * wVertex.z + 0.2 * sin(_Time.z)) * _Amount)));
			wVertex.y += _Height * tex2Dlod(_NoiseTex, float4(wVertex.x / 3 + _Time.z * _Speed, wVertex.z / 3, 0, 0)) - _Height / 2;
			v.vertex = mul( unity_WorldToObject, wVertex );
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos)));
            // float surfZ = -mul(UNITY_MATRIX_V, float4(IN.worldPos.xyz, 1)).z;
            // float diff = sceneZ - surfZ;
            // float intersect = 1 - saturate(diff / _Foam * tex2D(_FoamNoise, IN.uv_MainTex * 0.25 + (_Time.z * 0.005)));
            // fixed4 col = fixed4(lerp(tex2D(_MainTex, IN.uv_MainTex) * _Color, _FoamColor, pow(intersect, 4)));
			half factor = dot(normalize(IN.viewDir),o.Normal);
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = col.rgb + _FresnelColor * (_Fresnel - factor * _Fresnel);
            o.Alpha = col.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
