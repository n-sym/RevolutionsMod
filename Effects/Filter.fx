sampler uImage0 : register(s0); // The contents of the screen.
sampler uImage1 : register(s1); // Up to three extra textures you can use for various purposes (for instance as an overlay).
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition; // The position of the camera.
float2 uTargetPosition; // The "target" of the shader, what this actually means tends to vary per shader.
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect; // Doesn't seem to be used, but included for parity.
float2 uZoom;

float2 cvtpos(float2 f)
{
    return (f.x / uScreenResolution.x, f.y / uScreenResolution.y);
}

float cvtnumx(float f)
{
    return (f / uScreenResolution.x);
}

float cvtnumy(float f)
{
    return (f / uScreenResolution.y);
}

float4 PixelShaderFunction0(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    /*float4 color = tex2D(uImage0, coords);
    float a = color.r + color.g + color.b;
    a /= 3;
    color = (a, a, a, a);
    color.xyz *= uColor;*/
    float a = 0;
    float b = 0;
    a = 2 * coords.x - 1;
    a = asin(a) / 3.141;
    a += 0.5;
    b = 2 * coords.y - 1;
    b = asin(b) / 3.141;
    b += 0.5;
    float4 color = tex2D(uImage0, float2(a, b));
    return color;
}

float4 PixelShaderFunction1(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float a = 0;
    float b = 0;
    a = coords.x - 0.5;
    a *= 1.587;
    a *= a * a;
    a += 0.5;
    b = coords.y - 0.5;
    b *= 1.587;
    b *= b * b;
    b += 0.5;
    float4 color = tex2D(uImage0, float2(a, b));
    return color;
}
float4 PixelShaderFunction100(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float a = 0;
    float b = 0;
    a = 1 - coords.x;
    b = 1 - coords.y;
    float4 color = tex2D(uImage0, float2(a, b));
    return color;
}
float4 PixelShaderFunction2(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float a = 0;
    float b = 0;
    a = 1;
    a -= (coords.x - 1) * (coords.x - 1);
    a = sqrt(a);
    b = 1;
    b -= (coords.y - 1) * (coords.y - 1);
    b = sqrt(b);
    float4 color = tex2D(uImage0, float2(a, b));
    return color;
}
float4 PixelShaderFunction3(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float a = 0;
    float b = 0;
    a = 1;
    a -= coords.x * coords.x;
    a = -sqrt(a);
    a += 1;
    b = 1;
    b -= coords.y * coords.y;
    b = -sqrt(b);
    b += 1;
    float4 color = tex2D(uImage0, float2(a, b));
    return color;
}
/*
float4 color = tex2D(uImage0, coords);
    float a = color.r + color.g + color.b;
    a /= 3;
    color = (a, a, a, a);
    return color;
*/

technique Technique1
{
    pass Filter0
    {
        PixelShader = compile ps_2_0 PixelShaderFunction0();
    }
    pass Filter1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction1();
    }
    pass Filter2
    {
        PixelShader = compile ps_2_0 PixelShaderFunction2();
    }
    pass Filter3
    {
        PixelShader = compile ps_2_0 PixelShaderFunction3();
    }
    pass Filter100
    {
        PixelShader = compile ps_2_0 PixelShaderFunction100();
    }

}