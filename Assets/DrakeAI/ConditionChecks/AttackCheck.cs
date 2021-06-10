using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("AI/Conditions/AttackCheck"))]

public class AttackCheck : Condition
{
    public override bool ConditionsCheck(Agent agent)
    {
        return false;
    }
}
