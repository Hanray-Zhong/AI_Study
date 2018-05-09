using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : FSMState {

    public GameObject[] wanderPoints;
    public GameObject npc;
    public GameObject wanderPoint;

    private NavMeshAgent Nma;                   //定义NMA

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
