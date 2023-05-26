#ifndef VT_INPUT
#define VT_INPUT

CBUFFER_START(UnityPerMaterial)
    uniform float4 _MainTex_ST;
    uniform float4 _Color;
    uniform int _UseVertColor;
    uniform float3 _HideColor;
    // uniform float4 _BayerTex_TexelSize;
CBUFFER_END

#endif