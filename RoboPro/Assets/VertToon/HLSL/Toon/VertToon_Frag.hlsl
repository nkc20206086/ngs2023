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
    float3 mainTexColor = tex2D(_MainTex, i.uv);
    col.rgb = mainTexColor;
    col.rgb *= i.vertColor;

    // Diff
    float NdotL = dot(normalWS, lightDirWS);
    float halfLambert = NdotL * 0.5 + 0.5;
    float diff = halfLambert;
    col.rgb *= diff;

    // LastColor
    float4 lastCol = float4(0.0, 0.0, 0.0, 1.0);
    lastCol.rgb = col.rgb;

    return lastCol;
}

#endif