using UnityEngine;
using Unity.Barracuda;

namespace Mediapipe.PoseDetection{
    public class PoseDetecter: System.IDisposable
    {
        // Input image size defined by pose detection network model.
        const int IMAGE_SIZE = 128;
        const int MAX_DETECTION = 64;
        IWorker woker;
        Model model;
        ComputeShader preProcessCS;
        ComputeShader postProcessCS;
        ComputeShader postProcess2CS;
        ComputeBuffer networkInputBuffer;
        public ComputeBuffer countBuffer;
        public ComputeBuffer outputBuffer;
        public ComputeBuffer output2Buffer;

        public PoseDetecter(PoseDetectionResource resource){
            preProcessCS = resource.preProcessCS;
            postProcessCS = resource.postProcessCS;
            postProcess2CS = resource.postProcess2CS;
            model = ModelLoader.Load(resource.model);
            woker = model.CreateWorker();
            networkInputBuffer = new ComputeBuffer(IMAGE_SIZE * IMAGE_SIZE * 3, sizeof(float));
            countBuffer = new ComputeBuffer(1, sizeof(uint), ComputeBufferType.Raw);
            outputBuffer = new ComputeBuffer(MAX_DETECTION, PoseDetection.SIZE, ComputeBufferType.Append);
            output2Buffer = new ComputeBuffer(MAX_DETECTION, PoseDetection.SIZE, ComputeBufferType.Append);
        }

        public void ProcessImage(Texture inputTexture){
            outputBuffer.SetCounterValue(0);
            output2Buffer.SetCounterValue(0);

            // Resize `inputTexture` texture to network model image size.
            preProcessCS.SetTexture(0, "inputTexture", inputTexture);
            preProcessCS.SetBuffer(0, "networkInputBuffer", networkInputBuffer);
            preProcessCS.Dispatch(0, IMAGE_SIZE / 8, IMAGE_SIZE / 8, 1);

            //Execute neural network model.
            var inputTensor = new Tensor(1, IMAGE_SIZE, IMAGE_SIZE, 3, networkInputBuffer);
            woker.Execute(inputTensor);
            inputTensor.Dispose();

            //Get neural network model raw output as RenderTexture;
            var scores = CopyOutputToTempRT("classificators", 1, 896);
            var boxes = CopyOutputToTempRT("regressors", 12, 896);

            postProcessCS.SetFloat("IMAGE_SIZE", IMAGE_SIZE);
            postProcessCS.SetTexture(0, "scores", scores);
            postProcessCS.SetTexture(0, "boxes", boxes);
            postProcessCS.SetBuffer(0, "outputBuffer", outputBuffer);
            postProcessCS.Dispatch(0, 1, 1, 1);

            postProcessCS.SetTexture(1, "scores", scores);
            postProcessCS.SetTexture(1, "boxes", boxes);
            postProcessCS.SetBuffer(1, "outputBuffer", outputBuffer);
            postProcessCS.Dispatch(1, 1, 1, 1);

            RenderTexture.ReleaseTemporary(scores);
            RenderTexture.ReleaseTemporary(boxes);
            ComputeBuffer.CopyCount(outputBuffer, countBuffer, 0);
            
            postProcess2CS.SetBuffer(0, "inputBuffer", outputBuffer);
            postProcess2CS.SetBuffer(0, "inputCountBuffer", countBuffer);
            postProcess2CS.SetBuffer(0, "outputBuffer", output2Buffer);
            postProcess2CS.Dispatch(0, 1, 1, 1);

            ComputeBuffer.CopyCount(output2Buffer, countBuffer, 0);
        }

        public void Dispose(){
            networkInputBuffer.Dispose();
            countBuffer.Dispose();
            outputBuffer.Dispose();
            output2Buffer.Dispose();
            woker.Dispose();
        }

        RenderTexture CopyOutputToTempRT(string name, int w, int h)
        {
            var fmt = RenderTextureFormat.RFloat;
            var shape = new TensorShape(1, h, w, 1);
            var rt = RenderTexture.GetTemporary(w, h, 0, fmt);
            var tensor = woker.PeekOutput(name).Reshape(shape);
            tensor.ToRenderTexture(rt);
            tensor.Dispose();
            return rt;
        }
    }
}
