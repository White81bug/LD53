using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestHolder : MonoBehaviour
{
  public Quest quest;
  public bool canPickUp;

  private void Update()
  {
    if (quest == null) return;
    canPickUp = quest.Goal.GoalType == GoalType.Gather;
    if (quest.Goal.IsReached()) quest = null;
  }
}
