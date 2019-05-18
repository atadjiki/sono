using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{

    public static FragmentManager instance;

    public List<FragmentController> fragments;

    private SavedData fragmentList;
    public float maxFrames = 120;
    public float currentFrames = 0;

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
            SavedData.instance.fragments = fragments;
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
       // print("FRAGMENTS REFRESHING");
        string p = PlayerPrefs.GetString("SavedData");
        SavedData s = JsonUtility.FromJson<SavedData>(p);
        fragments.Clear();
        fragments.AddRange(FindObjectsOfType<FragmentController>());

        SavedData.instance.fragments = fragments;
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

    public void AddFragment(FragmentController fragment)
    {
        fragments.Add(fragment);

    }
}
