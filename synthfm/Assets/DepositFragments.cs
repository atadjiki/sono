using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositFragments : MonoBehaviour
{
    [SerializeField] public GameObject CM_Deposit;
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Tag of object collided with hub: " + collision.gameObject.tag);
        //print("Name of object collided with hub: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {

            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach (FragmentController fragment in fragments)
            {
                if (fragment.currentState == FragmentController.states.FOLLOW)
                {
                    fragment.Deposit(gameObject.transform);
                    //GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = fragment.transform;
                    //CM_Deposit.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = fragment.transform;
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach (FragmentController fragment in fragments)
            {
                if (fragment.currentState == FragmentController.states.DEPOSIT)
                {

                    StartCoroutine(DeleteFragments(fragment));
                }

            }
        }

    }

    private IEnumerator DeleteFragments(FragmentController fragment)
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(fragment.gameObject);

    }
}
