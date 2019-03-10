using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Audio/Pattern", order = 1)]
public class ScorePattern : ScriptableObject
{
    public string PatternName;
    public AudioClip[] clips;
}
