Shader "Tobi/PortalShader"
{
    Properties
    {
        _PortalShapeMask ("Texture", 2D) = "white" {}
        _PortalDestinationTexture ("Texture", 2D) = "white" {}
        _PortalInnerMask ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            BlendOp RevSub
            Blend One Zero, Zero Zero
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO //Insert
            };

            float4 _PortalShapeMask_ST;
            float4 _PortalDestinationTexture_ST;
            float4 _PortalInnerMask_ST;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); //Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _PortalDestinationTexture);
                return o;
            }

            UNITY_DECLARE_SCREENSPACE_TEXTURE(_PortalDestinationTexture); //Insert
            UNITY_DECLARE_SCREENSPACE_TEXTURE(_PortalShapeMask); //Insert
            UNITY_DECLARE_SCREENSPACE_TEXTURE(_PortalInnerMask); //Insert

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i); //Insert

                
                
                return float4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
