using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _overMaterial;

    private Renderer _gameObjectRenderer;
    private GazeTarget _gazeTargetComponent;
    
    void Awake ()
    {
        _gazeTargetComponent = GetComponent<GazeTarget>();
        _gameObjectRenderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _gazeTargetComponent.GazeEntered += OnGazeEntered;
        _gazeTargetComponent.GazeOut += OnGazeOut;
    }


    private void OnDisable()
    {
        _gazeTargetComponent.GazeEntered -= OnGazeEntered;
        _gazeTargetComponent.GazeOut -= OnGazeOut;
    }

    //Handle the OnGazeEntered event
    public void OnGazeEntered(object sender, Vector3 impactPoint)
    {
        Debug.Log("Impact point: " + impactPoint);
        // Change material of GameObject's renderer to "over" material
        if (_gameObjectRenderer)
        {
            _gameObjectRenderer.material = _overMaterial;
        }
    }

    //Handle the OnGazeOut event
    public void OnGazeOut()
    {
        // Change material to "normal" material
        if (_gameObjectRenderer)
        {
            _gameObjectRenderer.material = _normalMaterial;
        }

    }
}
