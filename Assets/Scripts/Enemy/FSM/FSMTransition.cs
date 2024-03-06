using System;

[Serializable]
public class FSMTransition
{
    public FSMDecision Decision; // player In rangeAttack -> true or false
    public string TrueState; // current state to attackstate
    public string FalseState; // current state to patrol state
}
