using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 维护一个状态的列表
/// </summary>
public class FSMSystem : MonoBehaviour {
    
    /// <summary>
    ///管理状态的列表 
    /// </summary>
    private List<FSMState> states;
    // 只能通过 trans 参数来改变 state ，不能直接改变
    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }

    public FSMSystem()
    {
        states = new List<FSMState>();
    }

    /// <summary>  
    /// 将新的状态加入FSM  
    /// </summary>  
    public void AddState(FSMState s)
    {
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        // 加入第一个状态时，如果列表时空的，加入列表并返回
        if (states.Count == 0)
        {
            states.Add(s);
            currentState = s;
            currentStateID = s.ID;
            return;
        }
        
        //检测列表中是否存在
        foreach (FSMState state in states)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: Impossible to add state because state has already been added");
                return;
            }
        }

        states.Add(s);
    }

    public void DeleteState(StateID id)
    {
        // 找到对应的状态并删除 
        foreach (FSMState state in states)
        {
            if (state.ID == id)
            {
                states.Remove(state);
                return;
            }
        }

        // 找不到对应的状态
        Debug.LogError("FSM ERROR: Impossible to delete state. It was not on the list of states");
    }

    /// <summary>
    /// 第一次启动状态机
    /// </summary>
    /// <param name="id"></param>
    public void start(StateID id)
    {
        foreach (FSMState state in states)
        {
            if (state.ID == id)
            {
                currentState = state;
                currentState.DoBeforeEntering();
                return;
            }
        }

        Debug.LogError("The state " + id + " is not exit in the fsm.");
    }


    /// <summary>
    /// 基于转换参数，改变现在的状态
    /// </summary>
    /// <param name="trans"></param>
    public void PerformTransition(Transition trans)
    {
        // 根绝当前的状态类，以Trans为参数调用它的 GetOutputState 方法  
        StateID id = currentState.GetOutputState(trans);

        // 更新现在的状态         
        currentStateID = id;
        foreach (FSMState state in states)
        {
            if (state.ID == currentStateID)
            {
                // 先做离开这个状态前要做的  
                currentState.DoBeforeLeaving();

                // 改变状态
                currentState = state;

                // 进入了新状态，开始做进入状态前要做的
                currentState.DoBeforeEntering();
                break;
            }
        }
    }

    /// <summary>
    /// 由FSMSystem控制的 update() 要做的事
    /// </summary>
    public void DoUpdate()
    {
        Transition trans = currentState.CheckTranstition();
        if (trans != Transition.NullTransition)
            PerformTransition(trans);
        currentState.DoUpdate();
    }
}
