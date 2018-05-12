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

    private float distance = 0;

    private NavMeshAgent Nma;

    /// <summary>
    /// MoveToState 构造方法
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="target"></param>
    public MoveToState(GameObject npc, GameObject target)
    {
        stateID = StateID.MoveTo;
        this.npc = npc;

        Nma = npc.GetComponent<NavMeshAgent>();
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
        target = controller.MoveTarget;
        if (target != null && target.layer == LayerMask.NameToLayer("AI"))
        {
            distance = Vector3.Distance(target.transform.position, npc.transform.position);
        }


        if (target != null && target.layer == LayerMask.NameToLayer("AI"))
        {
            shootDistinction = target.transform.position - npc.transform.position;
            shootWay = new Ray(npc.transform.position, shootDistinction);
            RaycastHit hit;

            if (distance <= 20 && !Physics.Raycast(shootWay, out hit, distance, 1 << LayerMask.NameToLayer("Wall")))
            {
                Nma.ResetPath();
                return Transition.ReadyToAttack;
            }
        }

        if (target == null)
        {
            return Transition.LostEnemy;
        }
        
        return Transition.NullTransition;
    }

    public override void DoUpdate(GameObject npc, GameObject target)
    {
        this.target = controller.MoveTarget;
        Nma.SetDestination(this.target.transform.position);
    }
}
