// Edited From https://gist.github.com/TarasOsiris/e0e6e6c3b8fdb0d8074b
Shader "MyShaders/Flow Map Simple" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _FlowMap ("Flow Map", 2D) = "white" {}
        _FlowSpeed ("Flow Speed", float) = -0.5
        _Metallic ("Metallic", float) = 0
        _Smoothness ("Smoothness", float) = 0
        _ContrastMax ("Emissive Contrast Max", float) = 1
        _ContrastMin ("Emissive Contrast Min", float) = 1
        _ContrastRate ("Contrast Change Rate", float) = 1
        _PeriodLength ("PeriodLength", float) = 1

        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        CGPROGRAM
        #pragma surface surf Standard
        #pragma multi_compile _ PIXELSNAP_ON
        #include "UnityCG.cginc"
        
        struct appdata_t
        {
            float4 vertex   : POSITION;
            float4 color    : COLOR;
            float2 texcoord : TEXCOORD0;
        };

        struct Input {
            float2 uv_MainTex;
        };

        struct v2f
        {
            float4 vertex   : SV_POSITION;
            fixed4 color    : COLOR;
            half2 texcoord  : TEXCOORD0;
        };
        
        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _FlowMap;
        float _FlowSpeed;
        float _ContrastMax;
        float _ContrastMin;
        float _ContrastRate;
        float _Metallic;
        float _Smoothness;
        float _PeriodLength;
        float _FlowStrength;


        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 flowDir = tex2D(_FlowMap, IN.uv_MainTex) * 2.0f - 1.0f;
            flowDir *= _FlowSpeed;

            float phase0 = frac((_Time.y * _PeriodLength + 0.5f));
            float phase1 = frac((_Time.y * _PeriodLength + 1.0f));

            half3 tex0 = tex2D(_MainTex, IN.uv_MainTex + flowDir.xy * phase0);
            half3 tex1 = tex2D(_MainTex, IN.uv_MainTex + flowDir.xy * phase1);

            float flowLerp = abs((0.5f - phase0) / 0.5f);
            half3 finalColor = lerp(tex0, tex1, flowLerp);


            // change contrast over time
            float deltaContrast = _ContrastMax - _ContrastMin;
            float contrastLerp = _ContrastMin + deltaContrast * abs(1 - 2 * frac(_Time * _ContrastRate) );
            fixed4 c = float4(finalColor, 1.0f) * _Color * contrastLerp;
            c.rgb *= c.a;

            // set the output
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;

        }

        ENDCG
    }
    FallBack "Diffuse"
}