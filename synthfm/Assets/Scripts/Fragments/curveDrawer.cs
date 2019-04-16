using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for visualizing the route of the curve

public class curveDrawer : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector2 Pos;

    private void OnDrawGizmos()
    {
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Pos = Mathf.Pow(1 - i, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - i, 2) * i * controlPoints[1].position +
                3 * (1 - i) * Mathf.Pow(i, 2) * controlPoints[2].position +
                Mathf.Pow(i, 3) * controlPoints[3].position;

            // Draw sphere
            Gizmos.DrawSphere(Pos, 0.20f);
        }

        // Draw lines
        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
             new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));


        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
             new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
    }


}
