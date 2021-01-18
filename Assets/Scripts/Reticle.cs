using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Based on: https://unity3d.com/learn/tutorials/topics/virtual-reality/interaction-vr
// Also seen in: https://github.com/OSVR/Unity-VR-Samples/blob/master/Assets/VRSampleScenes/Scripts/Utils/Reticle.cs
// and the VRCampus code of the Unity course https://www.coursera.org/learn/mobile-vr-app-development-unity/
public class Reticle : MonoBehaviour {

    // Default distance of the reticle from the camera if not gazing an object
    [SerializeField] private float _defaultDistance = 5.0f;
    // Our script needs to move, rotate and scale the reticle
    [SerializeField] private Transform _reticleTransform;
    // Reticle needs to be placed relative to the camera
    [SerializeField] private Transform _cameraTransform;

    // Surface distance to place the cursor off of the surface at
    [Tooltip("The distance from the hit surface to place the reticle")]
    [SerializeField] private float _surfaceReticleDistance = 0.02f;

    // Store the original scale of the reticle.
    private Vector3 _originalScale;
    // Store the original rotation of the reticle.
    private Quaternion _originalRotation;

    private void Start()
    {
        if (!_cameraTransform)
        {
            // No camera was assigned - search for camera of this game object
            _cameraTransform = GetComponent<Camera>().transform;
        }

        if (_reticleTransform)
        {
            // Store the original scale and rotation.
            _originalScale = _reticleTransform.localScale;
            _originalRotation = _reticleTransform.localRotation;
        }
    }
    
    // Set position of the reticle if no hit was detected. Places
    // it at default distance from the camera, based on original rotation.
    public void SetPosition()
    {
        if (!_reticleTransform) return;
        // Set the position of the reticle to the default distance in front of the camera.
        _reticleTransform.position = _cameraTransform.position 
                                     + _cameraTransform.forward * _defaultDistance;

        // Set the scale based on the original and the distance from the camera.
        _reticleTransform.localScale = _originalScale * _defaultDistance;

        // The rotation should just be the default.
        _reticleTransform.localRotation = _originalRotation;
    }

    // A hit was detected by the raycast. Position the reticle at the
    // hit point, orient it to the surface normal and scale it according
    // to the hit distance. Add a small offset to the surface to prevent clipping.
    public void SetPosition(RaycastHit hit)
    {
        if (!_reticleTransform) return;
        // Set the position to the hit point plus a small offset to avoid clipping
        _reticleTransform.position = hit.point
                                     + hit.normal * _surfaceReticleDistance;

        // Set the scale based on the distance of the hit from the camera (= raycast origin)
        _reticleTransform.localScale = _originalScale * hit.distance;

        // Set its rotation based on its forward vector facing along the hit normal
        _reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    }
}
