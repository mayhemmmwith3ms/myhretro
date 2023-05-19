sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
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
float4 uSourceRect;
float2 uZoom;

const int ditherMap[4][4] = 
{
    {0, 8, 2, 10},
    {12, 4, 14, 6},
    {3, 11, 1, 9},
    {15, 7, 13, 5}
};

float4 DitherShader(float2 coords : TEXCOORD0) : COLOR0
{
    float2 halfScreenRes = uScreenResolution / 2;
    float2 roundedCoords = float2(round(coords.x * halfScreenRes.x) / (halfScreenRes.x), round(coords.y * halfScreenRes.y) / (halfScreenRes.y));
    float4 color = tex2D(uImage0, roundedCoords);
    int2 ditherCoords = int2((roundedCoords.x * halfScreenRes.x) % 4, (roundedCoords.y * halfScreenRes.y) % 4);
    float noise = ((float)ditherMap[ditherCoords.x][ditherCoords.y] / 16.0) - 0.5;

    return color + noise * uIntensity;
}

technique Technique1
{
    pass DitherShader
    {
        PixelShader = compile ps_2_0 DitherShader();
    }
}