using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentState : ScriptableObject
{
    public abstract AgentState RunCurrentState(Agent agent);
    [System.Serializable]
    public struct State
    {
        public string StateName;
        public AgentState NextState;
        public Transition[] transitions;
    }
    public State[] stateMachine;
    public struct Variables
    {
        public float MoveSpeed, Gravity;
        public int MaxHealth;
    }
}
