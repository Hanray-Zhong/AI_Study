using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLuncherBullet : MonoBehaviour {
    public GameObject ExplosionEffect;

    private float damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        
        Collider[] cols = Physics.OverlapSphere(gameObject.transform.position, 3);
        if(cols.Length != 0)
        {
            foreach (var item in cols)
            {
                Unit u = item.GetComponent<Unit>();
                if (u != null)
                {
                    u.ApplyDamage(damage);
                }
            }
        }
        GameObject explosion = Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 2);
    }

    private void FixedUpdate()
    {
        GameObject.Destroy(gameObject, 2);
    }
}
