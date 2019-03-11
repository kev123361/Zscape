Shader "MyShaders/Flow"
{
    Properties
    {
        _MainTex    ("Texture", 2D)     = "white" {}
        _Color      ("Color", Color)    = (1, 1, 1, 1)
        _Glossiness ("Smoothness", Range(0,1))  = 1
        _Metallic   ("Metallic", Range(0, 1))   = 0
        _RateX      ("Translate Rate X", float) = 0
        _RateY      ("Translate Rate Y", float) = 0
        _TileX      ("Tile X", float) = 1
        _TileY      ("Tile Y", float) = 1
		[NoScaleOffset] _FlowMap ("Flow (RG, A noise)", 2D) = "black" {}
		_UJump      ("U jump per phase", Range(-0.25, 0.25)) = 0.25
		_VJump      ("V jump per phase", Range(-0.25, 0.25)) = 0.25
        _FlowRate   ("Flow Rate", float) = 1
        _FlowInfluence   ("Flow Influence", float) = 1
        _BlendFlow  ("Blend Flow Toggle", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard

        #include "UnityCG.cginc"

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };
        sampler2D _MainTex;
        sampler2D _FlowMap;

        struct Input {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        half _Metallic;
        half _Glossiness;
        float _RateX;
        float _RateY;
        float _TileX;
        float _TileY;
        float _UJump;
        float _VJump;
        float _FlowRate;
        float _FlowInfluence;
        float _BlendFlow;

        float2 flowUV(float2 uv, float2 flowDir, float t) {

            float progress = frac(t);
            return uv - flowDir * progress;
        }

        float3 flowUVW(float2 uv, float2 flowDir, float jump, float tileX, float tileY, float t, bool flowB) {

            float phaseOffset = flowB ? .5 : 0;

            float progress = frac(t + phaseOffset);
            float3 uvw;
            uvw.xy = (uv - flowDir * progress);
            uvw.xy *= float2(tileX, tileY);
            uvw.xy += phaseOffset;// + (t - progress) * jump;
            uvw.z = 1 - abs(1 - 2 * progress); // fades the flow to help with transition
            return uvw;
        }

        void surf(Input IN, inout SurfaceOutputStandard o) {

            float2 flow = tex2D(_FlowMap, IN.uv_MainTex).rg * _FlowInfluence * 2 - 1;
			float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
			float time = (_Time.y + noise) * _FlowRate;

            fixed4 c;

            if (_BlendFlow != 0) {
                // blend two phases of the flow to make repeat less obvious

                float phaseJump = float2(_UJump, _VJump);
                
                float3 uvwA   = flowUVW(IN.uv_MainTex, flow, phaseJump, _TileX, _TileY, time, false);
                float3 uvwB   = flowUVW(IN.uv_MainTex, flow, phaseJump, _TileX, _TileY, time, true);

                fixed4 texA = tex2D(_MainTex, uvwA.xy) * uvwA.z;
                fixed4 texB = tex2D(_MainTex, uvwB.xy) * uvwB.z;

                c = (texA + texB) * _Color;
            } else {
                float3 uvw   = flowUVW(IN.uv_MainTex, flow, 0, _TileX, _TileY, time, false);
                c = tex2D(_MainTex, uvw.xy) * uvw.z * _Color;
            }
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
        
    }
}
