using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机的控制器
/// </summary>
public class StateController : FSMSystem {

    public FSMSystem FSM;
    public GameObject MoveTarget = null;
    public GameObject[] wanderPoints;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        FSM.DoUpdate(gameObject, MoveTarget);
        Debug.Log("Current State is: " + FSM.CurrentState);


    }

    /// <summary>
    /// 初始化状态机
    /// </summary>
    public void Init()
    {
        ConstructFSM();
    }

    /// <summary>
    /// 初始化状态机时，构造一个状态机
    /// </summary>
    void ConstructFSM()
    {
        FSM = new FSMSystem();

        AttackState attackState = new AttackState(gameObject);
        attackState.AddTransition(Transition.ReadyToAttack, StateID.Attack);

        MoveToState moveToState = new MoveToState(gameObject, MoveTarget);
        moveToState.AddTransition(Transition.SawEnemy, StateID.MoveTo);
        moveToState.AddTransition(Transition.SawItem, StateID.MoveTo);

        WanderState wanderState = new WanderState(gameObject, wanderPoints);
        wanderState.AddTransition(Transition.LostEnemy, StateID.Wander);


        FSM.AddState(attackState);
        FSM.AddState(wanderState);
        FSM.AddState(moveToState);


        FSM.start(StateID.Attack);
    }
}
