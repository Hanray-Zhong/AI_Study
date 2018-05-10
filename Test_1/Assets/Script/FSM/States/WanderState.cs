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
    private float sightRange = 6;
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
            MoveToState moveToState_enemy = new MoveToState(npc, enemy);
            moveToState_enemy.AddTransition(Transition.SawEnemy, StateID.MoveTo);
            controller.AddState(moveToState_enemy);
            return Transition.SawEnemy;
        }

        Collider[] colsOfItem = Physics.OverlapSphere(npc.transform.position, sightRange, 1 << LayerMask.NameToLayer("Item"));
        if (colsOfItem.Length != 0)
        {
            MoveToState moveToState_Item = new MoveToState(npc, colsOfItem[0].gameObject);
            moveToState_Item.AddTransition(Transition.SawItem, StateID.MoveTo);
            controller.AddState(moveToState_Item);
            return Transition.SawItem;
        }

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
