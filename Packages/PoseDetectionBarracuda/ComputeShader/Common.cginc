#ifndef POSE_DETECTION_COMMON
#define POSE_DETECTION_COMMON

// Input image size defined by pose detection neural network model.
#define IMAGE_SIZE 128

// This struct is related with "PoseDetection.cs"
struct PoseDetection
{
    float score;
    float2 center;
    float2 extent;
    float2 keyPoints[4];
};

#endif