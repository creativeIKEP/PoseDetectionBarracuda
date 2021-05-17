# PoseDetectionBarracuda
![demo](/screenshot/demo.png)

PoseDetectionBarracuda is a human pose detecter that runs the [Mediapipe Pose](https://google.github.io/mediapipe/solutions/pose) Detection neural network model on the [Unity Barracuda](https://docs.unity3d.com/Packages/com.unity.barracuda@latest).

PoseDetectionBarracuda implementation is inspired by [BlazePalmBarracuda](https://github.com/keijiro/BlazePalmBarracuda) and I referenced [his](https://github.com/keijiro) source code.(Thanks, [keijiro](https://github.com/keijiro)!).

### Install
PoseDetectionBarracuda can be installed by adding below URL on the Unity Package Manager's window.
```
https://github.com/creativeIKEP/PoseDetectionBarracuda.git?path=Packages/PoseDetectionBarracuda#v1.0.0
```

### Demo Image
Demo image was downloaded from [here](https://unsplash.com/photos/4jqfc2vbHJQ).

### ONNX Model
The ONNX model files have been converted for Unity Barracuda from Mediapipe's ["pose_detection.tflite"](https://github.com/google/mediapipe/blob/0.8.3.2/mediapipe/modules/pose_detection/pose_detection.tflite) file.
The conversion operation is the same as [FaceLandmarkBarracuda](https://github.com/keijiro/FaceLandmarkBarracuda) by [keijiro](https://github.com/keijiro).
Check [his operation script](https://colab.research.google.com/drive/1C6zEB3__gcHEWnWRm-b4jIA0srA1gkyq?usp=sharing) for details.

### Author
[IKEP](https://ikep.jp)

### LICENSE
Copyright (c) 2021 IKEP

[Apache-2.0](/LICENSE.md)
