using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/Behaviour")]
public class AgentBehaviourState : AgentState
{
    public override AgentState RunCurrentState(Agent agent)
    {
        foreach (State state in agent.currentState.stateMachine)
        {
            bool AllTrue = true;
            for (int i = 0; i < state.transitions.Length; i++)
            {
                bool ConditionSucceded = state.transitions[i].condition.ConditionsCheck(agent);
                if (state.transitions[i].FalseRequirment)
                {
                    ConditionSucceded = !ConditionSucceded;
                }
                if (!ConditionSucceded)
                {
                    AllTrue = false;
                }
            }
            if (AllTrue)
            {
                return state.NextState;
            }
        }
        return this;
    }
}
