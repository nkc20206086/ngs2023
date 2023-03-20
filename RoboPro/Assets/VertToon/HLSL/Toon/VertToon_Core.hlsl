#ifndef VT_CORE
#define VT_CORE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

struct Attributes
{
    float4 positionOS : POSITION;
    float2 uv : TEXCOORD0;
    float3 color : COLOR;
    float3 normalOS : NORMAL;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv : TEXCOORD0;
    float4 positionHCS : SV_POSITION;
    float3 vertColor : COLOR;
    float3 normalWS : NORML;
    float4 lightDirWS : TEXCOORD1; // w : 未使用

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

uniform sampler2D _MainTex;

// CBUFFER
#include "../VertToon/HLSL/Toon/VertToon_Input.hlsl"

#include "../VertToon/HLSL/Toon/VertToon_Vert.hlsl"
#include "../VertToon/HLSL/Toon/VertToon_Frag.hlsl"

#endif