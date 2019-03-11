Shader "MyShaders/TextureSliding"
{
    Properties
    {
        _MainTex    ("Texture", 2D)     = "white" {}
        _Color      ("Color", Color)    = (1, 1, 1, 1)
        _Glossiness ("Smoothness", Range(0,1))  = 1
        _Metallic   ("Metallic", Range(0, 1))   = 0
        _RateX      ("Translate Rate X", float) = 0
        _RateY      ("Translate Rate Y", float) = 0
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

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        half _Metallic;
        half _Glossiness;
        float _RateX;
        float _RateY;

        float2 offsetUV(float2 uv, float x, float y) {

            return uv.xy + fixed2(x,y);
        }


        void surf(Input IN, inout SurfaceOutputStandard o) {

            float2 uv   = offsetUV(IN.uv_MainTex, _Time.y * _RateX, _Time.y * _RateY); // float2, float
            fixed4 c    = tex2D(_MainTex, uv) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
        
    }
}
