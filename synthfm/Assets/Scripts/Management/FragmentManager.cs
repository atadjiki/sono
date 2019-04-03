using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{

    public static FragmentManager instance;

    public List<FragmentController> fragments;

    private float maxFrames = 1000;
    private float currentFrames = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RefreshFragmentList();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFrames >= maxFrames)
        {
            RefreshFragmentList();
            currentFrames = 0;
        }
        else
        {
            currentFrames++;
        }
    }

    public void RefreshFragmentList()
    {
        fragments.Clear();
        fragments.AddRange(FindObjectsOfType<FragmentController>());
    }

    public List<FragmentController> AttachedFragments()
    {
        List<FragmentController> attachedFragments = new List<FragmentController>();

        foreach(FragmentController fragment in fragments)
        {
            if (fragment.isAttached)
            {
                attachedFragments.Add(fragment);
            }
        }
        return attachedFragments;
    }

    public int CountAttachedFragments()
    {
        int count = 0;

        foreach (FragmentController fragment in fragments)
        {
            if (fragment.isAttached)
            {
                count++;
            }
        }
        return count;
    }

    public List<FragmentController> UnattachedFragments()
    {
        List<FragmentController> unattachedFragments = new List<FragmentController>();

        foreach (FragmentController fragment in fragments)
        {
            if (!fragment.isAttached)
            {
                unattachedFragments.Add(fragment);
            }
        }
        return unattachedFragments;
    }

    public int CountUnttachedFragments()
    {
        int count = 0;

        foreach (FragmentController fragment in fragments)
        {
            if (!fragment.isAttached)
            {
                count++;
            }
        }
        return count;
    }

    public List<FragmentController> GetFragmentsByState(FragmentController.states state)
    {
        List<FragmentController> fragmentsInState = new List<FragmentController>();

        foreach(FragmentController fragment in fragments)
        {
            if(fragment.GetState() == state)
            {
                fragmentsInState.Add(fragment);
            }
        }

        return fragmentsInState;

    }
}
