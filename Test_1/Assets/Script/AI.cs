using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AI : MonoBehaviour {

    public Weapon weapon;
    public float ProjectileSpeed;
    public float ShootCD = 0;

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

    private void Update()
    {
        ShootCD++;

        if (Input.GetKey(KeyCode.Space) && ShootCD > ProjectileSpeed)
        {
            if (currentBulletNum > 0)
                currentBulletNum--;
            else if(currentBulletNum != -1)
                return;
            weapon.Shoot(currentWeapon);
            ShootCD = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
            ChangeWeapon(Raven_Weapon.Blaster);
        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
            ChangeWeapon(Raven_Weapon.ShotGun);
        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
            ChangeWeapon(Raven_Weapon.RocketLuncher);
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
