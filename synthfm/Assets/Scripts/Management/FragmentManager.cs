using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{

    public static FragmentManager instance;

    public List<FragmentController> fragments;

    private SavedData fragmentList;
    private float maxFrames = 1000;
    private float currentFrames = 0;

    private void Awake()
    {
        //fragmentList = new SavedData();
        if (SavedData.instance == null)
        {
            SavedData.instance = new SavedData();
        }

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
        SetupFragments();

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
    
    public void SetupFragments()
    {
        if(fragments != null)
        {
            SavedData.instance.hubFragments = fragments;
            print(fragments.Capacity);
            string json = JsonUtility.ToJson(SavedData.instance);
            PlayerPrefs.SetString("SavedData", json);
        }
        else
        {
            print("no fragments to get, cant populate json!");

        }
    }
    
    public void RefreshFragmentList()
    {
        string p = PlayerPrefs.GetString("SavedData");
        SavedData s = JsonUtility.FromJson<SavedData>(p);

        if (s.hubFragments.Capacity == 0)
        {
            fragments.Clear();
            fragments.AddRange(FindObjectsOfType<FragmentController>());
        }
        else
        {
            //TO DO: Get the state of fragments and do spawn them. s.HubFragments will get you all the fragments and everything associated with them
            fragments = s.hubFragments;  
        }

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
