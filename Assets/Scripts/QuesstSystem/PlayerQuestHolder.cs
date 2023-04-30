using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestHolder : MonoBehaviour
{
  public Quest quest;

  private void Update()
  {
    if (quest == null) return;
    if (quest.Goal.IsReached()) quest = null;
  }
}
