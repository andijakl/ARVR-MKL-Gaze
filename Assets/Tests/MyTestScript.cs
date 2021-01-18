using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MyTestScript
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("VR-Gaze");
    }

    [UnityTest]
    public IEnumerator MyTestChangeColorToGazeEntered()
    {
        // Find object in the scene
        var targetObj = GameObject.Find("Sphere");
        // Assert that the object was found
        Assert.IsNotNull(targetObj, "Could not find sphere");
        // Get our script component from the object and assert it was found
        var colorChanger = targetObj.GetComponent<ColorChanger>();
        Assert.IsNotNull(colorChanger, "Sphere does not have color changer attached");

        // Call custom method and assert if the material changed
        colorChanger.OnGazeEntered(null, new Vector3());
        var targetObjRenderer = targetObj.GetComponent<Renderer>();
        // sharedMaterial gives the original material name. Otherwise, " (instance)" would be added
        Assert.That(targetObjRenderer.sharedMaterial.name, Is.EqualTo("SelectedMaterial"));

        // Wait one frame
        yield return null;

        // Check if changing back the material when exiting gaze works as well
        colorChanger.OnGazeOut();
        Assert.That(targetObjRenderer.sharedMaterial.name, Is.EqualTo("NormalMaterial"));
    }
}
