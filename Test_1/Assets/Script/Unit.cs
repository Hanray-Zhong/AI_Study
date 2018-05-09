using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float health = 10;
    public GameObject DeadEffect;

    private bool isDead = false;

    public void ApplyDamage(float damage)
    {
        if (damage < health)
        {
            health -= damage;
        }

        else
        {
            isDead = true;
            health = 0;
            if (isDead)
            {
                Destruct();
            }
        }
    }

    private void Destruct()
    {
        GameObject dead = Instantiate(DeadEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(dead, 3);
        isDead = false;
    }

}
