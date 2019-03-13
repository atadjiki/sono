using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class GravityWellPuzzlesCompeteTemplate : MonoBehaviour
{
    public VisualEffect GravityWell;
    public bool PuzzleComplete;
    public Gradient GradientOnKill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PuzzleComplete == true)
        {
            GravityWell.SetFloat("Sett0-100Onkill", 100);
            GravityWell.SetGradient("GradientOnstart", GradientOnKill);
            GravityWell.SetFloat("EmissionRate", 0);
            GravityWell.SetFloat("EmissionRate2", 0);
            
        }
    }
}
