using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeColor : MonoBehaviour
{    
    [SerializeField] private float lerpDuration;
    [SerializeField] private UniversalAdditionalCameraData mainCam;
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
        //currentColor = mainCam.backgroundColorHDR;
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
            //mainCam.backgroundColorHDR = Color.Lerp(currentColor, bgcolorToChangeTo, t);
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
        //currentColor = mainCam.backgroundColorHDR;
        shouldchangeColor = false;
        t = 0f;
    }

    public void changeFragmentColors(string currentWorld)
    {
        List<FragmentController> fragments = new List<FragmentController>();
        string p = PlayerPrefs.GetString("SavedData");
        SavedData s = JsonUtility.FromJson<SavedData>(p);
        fragments.Clear();
        fragments.AddRange(FindObjectsOfType<FragmentController>());

        if (currentWorld == "Amber")
        {
            List<FragmentController> amberFrags = new List<FragmentController>();
            foreach(FragmentController frag in fragments)
            {
                if(frag.currentWorld == FragmentController.world.AMBER)
                {
                    amberFrags.Add(frag);
                }
            }

            List<List<Color>> allAmberColors = new List<List<Color>>();
            allAmberColors.Add(Amber01);
            allAmberColors.Add(Amber02);
            allAmberColors.Add(Amber03);

            changeFragColors(amberFrags,allAmberColors);


        }
        else if(currentWorld == "Latte")
        {
            List<FragmentController> latteFrags = new List<FragmentController>();
            foreach (FragmentController frag in fragments)
            {
                if (frag.currentWorld == FragmentController.world.LATTE)
                {
                    latteFrags.Add(frag);
                }
            }
            List<List<Color>> allLatteColors = new List<List<Color>>();
            allLatteColors.Add(Latte01);
            allLatteColors.Add(Latte02);
            allLatteColors.Add(Latte03);

            changeFragColors(latteFrags, allLatteColors);

        }
        else if(currentWorld == "Fiber")
        {
            List<FragmentController> fiberFrags = new List<FragmentController>();
            foreach (FragmentController frag in fragments)
            {
                if (frag.currentWorld == FragmentController.world.FIBER)
                {
                    fiberFrags.Add(frag);
                }
            }
            List<List<Color>> allFiberColors = new List<List<Color>>();
            allFiberColors.Add(Fiber01);
            allFiberColors.Add(Fiber02);
            allFiberColors.Add(Fiber03);

            changeFragColors(fiberFrags, allFiberColors);

        }
    }

    private void changeFragColors(List<FragmentController> frags, List<List<Color>> colors)
    {
        FragmentManager.instance.currentFrames = FragmentManager.instance.maxFrames;

        for (int i = 0; i<frags.Count;i++)
        {
            GameObject temp = frags[i].gameObject;
            List<Color> tempColor = colors[i];

            foreach (Transform child in temp.transform)
            {
                if(child.gameObject.name == "Trail")
                {
                    child.gameObject.GetComponent<TrailRenderer>().startColor = tempColor[1];

                }
                else if(child.gameObject.name == "Asset 2")
                {
                    child.gameObject.GetComponent<SpriteRenderer>().color = tempColor[0];
                }
            }
        }
    }
}
