using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBullet : MonoBehaviour {
    private float damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Unit u = other.GetComponent<Unit>();
        if (u != null)
        {
            u.ApplyDamage(damage);
        }

        else
            return;
    }

    private void FixedUpdate()
    {
        GameObject.Destroy(gameObject, 2);
    }
}
