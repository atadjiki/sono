using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class ChangeColor : MonoBehaviour
{
    private Camera mainCamera;
    
    [SerializeField] private Color lerpDuration;

    private Color currentColor;
    private Color skyBlue;

    private float duration = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        skyBlue = Color.red;
        mainCamera = gameObject.GetComponent<Camera>();
        mainCamera.clearFlags = CameraClearFlags.SolidColor;
        currentColor = mainCamera.backgroundColor;
        

    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        mainCamera.backgroundColor = Color.Lerp(currentColor, skyBlue, duration);
    }
}
