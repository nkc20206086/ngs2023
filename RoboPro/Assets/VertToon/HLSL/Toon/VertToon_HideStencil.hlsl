#ifndef VT_HIDESTENCIL
#define VT_HIDESTENCIL

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes
{
    float4 positionOS : POSITION;
    
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv : TEXCOORD0;
    float4 positionHCS : SV_POSITION;
    
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

// CBUFFER
#include "../VertToon/HLSL/Toon/VertToon_Input.hlsl"

Varyings vert(Attributes v)
{
    Varyings o = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);

    o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);

    return o;
}

float4 frag(Varyings i) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(i);

    return float4(_HideColor, 1);
}
#endif