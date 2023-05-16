#ifndef VT_FRAG
#define VT_FRAG

float4 frag(Varyings i) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(i);

    // Normal
    float3 normalWS = i.normalWS;

    // Light
    float3 lightDirWS = i.lightDirWS.xyz;

    // Albedo
    float4 col = (float4)1.;
    float3 mainTexColor = tex2D(_MainTex, i.uv).rgb;
    col.rgb = mainTexColor;
    col *= i.vertColor;

    // Diff
    float NdotL = dot(normalWS, lightDirWS);
    float halfLambert = NdotL * 0.5 + 0.5;
    float diff = halfLambert;
    col.rgb *= diff;

    // Clip
    // float cameraToObjDist = 5;
    // float dist = distance(i.posWS,  _WorldSpaceCameraPos);
    // float2 bayerUV = i.screenPos * _ScreenParams.xy * _BayerTex_TexelSize.xy;
    // float dither = tex2D(_BayerTex, bayerUV).r;
    // clip(saturate(dist/3) - dither);

    // LastColor
    float4 lastCol = float4(0.0, 0.0, 0.0, 1.0);
    lastCol = col;

    // TestCol
    // float4 testCol = float4(dist, dist, dist, 1);

    return lastCol;
    // return testCol;
}

#endif