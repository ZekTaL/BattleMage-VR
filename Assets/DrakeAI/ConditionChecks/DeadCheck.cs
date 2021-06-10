using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("AI/Conditions/DeadCheck"))]

public class DeadCheck : Condition
{
    public override bool ConditionsCheck(Agent agent)
    {
        if (agent.IsAlive)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    
}
