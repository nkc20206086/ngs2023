Shader "VertToon/Opaque"
{
    Properties
    {
        // Main
        _MainTex("Main Texture", 2D) = "white"{}
        _Color("Color", Color) = (1, 1, 1, 1)
        [Toggle] _UseVertColor ("Use vertex Color", int) = 1

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
        LOD 100

        Pass
        {
            Name "Universal Forward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            Cull Off
            ZWrite ON

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma target 4.5
            #pragma enable_d3d11_debug_symbols

            #include "../VertToon/HLSL/Toon/VertToon_Core.hlsl"
            ENDHLSL
        }

        // いったんこれで影を落とせれる
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            Cull Back
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma target 4.5
            #pragma enable_d3d11_debug_symbols

            #include "../VertToon/HLSL/Toon/VertToon_ShadowCaster.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }

            Cull Off
            ZWrite On
            ColorMask 0

            HLSLPROGRAM
            #pragma target 4.5

            #pragma vertex depthOnlyVertex
            #pragma fragment depthOnlyFragment

            #pragma multi_compile_instancing

            #include "../VertToon/HLSL/Toon/VertToon_DepthOnly.hlsl"
            ENDHLSL
        }
    }
}
