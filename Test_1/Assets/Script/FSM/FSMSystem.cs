using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 维护一个状态的字典
/// </summary>
public class FSMSystem : MonoBehaviour {
    
    /// <summary>
    ///管理状态的字典 
    /// </summary>
    private Dictionary<StateID, FSMState> states;
    // 只能通过 trans 参数来改变 state ，不能直接改变

    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }

    public FSMSystem()
    {
        states = new Dictionary<StateID, FSMState>();
    }

    /// <summary>  
    /// 将新的状态加入FSM  
    /// </summary>  
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("FSM ADD ERROR: " + state + " is null");
            return;
        }


        //检测列表中是否存在
        if (states.ContainsKey(state.ID))
        {
            Debug.LogError("FSM ADD ERROR: The " + state + " is exist");
            return;
        }

        states.Add(state.ID, state);
        Debug.Log("add state " + state + " successfully");
    }

    /// <summary>
    /// 删除FSM中的状态
    /// </summary>
    /// <param name="id"></param>
    public void DeleteState(StateID id)
    {
        if (id == StateID.NullState)
        {
            Debug.LogError("FSM DELETE EROOR: The state you want to delete is null.");
            return;
        }

        if (!states.ContainsKey(id))
        {
            Debug.LogError("FSM DELETE ERROR: The state " + id + " you want to delete is not exit."); return;
        }

        states.Remove(id);
    }

    
    /// <summary>
    /// 第一次启动状态机
    /// </summary>
    /// <param name="id"></param>
    public void start(StateID id)
    {
        FSMState state;
        bool isGet = states.TryGetValue(id, out state);
        if (isGet)
        {
            state.DoBeforeEntering();
            currentState = state;
        }
        else
        {
            Debug.LogError("FSM START ERROR: The state " + id + " is not exit in the fsm.");
        }
    }

    /// <summary>
    /// 基于转换参数，改变现在的状态
    /// </summary>
    /// <param name="trans"></param>
    public void PerformTransition(Transition trans)
    {
        // 根绝当前的状态类，以Trans为参数调用它的 GetOutputState 方法  
        StateID id = currentState.GetNextState(trans);

        // 更新现在的状态         
        currentStateID = id;
        FSMState fs;
        if (states.TryGetValue(currentStateID, out fs))
        {
            currentState.DoBeforeLeaving();
            currentState = fs;
            currentState.DoBeforeEntering();
        }
        else
        {
            Debug.LogError("FSM CHANGE ERROR: The FsmSystem is not contain the FsmState.");
            return;
        }
    }

    /// <summary>
    /// 由FSMSystem控制的 update() 检测状态是否需要转变
    /// </summary>
    public void DoUpdate(GameObject npc, GameObject target)
    {
        Transition trans = currentState.CheckTranstition();
        if (trans != Transition.NullTransition)
            PerformTransition(trans);
        currentState.DoUpdate(npc, target);
    }
}
