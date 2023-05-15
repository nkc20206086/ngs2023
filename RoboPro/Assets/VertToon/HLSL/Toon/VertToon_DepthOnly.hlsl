#ifndef VT_DEPTHONLY
#define VT_DEPTHONLY

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes
{
    float4 positionOS : POSITION;
    float2 uv : TEXCOORD0;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv : TEXCOORD0;
    float4 positionCS : SV_POSITION;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

// CBUFFER
#include "../VertToon/HLSL/Toon/VertToon_Input.hlsl"

Varyings depthOnlyVertex(Attributes v)
{
    Varyings o = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(v);

    o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
    return o;
}

float4 depthOnlyFragment(Varyings i) : SV_Target
{
    return 0;
}

#endif