# PoseDetectionBarracuda
![demo](https://user-images.githubusercontent.com/34697515/148678516-9b608be0-40a0-4c01-98da-c1c22d021cba.png)

PoseDetectionBarracuda is a human pose detecter that runs the [Mediapipe Pose](https://google.github.io/mediapipe/solutions/pose) Detection neural network model on the [Unity Barracuda](https://docs.unity3d.com/Packages/com.unity.barracuda@latest).

PoseDetectionBarracuda implementation is inspired by [BlazePalmBarracuda](https://github.com/keijiro/BlazePalmBarracuda) and I referenced [his](https://github.com/keijiro) source code.(Thanks, [keijiro](https://github.com/keijiro)!).

## Install
PoseDetectionBarracuda can be installed with npm or GitHub URL.

### Install from npm (Recommend)
PoseDetectionBarracuda can be installed by adding following sections to the manifest file (Packages/manifest.json).

To the `scopedRegistries` section:
```
{
  "name": "creativeikep",
  "url": "https://registry.npmjs.com",
  "scopes": [ "jp.ikep" ]
}
```
To the `dependencies` section:
```
"jp.ikep.mediapipe.posedetection": "1.0.1"
```
Finally, the manifest file looks like below:
```
{
    "scopedRegistries": [
        {
            "name": "creativeikep",
            "url": "https://registry.npmjs.com",
            "scopes": [ "jp.ikep" ]
        }
    ],
    "dependencies": {
        "jp.ikep.mediapipe.posedetection": "1.0.1",
        ...
    }
}
```

### Install from GitHub URL
PoseDetectionBarracuda can be installed by adding below URL on the Unity Package Manager's window.
```
https://github.com/creativeIKEP/PoseDetectionBarracuda.git?path=Packages/PoseDetectionBarracuda#v1.0.1
```

## Demo Image
Demo image was downloaded from [here](https://unsplash.com/photos/4jqfc2vbHJQ).

## ONNX Model
The ONNX model files have been converted for Unity Barracuda from Mediapipe's ["pose_detection.tflite"](https://github.com/google/mediapipe/blob/0.8.3.2/mediapipe/modules/pose_detection/pose_detection.tflite) file.
The conversion operation is the same as [FaceLandmarkBarracuda](https://github.com/keijiro/FaceLandmarkBarracuda) by [keijiro](https://github.com/keijiro).
Check [his operation script](https://colab.research.google.com/drive/1C6zEB3__gcHEWnWRm-b4jIA0srA1gkyq?usp=sharing) for details.

## Author
[IKEP](https://ikep.jp)

## LICENSE
Copyright (c) 2021 IKEP

[Apache-2.0](/LICENSE.md)
