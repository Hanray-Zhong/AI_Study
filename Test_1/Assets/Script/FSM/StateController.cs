using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机的控制器
/// </summary>
public class StateController : FSMSystem {

    public FSMSystem FSM;
    public GameObject moveTarget;

    public GameObject[] wanderPoints;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        FSM.DoUpdate();
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

        MoveToState moveToState_enemy = new MoveToState(gameObject, moveTarget);
        moveToState_enemy.AddTransition(Transition.SawEnemy, StateID.MoveTo);

        MoveToState moveToState_Item = new MoveToState(gameObject, moveTarget);
        moveToState_enemy.AddTransition(Transition.SawEnemy, StateID.MoveTo);

        AttackState attackState = new AttackState();
        attackState.AddTransition(Transition.ReadyToAttack, StateID.Attack);

        WanderState wanderState = new WanderState(gameObject, wanderPoints);
        wanderState.AddTransition(Transition.LostEnemy, StateID.Wander);

        FSM.AddState(moveToState_enemy);
        FSM.AddState(moveToState_Item);
        FSM.AddState(attackState);
        FSM.AddState(wanderState);

        FSM.start(StateID.Wander);
    }
}
