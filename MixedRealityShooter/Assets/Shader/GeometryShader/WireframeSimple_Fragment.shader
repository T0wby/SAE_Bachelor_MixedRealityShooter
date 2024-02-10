Shader "Unlit/WireframeSimple_Fragment"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WireframeColor ("WireframeColor", Color) = (1,0,0,1)
        _WireframeWidth ("WireframeWidth", float) = 0.05
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent" "Queue" = "Transparent"
        }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

                UNITY_VERTEX_OUTPUT_STEREO_EYE_INDEX
            };

            float4 _MainTex_ST;
            float4 _WireframeColor;
            float _WireframeWidth;

            v2f vert(appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v); //Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex); //Insert

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                float2 uv = i.uv;
                uv.y = 1.0 - uv.y; // Flip y-coordinate to match Unity's texture coordinates
                fixed4 col = tex2D(_MainTex, uv);
                const float closest = min(col.r, min(col.g, col.b));
                float alpha = step(closest, _WireframeWidth);
                return fixed4(_WireframeColor.rgb, alpha);
            }
            ENDCG
        }
    }
}