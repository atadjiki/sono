using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLatteFragments : MonoBehaviour
{
    private ChangeColor cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<ChangeColor>();
        ChangeLatteFragment("Latte");
    }

    private void ChangeLatteFragment(string world)
    {
        cc.changeFragmentColors(world);
    }
}