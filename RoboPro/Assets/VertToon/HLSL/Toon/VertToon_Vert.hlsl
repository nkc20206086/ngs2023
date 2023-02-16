#ifndef VT_VERT
#define VT_VERT

Varyings vert(Attributes v)
{
    Varyings o = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);

    // pos
    o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
    
    // uv
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    // normal
    o.normalWS = TransformObjectToWorldNormal(v.normalOS);

    // DL
    Light light = GetMainLight();
    float3 dlColor = light.color;
    o.lightDirWS.xyz = light.direction.xyz;
    
    // vertColor
    o.vertColor.rgb = (float3)1.;
    o.vertColor.rgb *= dlColor;
    if(_UseVertColor)
    {
        o.vertColor.rgb *= v.color.rgb;
    }

    // Color
    o.vertColor.rgb *= _Color.rgb;
    
    return o;
}

#endif
