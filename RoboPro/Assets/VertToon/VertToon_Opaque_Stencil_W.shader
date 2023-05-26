Shader "VertToon/Opaque_Stencil_W"
{
    Properties
    {
        // Main
        _MainTex("Main Texture", 2D) = "white"{}
        _Color("Color", Color) = (1, 1, 1, 1)
        [Toggle] _UseVertColor("Use vertex Color", int) = 1

        // OtherSetting
        [Enum(Off,0 ,Front,1, Back,2)] _CullingMode("Culling", int) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 4
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

        // ForwardPass
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            Cull [_CullingMode]
            ZWrite ON
            ZTest [_ZTest]

            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma target 4.5
            #pragma enable_d3d11_debug_symbols

            #include "../VertToon/HLSL/Toon/VertToon_Core.hlsl"
            ENDHLSL
        }

        // ShadowPass
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

        // DepthPass
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }

            Cull [_CullingMode]
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
    CustomEditor "AyahaShader.VertToon.VertToonGUI"
}
