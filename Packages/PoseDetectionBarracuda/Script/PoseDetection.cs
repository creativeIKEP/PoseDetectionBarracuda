using UnityEngine;

namespace Mediapipe.PoseDetection{
    // This struct is related with PoseDetection struct in "Common.cginc".
    public struct PoseDetection{
        public readonly float score;
        public readonly Vector2 center;
        public readonly Vector2 extent;
        
        public readonly Vector2 hipCenter;
        public readonly Vector2 roi_full;
        public readonly Vector2 shoulderCenter;
        public readonly Vector2 roi_upper;
        public const int SIZE = 13 * sizeof(float);
    }
}