using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFinalZoneAudio : MonoBehaviour
{
    private bool FinalZoneStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FinalZoneStarted)
            return;
        Debug.Log("Starting Final Audio");
        ScoreManager._instance.LoadFinalZone();
        ScoreManager._instance.Crossfade();
        StartCoroutine(ScoreManager._instance.PlayCreditsAfterFinal());
        FinalZoneStarted = true;
    }
}
