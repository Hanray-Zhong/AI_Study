using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : FSMState {

    public GameObject[] wanderPoints;
    public GameObject npc;
    public GameObject wanderPoint;
    public GameObject enemy;

    private NavMeshAgent Nma;                   //定义NMA
    private float sightRange = 23;
    private StateController controller;

    /// <summary>
    /// WanderStatte 构造方法
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="wanderPoints"></param>
    public WanderState(GameObject npc, GameObject[] wanderPoints)
    {
        stateID = StateID.Wander;
        this.npc = npc;
        this.wanderPoints = wanderPoints;

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
        Collider[] cols = Physics.OverlapSphere(npc.transform.position, sightRange, 1 << LayerMask.NameToLayer("AI"));
        bool isOthers = false;
        foreach (var item in cols)
        {
            if (item.gameObject != npc)
            {
                isOthers = true;
                enemy = item.gameObject;
                break;
            }
        }

        if (cols.Length != 0 && isOthers)
        {
            controller.MoveTarget = enemy;
            wanderPoint = null;
            return Transition.SawEnemy;
        }

        Collider[] colsOfItem = Physics.OverlapSphere(npc.transform.position, sightRange, 1 << LayerMask.NameToLayer("Item"));
        if (colsOfItem.Length != 0)
        {
            controller.MoveTarget = colsOfItem[0].gameObject;
            wanderPoint = null;
            return Transition.SawItem;
        }

        return Transition.NullTransition;
    }

    public override void DoUpdate(GameObject npc, GameObject target)
    {
        if(wanderPoint == null)
        {
            int areaNumber = Random.Range(0, wanderPoints.Length);
            wanderPoint = wanderPoints[areaNumber];
            Nma.SetDestination(wanderPoint.transform.position);
        }
        

        float distance = Vector3.Distance(wanderPoint.transform.position, npc.transform.position);

        if (distance < 3)
            wanderPoint = null;
    }
}
