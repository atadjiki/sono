using System.Collections.Generic;
[System.Serializable]
public class SavedData 
{
    public List<FragmentController> hubFragments;
    public List<FragmentController> fiberFragments;
    public List<FragmentController> latteFragments;
    public List<FragmentController> amberFragments;

    public List<FragmentController> fragments;

    public List<Puzzle> hubLevels;
    public List<Puzzle> amberLevels;
    public List<Puzzle> fiberLevels;
    public List<Puzzle> latteLevels;

    public static SavedData instance;
}
