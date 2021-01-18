# ARVR-MKL-Gaze

Reference project for raycasting, gaze and reticle support in AR / VR projects with Unity

## What is this about?

Reference architecture showcasing several core concepts of scene setup for Augmented and Virtual Reality projects in Unity.

* **Raycasting:** for gaze, hand-based and controller-based interaction
* **Delegates & Events:** for informing the target game object that the raycast entered or left the object.
  * Uses the C# event system to communicate within the scene.
  * `GazeOut` is defined as an event based on a custom delegate without parameters.
  * `GazeEntered` is defined using the EventHandler implicit delegate, sending the hit position as parameter.
* **Reticle:** to indicate the position where the raycast hit the target object
  * Includes a default distance if no hit was detected.
  * Its orientation adapts to the surface normal of the object that was hit.
* **Unit tests:** Showcase of automated Play Mode unit tests using the [NUnit](https://nunit.org/) system through Unity.

## How are events processed in this solution?

The source `GameObject` (in this case the camera) performs a raycast in its forward direction. In case it hits a `GameObject`, it checks if that has an instance of the generic `GazeTarget` script attached. This indicates that the target object is interested in receiving gaze events. In that case, the script on the camera calls the appropriate method of the target script (`OnGazeEntered()` or `OnGazeOut()`) via a direct method call.

The target object additionally has a custom implementation that is unique to how this specific `GameObject` should react to being looked upon. In this sample, the targets change their color; the respective scripts are called `ColorChanger`.

The GameObject-"internal" communication from the generic `GazeTarget` script to the custom `ColorChanger` script happens via events. `GazeTarget` emits the `GazeOut` and `GazeEntered` events, which the `ColorChanger` script subscribes to.

![Visualization of the raycast / gaze event architecture](https://raw.githubusercontent.com/andijakl/ARVR-MKL-Gaze/main/EventSystemForGaze.svg)

### Advantages of this architecture

This architecture approach has the advantage that the source `GameObject` (e.g., the camera) doesn't need to know anything about the receiving object and can easily distinguish the raycast hits based on whether that object has a `GazeTarget` script component attached.

The `GazeTarget` script can remain generic. You can attach the same script to any `GameObject`. For the custom actions the receiving (gazed-upon) object should perform, a custom C# script only needs to subscribe to the events emitted by the `GazeTarget` script component from the same `GameObject`.

## Credits

The concept of sending the events is based on the [Mobile VR App Development course by Coursera](https://www.coursera.org/learn/mobile-vr-app-development-unity/). The implementation of the Reticle is also based on the [Open Source Virtual Reality](https://github.com/OSVR/Unity-VR-Samples/blob/8ee8a7fd53dc75532678b1d4a9023ba3d31d94ea/Assets/VRSampleScenes/Scripts/Utils/Reticle.cs) framework.

Released under the MIT License – see the LICENSE file for details.

Developed by Andreas Jakl, Professor at the St. Pölten University of Applied Sciences, Austria.

* <https://www.andreasjakl.com/>
* <https://twitter.com/andijakl>
* <https://www.fhstp.ac.at/>
