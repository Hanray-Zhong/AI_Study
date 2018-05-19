using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Unit u = other.GetComponent<Unit>();
        if (u != null)
        {
            u.health = 10;
        }

        else
            return;
    }
    
}
