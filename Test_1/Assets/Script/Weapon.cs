using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Raven_Weapon
{
    Blaster = 0,
    ShotGun,
    RocketLuncher,
}

public class Weapon : MonoBehaviour {

    public float RateOfFire;                    //武器攻击范围
    public float ProjectileSpeed;               //武器射击速度
    public GameObject Bullet;                   //武器的子弹物件
    public Transform ShootPoint;
    public float ShootForce;
    public float Radius;
    public Raven_Weapon currentWeapon;


    public void ChooseWeapon(Raven_Weapon weaponID)
    {
        if (weaponID == Raven_Weapon.Blaster)
        {
            RateOfFire = 5;
            ProjectileSpeed = 10;
            Bullet = (GameObject)Resources.Load("Prefabs/BlasterBullet", typeof(GameObject));
            currentWeapon = Raven_Weapon.Blaster;
        }
        if (weaponID == Raven_Weapon.ShotGun)
        {
            RateOfFire = 5;
            ProjectileSpeed = 30;
            Bullet = (GameObject)Resources.Load("Prefabs/ShotGunBullet", typeof(GameObject));
            currentWeapon = Raven_Weapon.ShotGun;
        }
        if (weaponID == Raven_Weapon.RocketLuncher)
        {
            RateOfFire = 10;
            ProjectileSpeed = 45;
            Bullet = (GameObject)Resources.Load("Prefabs/RocketLuncherBullet", typeof(GameObject));
            currentWeapon = Raven_Weapon.RocketLuncher;
        }
    }

    public void Shoot(Raven_Weapon weapon)
    {
        if (weapon == Raven_Weapon.Blaster)
        {
            GameObject newBullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation) as GameObject;
            Vector3 shootDirection = ShootPoint.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(shootDirection * ShootForce, ForceMode.Impulse);
            return;
        }

        if (weapon == Raven_Weapon.ShotGun)
        {
            Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            Collider[] cols = Physics.OverlapSphere(ShootPoint.position, Radius, 1 << 8);
            if (cols.Length != 0)
            {
                foreach (var item in cols)
                {
                    Rigidbody r = item.GetComponent<Rigidbody>();
                    if (r != null)
                        r.AddForce(r.transform.forward * ShootForce, ForceMode.Impulse);
                }
            }
            return;
        }

        if (weapon == Raven_Weapon.RocketLuncher)
        {
            GameObject newBullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation) as GameObject;
            Vector3 shootDirection = ShootPoint.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(shootDirection * 50, ForceMode.Impulse);
            return;
        }
    }
}
