using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    /// <summary>
    /// 人物属性
    /// </summary>
	public float health = 10;
    public GameObject DeadEffect;
    private bool isDead = false;

    /// <summary>
    /// 武器属性
    /// </summary>
    public Weapon weapon;
    public float ProjectileSpeed;
    public float ShootCD = 0;

    public int[] bulletNumInBag;
    public int currentBulletNum;
    public Raven_Weapon currentWeapon;


    

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
