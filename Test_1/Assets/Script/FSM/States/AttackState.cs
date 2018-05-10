using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState {

    public GameObject npc;

    public AttackState(GameObject npc)
    {
        this.npc = npc; 
    }


    public override void DoBeforeEntering()
    {
        
    }

    public override void DoBeforeLeaving()
    {
        
    }

    public override Transition CheckTranstition()
    {
        throw new System.NotImplementedException();
    }

    public override void DoUpdate()
    {
        throw new System.NotImplementedException();
    }

}
