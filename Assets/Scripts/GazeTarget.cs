using System;
using UnityEngine;

public class GazeTarget : MonoBehaviour
{
    // Called when gaze moves over the GameObject
    // Defined as EventHandler with one parameter - the impact point
    public event EventHandler<Vector3> GazeEntered;

    // Called when gaze leaves the GameObject
    // Defined as custom delegate
    public delegate void GazeOutEventHandler();
    public event GazeOutEventHandler GazeOut;


    protected bool IsOver { get; set; }


    public void OnGazeEntered(Vector3 impactPoint)
    {
        IsOver = true;
        //Debug.Log("OnGazeEntered");
        // Only calls Invoke if GazeEntered != null meaning that someone has subscribed to this event
        GazeEntered?.Invoke(this, impactPoint);
    }


    public void OnGazeOut()
    {
        IsOver = false;
        //Debug.Log("OnGazeOut");
        GazeOut?.Invoke();
    }
}
