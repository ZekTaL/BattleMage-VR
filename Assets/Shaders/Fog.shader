Shader "MyShaders/Fog"
{
    Properties
    {
        _OuterTex ("Outer Texture", 2D) = "white" {}
        _ScrollSpeedOuter ("Outer Scroll Speed", vector) = (10, 0, 0, 0)
        _ColorOuter ("Outer Color", Color) = (1, 0, 0, 1)
        _ColorInner ("Inner Color", Color) = (1, 1, 0, 1)
    }
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent" // inform the render pipeline of what type his is
            "Queue" = "Transparent" // changes the render order
        }

        LOD 100

        Pass
        {
            Cull Off // can choose also Back or Front
            ZWrite Off
            //ZTest Less
            Blend One One // Additive colors
            //Blend DstColor Zero multiply colors

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normals : TEXCOORD2;
            };

            sampler2D _OuterTex;
            float4 _OuterTex_ST;
            float4 _ScrollSpeedOuter;
            float4 _ColorOuter;

            v2f vert (appdata v)
            {
                //v.vertex.y += sin(v.vertex.x + _Time.y) * 0.3;

                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _OuterTex);
                o.normals = UnityObjectToWorldNormal(v.normals);

                // Shift the uvs over time.
                o.uv += _ScrollSpeedOuter * _Time.y * 0.01;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_OuterTex, i.uv);

                return (col * _ColorOuter);
            }
            ENDCG
        }
    }
}
