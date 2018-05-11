using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState {

    public GameObject npc;
    public Weapon npc_weapon;
    public Unit npc_unit;

    private float ShootCD = 0;

    public AttackState(GameObject npc)
    {
        this.npc = npc;

        npc_weapon = npc.GetComponent<Weapon>();
        npc_unit = npc.GetComponent<Unit>();
    }


    public override void DoBeforeEntering()
    {
        
    }

    public override void DoBeforeLeaving()
    {
        
    }

    public override Transition CheckTranstition()
    {
        return Transition.NullTransition;
    }

    public override void DoUpdate(GameObject npc, GameObject target)
    {
        ShootCD++;
        if (ShootCD > npc_unit.ProjectileSpeed)
        {
            if (npc_unit.currentBulletNum > 0)
                npc_unit.currentBulletNum--;
            else if (npc_unit.currentBulletNum != -1)
                return;
            npc_weapon.Shoot(npc_unit.currentWeapon);
            ShootCD = 0;
        }
    }

}
