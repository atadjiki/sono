using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFiberFragments : MonoBehaviour
{
    private ChangeColor cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<ChangeColor>();
        ChangeFiberFragment("Fiber");
    }

    private void ChangeFiberFragment(string world)
    {
        cc.changeFragmentColors(world);
    }
}
