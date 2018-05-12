using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reveive : MonoBehaviour {

    public bool allow = true;
    private float CalmDown = 0;

    private void FixedUpdate()
    {
        if (allow == false && CalmDown > 100)
        {
            allow = true;
            CalmDown = 0;
        }

        else if (allow == false && CalmDown <= 100)
        {
            CalmDown++;
        }
    }

}
