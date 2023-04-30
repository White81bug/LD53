using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType GoalType;
    
    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void StageCompleted()
    {
        currentAmount++;
    }
}

public enum GoalType
{
    Interact,
    Gather
}
