using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机的控制器
/// </summary>
public class StateController : MonoBehaviour {

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
        attackState.AddTransition(Transition.LostEnemy, StateID.Wander);

        MoveToState moveToState = new MoveToState(gameObject, MoveTarget);
        moveToState.AddTransition(Transition.ReadyToAttack, StateID.Attack);
        moveToState.AddTransition(Transition.LostEnemy, StateID.Wander);

        WanderState wanderState = new WanderState(gameObject, wanderPoints);
        wanderState.AddTransition(Transition.SawEnemy, StateID.MoveTo);
        wanderState.AddTransition(Transition.SawItem, StateID.MoveTo);



        FSM.AddState(attackState);
        FSM.AddState(wanderState);
        FSM.AddState(moveToState);

        

        FSM.start(StateID.Wander);
    }
}
