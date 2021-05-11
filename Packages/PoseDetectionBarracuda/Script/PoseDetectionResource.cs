using UnityEngine;
using Unity.Barracuda;

namespace Mediapipe.PoseDetection{
    [CreateAssetMenu(fileName = "PoseDetection", menuName = "ScriptableObjects/Pose Detection Resource")]
    public class PoseDetectionResource : ScriptableObject
    {
        public ComputeShader preProcessCS;
        public ComputeShader postProcessCS;
        public ComputeShader postProcess2CS;

        public NNModel model;
    }
}