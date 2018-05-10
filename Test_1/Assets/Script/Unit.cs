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

    public int[] bulletNumInBag;
    public int currentBulletNum;
    public Raven_Weapon currentWeapon;

    private void Start()
    {
        /******************************************/
        bulletNumInBag = new int[3];
        bulletNumInBag[0] = -1;
        bulletNumInBag[1] = 0;
        bulletNumInBag[2] = 0;
        /******************************************/

        weapon = gameObject.GetComponent<Weapon>();

        ChangeWeapon(0);
    }


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

    public void ChangeWeapon(Raven_Weapon weaponID)
    {
        Weapon weapon = gameObject.GetComponent<Weapon>();
        weapon.ChooseWeapon(weaponID);

        ProjectileSpeed = weapon.ProjectileSpeed;

        currentBulletNum = bulletNumInBag[(int)weaponID];
        currentWeapon = weaponID;
    }

}
