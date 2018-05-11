using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 转换
/// </summary>
public enum Transition
{
    NullTransition = 0,
    SawEnemy,
    SawItem,
    ReadyToAttack,
    LostEnemy,
}

/// <summary>
/// 状态
/// </summary>
public enum StateID
{
    NullState = 0,
    Wander,
    MoveTo,
    Attack,
}

/// <summary>
/// 管理状态的类
/// </summary>
public abstract class FSMState {

    protected Dictionary<Transition, StateID> transitions;

    protected StateID stateID;
    public StateID ID { get { return stateID; } }


    public FSMState()
    {
        transitions = new Dictionary<Transition, StateID>();
    }

    /// <summary>
    /// 向字典中添加项，每一个转换(trans)对应一个状态(id)
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="id"></param>
    public void AddTransition(Transition trans, StateID id)
    {
        //检测是否已经在 Map 中
        if (transitions.ContainsKey(trans))
        {
            Debug.LogError("FSMState ERROR: " + trans + " is already inside the transitions");
            return;
        }
        else
        {
            transitions.Add(trans, id);
            Debug.Log("add transition " + trans + " successfully " + " " + transitions.ContainsKey(trans));
        }
    }

    /// <summary>
    /// 删除字典中的项
    /// </summary>
    /// <param name="trans"></param>
    public void DeleteTransition(Transition trans)
    {
        // 检查是否在字典中，如果在，移除
        if (transitions.ContainsKey(trans))
        {
            transitions.Remove(trans);
            return;
        }
        //如果要删除的项不在字典中，报告错误
        Debug.LogError("FSMState ERROR: Transition passed was not on this State List");
    }

    /// <summary>
    /// 查询字典，确定在当前状态下，发生转换时，转换到新的状态编号并返回
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public StateID GetNextState(Transition trans)
    {
        return transitions[trans];
    }

    /// <summary>
    /// 进入状态前做
    /// </summary>
    public virtual void DoBeforeEntering() { }

    /// <summary>
    /// 离开状态前做
    /// </summary>
    public virtual void DoBeforeLeaving() { }

    /// <summary>
    /// 确定要转换时转化的状态
    /// </summary>
    /// <returns></returns>
    public abstract Transition CheckTranstition();

    /// <summary>
    /// 在 update 中这个状态要怎么做
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="target"></param>
    public abstract void DoUpdate(GameObject npc, GameObject target);
}
