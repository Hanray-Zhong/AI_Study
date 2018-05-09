using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToState : FSMState {

    public GameObject target;
    public GameObject npc;



    private NavMeshAgent Nma;                   //定义NMA

    /// <summary>
    /// MoveToState 构造方法
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="target"></param>
    public MoveToState(GameObject npc, GameObject target)
    {
        stateID = StateID.MoveTo;
        this.npc = npc;
        this.target = target;

        Nma = npc.GetComponent<NavMeshAgent>();
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

    public override void DoUpdate()
    {
        Nma.SetDestination(target.transform.position);
    }
}
