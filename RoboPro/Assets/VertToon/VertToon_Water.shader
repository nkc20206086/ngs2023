Shader "VertToon/Water"
{
    Properties
    {
        _WaterColor("Water Color", Color) = (0, 0, 1, 1)
        _FoamColor("Foam Color", Color) = (1, 1, 1 ,1)
        _Height("Height", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "IgnoreProjector" = "True"
            "Queue" = "Geometry"
        }
        Stencil
        {
            Ref 2
            Comp NotEqual
        }
        LOD 100

        // ForwardPass
        Pass
        {
            Name "Universal Forward"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5
            #pragma enable_d3d11_debug_symbols

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float3 positionVS : TEXCOORD1;
            };

            CBUFFER_START(UnityPerMaterial)
                uniform float3 _WaterColor;
                uniform float3 _FoamColor;
                uniform float _Height;
            CBUFFER_END

            Varyings vert (Attributes v)
            {
                Varyings o = (Varyings)0;

                // positions
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);
                o.positionHCS = vertexInput.positionCS;
                o.positionVS = vertexInput.positionVS;
                o.screenPos = vertexInput.positionNDC;

                return o;
            }

            float4 frag (Varyings i) : SV_Target
            {
                float4 lastCol = (float4)1;

                // Depth
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                float depth = _CameraDepthTexture.Sample(sampler_CameraDepthTexture, screenUV).r;
                depth = LinearEyeDepth(depth, _ZBufferParams);
                float screenDepth = depth - i.screenPos.w;
                float fragmentEyeDepth = -i.positionVS.z;
                float rawDepth = SampleSceneDepth(screenUV);
                float orthoLinearDepth = _ProjectionParams.x > 0.0 ? rawDepth : 1.0 - rawDepth;
                float sceneEyeDepth = lerp(_ProjectionParams.y, _ProjectionParams.z, orthoLinearDepth);

                // Foam
                float depthDifference = 1.0 - saturate((sceneEyeDepth - fragmentEyeDepth));
                float foam = 1.0 - step(depthDifference, 1.0 - _Height);

                // WaterColor
                float3 foamColor = (float3)foam * _FoamColor;

                // LastColor
                lastCol = float4(lerp(_WaterColor, foamColor, foam), 1);
                return lastCol;
            }
            ENDHLSL
        }
    }
    CustomEditor "AyahaShader.VertToon.VertToonWaterGUI"
}
