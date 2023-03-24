Shader "VertToon/DepthOnly"
{
    SubShader
    {
        Tags
        {
            "RenderType" = "Background+10"
            "LightMode" = "DepthOnly"
        }
        LOD 100
        ZWrite On
        ZTest LEqual
        ColorMask 0

        Pass
        {

        }
    }
}
