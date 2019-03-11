using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZone : MonoBehaviour
{

    public GameObject portal;
    public LevelManager levelManager;

    CircleCollider2D ogCollider;
    CircleCollider2D tempCollider;

    bool DoOnce;

    private void Start()
    {
        DoOnce = false;

        ogCollider = gameObject.GetComponent<CircleCollider2D>();
        tempCollider = gameObject.AddComponent<CircleCollider2D>();
        //tempCollider = ogCollider;
        tempCollider.isTrigger = true;
        tempCollider.radius = 100;
    }

    private void Update()
    {
        //tempCollider.radius -= Time.deltaTime * 5;
        //print(tempCollider.offset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == GameObject.Find("Player"))
        {
            Instantiate(portal, this.transform);

            List<FragmentController> fragments = FragmentManager.instance.AttachedFragments();
            foreach(FragmentController fragment in fragments)
            {
                if(DoOnce == false)
                {
                    DoOnce = true;
                    fragment.getColliderDeposit(tempCollider);

                }
                if (fragment.currentState != FragmentController.states.DEPOSIT)
                {
                    fragment.Collect(this.transform);
                }
            }

        }
    }
}
