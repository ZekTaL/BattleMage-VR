using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public string Name;
    public Condition condition;
    [Tooltip("If this is checked, then the condition needs to return false")]
    public bool FalseRequirment = false;

}
