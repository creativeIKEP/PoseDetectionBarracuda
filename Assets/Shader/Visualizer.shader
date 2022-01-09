Shader "Hidden/PoseDetection/Visualizer"
{
    CGINCLUDE

    #include "UnityCG.cginc"
    #include "Packages/PoseDetectionBarracuda/ComputeShader/Common.cginc"

    #define PI 3.14159265359

    StructuredBuffer<PoseDetection> _pds;
    
    float2x2 rot2D(float angle)
    {
        return float2x2(cos(angle), -sin(angle),
                        sin(angle),  cos(angle));
    }


    float4 VertexFaceBox(uint vid : SV_VertexID, uint iid : SV_InstanceID): SV_POSITION
    {   
        PoseDetection pd = _pds[iid];
        float x = pd.center.x + pd.extent.x * lerp(-0.5, 0.5, vid & 1);
        float y = pd.center.y + pd.extent.y * lerp(-0.5, 0.5, vid < 2 || vid == 5);
        y = 1.0 - y;

        x = (2 * x - 1) * _ScreenParams.y / _ScreenParams.x;
        y =  2 * y - 1;

        return float4(x, y, 0, 1);
    }

    float4 FragmentFaceBox(float4 position : SV_Position): SV_Target
    {
        return float4(1, 0, 0, 0.5);
    }


    float4 VertexBodyBox(uint vid : SV_VertexID, uint iid : SV_InstanceID): SV_POSITION
    {
        PoseDetection pd = _pds[iid];
        float2 hip = pd.keyPoints[0];
        float2 shoulder = pd.keyPoints[2];

        float2 center = hip;
        float2 roi = pd.keyPoints[1];

        float sizeX = abs(roi.x - center.x);
        float sizeY = abs(roi.y - center.y);
        float size = max(sizeX, sizeY) * 3.0;
        
        float x = size * lerp(-0.5, 0.5, vid & 1);
        float y = size * lerp(-0.5, 0.5, vid < 2 || vid == 5);

        float target = PI * 0.5;
        float angle = target - atan2(-(shoulder.y - hip.y), shoulder.x - hip.x);
        angle = angle - 2 * PI * floor((angle + PI) / (2 * PI));

        float2 rotPos = mul(rot2D(angle), float2(x, y));
        x = rotPos.x + center.x;
        y = rotPos.y + center.y;
        y = 1.0 - y;

        x = (2 * x - 1) * _ScreenParams.y / _ScreenParams.x;
        y = (2 * y - 1);

        return float4(x, y, 0, 1);
    }

    float4 FragmentBodyBox(float4 position : SV_Position): SV_Target
    {
        return float4(0, 1, 0, 0.5);
    }


    float4 VertexBodyLine(uint vid : SV_VertexID, uint iid : SV_InstanceID): SV_POSITION
    {
        PoseDetection pd = _pds[iid];
        float2 hip = pd.keyPoints[0];
        float2 shoulder = pd.keyPoints[2];

        float x = lerp(hip.x, shoulder.x, vid == 1);
        float y = lerp(hip.y, shoulder.y, vid == 1);
        y = 1.0 - y;

        x = (2 * x - 1) * _ScreenParams.y / _ScreenParams.x;
        y = (2 * y - 1);
        return float4(x, y, 0, 1);
    }

    float4 FragmentBodyLine(float4 position : SV_Position): SV_Target
    {
        return float4(0, 0, 1, 1);
    }

    ENDCG


    SubShader
    {
        ZWrite Off ZTest Always Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex VertexFaceBox
            #pragma fragment FragmentFaceBox
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex VertexBodyBox
            #pragma fragment FragmentBodyBox
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex VertexBodyLine
            #pragma fragment FragmentBodyLine
            ENDCG
        }
    }
}
