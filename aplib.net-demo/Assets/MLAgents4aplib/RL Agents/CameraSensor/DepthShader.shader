Shader "Custom/DepthTextureVisualize"
{
    Properties {}
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            //A simple depth shader that:
            //1. for each pixel on the screen, checks the distance to the object to be rendered at that pixel
            //2. Normalizes those distance values
            //3. Renders the values in grayscale; closest being white and furthest being black.
            
            //In order to control the range of the received values (those which will be normalized), we use the orthographic projection mode on the camera.
            //In that orthographic projection mode, we set the distance of the near and far camera planes so that all objects of interest are in view.
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            struct appdata
            {
                float4 positionOS : POSITION;
            };

            struct v2f
            {
                float4 positionHCS : SV_POSITION;
            };

            v2f vert(appdata IN)
            {
                v2f OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            float4 frag(v2f IN) : SV_Target
            {
                float2 UV = IN.positionHCS.xy / _ScaledScreenParams.xy;
                float depth = SampleSceneDepth(UV);

                // Normalize depth value to [0, 1] range
                depth = saturate(depth);

                // Debugging: Visualize depth as a gradient
                float3 color = float3(depth, depth, depth);

                return float4(color, 1.0);
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}


