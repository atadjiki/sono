using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class ChangeColor : MonoBehaviour
{    
    [SerializeField] private float lerpDuration;
    [SerializeField] private HDAdditionalCameraData mainCam;
    [SerializeField] private MeshRenderer playerBody;

    [SerializeField] public Color dark;
    [SerializeField] public Color saturated;
    [SerializeField] public Color voidbgColor;
    [SerializeField] public List<Color> firstFiberPuzzleColor;
    [SerializeField] public List<Color> secondFiberPuzzleColor;
    [SerializeField] public List<Color> thirdFiberPuzzleColor;
    [SerializeField] public List<Color> firstamberPuzzleColor;
    [SerializeField] public List<Color> secondamberPuzzleColor;
    [SerializeField] public List<Color> thirdamberPuzzleColor;
    [SerializeField] public List<Color> firstlattePuzzleColor;
    [SerializeField] public List<Color> secondlattePuzzleColor;
    [SerializeField] public List<Color> thirdlattePuzzleColor;
    [SerializeField] public List<Color> voidColor;


    private Color currentColor;
    private Color colorToChangeTo;
    private bool shouldchangeColor;



    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        print(playerBody.material.GetColor("Color_D2FAE4B8"));
        shouldchangeColor = false;
        t = 0f;
        currentColor = mainCam.backgroundColorHDR;
        colorToChangeTo = saturated;
        //StartCoroutine(changeColor(saturated));

    }

    // Update is called once per frame
    void Update()
    {

        if (t < 1f && shouldchangeColor == true)
        {
            mainCam.backgroundColorHDR = Color.Lerp(currentColor, colorToChangeTo, t);
            t += Time.deltaTime / lerpDuration;
        }
    }

    IEnumerator changeColor(Color color)
    {
        shouldchangeColor = true;
        colorToChangeTo = color;
        yield return new WaitForSeconds(lerpDuration);
        shouldchangeColor = false;
        t = 0;
    }
}
