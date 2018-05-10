using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToState : FSMState {

    public GameObject target;
    public GameObject npc;

    private Ray shootWay;
    private Vector3 shootDistinction;
    private StateController controller;



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
        controller = npc.GetComponent<StateController>();
    }

    public override void DoBeforeEntering()
    {
        
    }

    public override void DoBeforeLeaving()
    {
        controller.DeleteState(StateID.MoveTo);
    }

    public override Transition CheckTranstition()
    {
        float distance = Vector3.Distance(target.transform.position, npc.transform.position);
        
        
        if (target.layer == 1 << LayerMask.NameToLayer("AI"))
        {
            shootDistinction = target.transform.position - npc.transform.position;
            shootWay = new Ray(npc.transform.position, shootDistinction);
            RaycastHit hit;
            if (distance <= 10 || !Physics.Raycast(shootWay, out hit, distance, 1 << LayerMask.NameToLayer("wall")))
            {
                npc.transform.LookAt(target.transform.position);
                return Transition.ReadyToAttack;
            }
        }

        if (target == null)
        {
            return Transition.LostEnemy;
        }
        

        return Transition.NullTransition;
    }

    public override void DoUpdate()
    {
        Nma.SetDestination(target.transform.position);
    }
}
