using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRocketBullet : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Unit u = other.GetComponent<Unit>();
        if (u != null)
        {
            u.bulletNumInBag[2] += 3;
        }

        else
            return;
    }
    
}
