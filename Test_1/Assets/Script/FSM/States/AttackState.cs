using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState {

    public GameObject npc;
    public Weapon npc_weapon;
    public Unit npc_unit;

    private float ShootCD = 0;
    private StateController controller;

    /// <summary>
    /// 攻击状态的构造方法
    /// </summary>
    /// <param name="npc"></param>
    public AttackState(GameObject npc)
    {
        stateID = StateID.Attack;
        this.npc = npc;

        npc_weapon = npc.GetComponent<Weapon>();
        npc_unit = npc.GetComponent<Unit>();
        controller = npc.GetComponent<StateController>();
    }


    public override void DoBeforeEntering()
    {
        
    }

    public override void DoBeforeLeaving()
    {
        
    }

    public override Transition CheckTranstition()
    {
        if(controller.MoveTarget == null)
        {
            return Transition.LostEnemy;
        }
        return Transition.NullTransition;
    }

    public override void DoUpdate(GameObject npc, GameObject target)
    {
        ShootCD++;
        npc.transform.LookAt(target.transform.position);
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
