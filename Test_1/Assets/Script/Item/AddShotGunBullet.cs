using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShotGunBullet : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Unit u = other.GetComponent<Unit>();
        if (u != null)
        {
            u.bulletNumInBag[1] += 10;
        }

        else
            return;
    }
}
