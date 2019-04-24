using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleProgressManager : MonoBehaviour
{

    public static PuzzleProgressManager instance;

    private int amber_puzzles = 3;
    private int amber_count = 0;
    private bool amber_deleted = false;

    private int latte_puzzles = 3;
    private int latte_count = 0;
    private bool latte_deleted = false;

    private int fiber_puzzles = 3;
    private int fiber_count = 0;
    private bool fiber_deleted = false;

    private GameObject lastCompleted = null;

    Cinemachine.CinemachineVirtualCamera mainCamera;

    public enum World { Amber, Latte, Fiber};

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

        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    public bool NotifyCount(World world, GameObject completedPuzzle)
    {

        lastCompleted = completedPuzzle;

        if(world == World.Amber)
        {
            if (amber_deleted) return false;

            amber_count++;
            Debug.Log("Notified - amber - " + amber_count);

            if (amber_count >= amber_puzzles || amber_count >= amber_puzzles)
            {
                ArtifactDropper.instance.DropArtifact(ArtifactDropper.World.Amber);

                
                foreach (ClusterManager puzzle in GameObject.FindObjectsOfType<ClusterManager>())
                {
                    foreach (RockIt rock in puzzle.gameObject.GetComponentsInChildren<RockIt>())
                    {
                        rock.enabled = false;
                    }
                    foreach (Crystal crystal in puzzle.gameObject.GetComponentsInChildren<Crystal>())
                    {
                        crystal.enabled = false;
                    }


                    StartCoroutine(FadeOutPuzzle(puzzle.gameObject));
                    //TODO: Fade out

                }
                mainCamera.enabled = true;
                amber_deleted = true;
                FXToggle.instance.ToggleFX(FragmentController.world.AMBER);
            }
            return true;

        }else if(world == World.Fiber)
        {

            if (fiber_deleted ||fiber_count >= fiber_puzzles) return false;

            fiber_count++;
            Debug.Log("Notified - Fiber - " + fiber_count);

            if (fiber_count >= fiber_puzzles)
            {
                ArtifactDropper.instance.DropArtifact(ArtifactDropper.World.Fiber);
                foreach (GatePuzzle puzzle in GameObject.FindObjectsOfType<GatePuzzle>())
                {
                    StartCoroutine(FadeOutPuzzle(puzzle.gameObject));
                    //TODO: Fade out
                }

                mainCamera.enabled = true;
                fiber_deleted = true;
                FXToggle.instance.ToggleFX(FragmentController.world.FIBER);
            }
            return true;
        }
        else
        {
            if (latte_deleted || latte_count >= latte_puzzles) return false;

            latte_count++;
            Debug.Log("Notified - latte - " + latte_count);
            if (latte_count >= latte_puzzles)
            {
                ArtifactDropper.instance.DropArtifact(ArtifactDropper.World.Latte);
                foreach (LattePuzzle puzzle in GameObject.FindObjectsOfType<LattePuzzle>())
                {
                    StartCoroutine(FadeOutPuzzle(puzzle.gameObject));
                   //TODO: Fade out
                    
                }

                mainCamera.enabled = true;
                latte_deleted = true;
                FXToggle.instance.ToggleFX(FragmentController.world.LATTE);
            }

            return true;
        }
    }

    public IEnumerator FadeOutPuzzle(GameObject toDelete)
    {
        Debug.Log("Fading out puzzle " + toDelete.gameObject.name);
        foreach(SpriteRenderer renderer in toDelete.GetComponentsInChildren<SpriteRenderer>())
        {
            Debug.Log("Fading out " + renderer.gameObject.name);
            StartCoroutine(FadeTo(renderer));
        }
        yield return new WaitForSeconds(2.5f);
        //Destroy(toDelete);
    }

    // Define an enumerator to perform our fading.
    // Pass it the material to fade, the opacity to fade to (0 = transparent, 1 = opaque),
    // and the number of seconds to fade over.
  //  https://gamedev.stackexchange.com/questions/142791/how-can-i-fade-a-game-object-in-and-out-over-a-specified-duration
    IEnumerator FadeTo(SpriteRenderer renderer)
    {

        // Cache the current color of the material, and its initiql opacity.
        Color color = renderer.color;
        float startOpacity = color.a;
        float targetOpacity = 0.0f;
        float duration = 2.5f;

        // Track how many seconds we've been fading.
        float t = 0;

        while (t < duration)
        {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            renderer.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }
    }

    public int GetCount(World world)
    {
        if(world == World.Amber)
        {
            return amber_count;
        }else if(world == World.Fiber)
        {
            return fiber_count;
        }
        else
        {
            return latte_count;
        }
    }

    public GameObject GetLastPuzzle()
    {
        return lastCompleted;
    }

    public Transform GetLastPuzzleLocation()
    {
        return lastCompleted.transform;
    }


}
