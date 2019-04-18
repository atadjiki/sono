using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class ChangeColor : MonoBehaviour
{    
    [SerializeField] private float lerpDuration;
    [SerializeField] private HDAdditionalCameraData mainCam;
    [SerializeField] private MeshRenderer playerBody;
    [SerializeField] private TrailRenderer playerTrail;

    [Header("All Color profiles for puzzles and worlds")]

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

    [Header("All Color profiles for Fragments")]

    [SerializeField] public List<Color> Fiber01;
    [SerializeField] public List<Color> Fiber02;
    [SerializeField] public List<Color> Fiber03;

    [SerializeField] public List<Color> Latte01;
    [SerializeField] public List<Color> Latte02;
    [SerializeField] public List<Color> Latte03;

    [SerializeField] public List<Color> Amber01;
    [SerializeField] public List<Color> Amber02;
    [SerializeField] public List<Color> Amber03;

    private Color currentColor;
    public Color currentPlayercolor;
    public Color currentTrailColor;
    private Color bgcolorToChangeTo;
    private Color playercolorToChangeTo;
    private Color trailColortoChangeTo;
    private bool shouldchangeColor;



    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayercolor = (playerBody.material.GetColor("Color_D2FAE4B8"));
        shouldchangeColor = false;
        t = 0f;
        currentTrailColor = playerTrail.startColor;
        currentColor = mainCam.backgroundColorHDR;
    }

    // Update is called once per frame
    void Update()
    {

        if (t < 1f && shouldchangeColor == true)
        {
            Color playerCol = playerBody.material.GetColor("Color_D2FAE4B8");
            playerCol = Color.Lerp(currentPlayercolor, playercolorToChangeTo, t);
            playerBody.material.SetColor("Color_D2FAE4B8", playerCol);
            playerTrail.startColor = Color.Lerp(currentTrailColor, trailColortoChangeTo, t);
            mainCam.backgroundColorHDR = Color.Lerp(currentColor, bgcolorToChangeTo, t);
            t += Time.deltaTime / lerpDuration;
        }
    }

    public IEnumerator changeColor(Color bgColor, Color playerColor,Color trailColor)
    {
        shouldchangeColor = true;
        bgcolorToChangeTo = bgColor;
        trailColortoChangeTo = trailColor;
        playercolorToChangeTo = playerColor;
        yield return new WaitForSeconds(lerpDuration);
        currentPlayercolor = (playerBody.material.GetColor("Color_D2FAE4B8"));
        currentTrailColor = playerTrail.startColor;
        currentColor = mainCam.backgroundColorHDR;
        shouldchangeColor = false;
        t = 0f;
    }

    public void changeFragmentColors(string currentWorld)
    {
        if(currentWorld == "Amber")
        {

        }
        else if(currentWorld == "Latte")
        {

        }
        else if(currentWorld == "Fiber")
        {

        }
    }
}
