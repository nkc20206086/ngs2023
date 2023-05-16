#ifndef VT_VERT
#define VT_VERT

Varyings vert(Attributes v)
{
    Varyings o = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);

    // pos
    VertexPositionInputs vertexInput = GetVertexPositionInputs(v.positionOS.xyz);
    o.positionHCS = vertexInput.positionCS;
    // o.screenPos = vertexInput.positionNDC;
    // o.posWS = vertexInput.positionWS;

    // uv
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    // normal
    o.normalWS = TransformObjectToWorldNormal(v.normalOS);

    // DL
    Light light = GetMainLight();
    float3 dlColor = light.color;
    o.lightDirWS.xyz = light.direction.xyz;
    
    // vertColor
    o.vertColor = (float4)1.;
    o.vertColor.rgb *= dlColor;
    if(_UseVertColor)
    {
        o.vertColor.rgb *= v.color.rgb;
    }

    // Color
    o.vertColor *= _Color;
    
    return o;
}

#endif
