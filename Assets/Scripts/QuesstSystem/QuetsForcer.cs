using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetsForcer : MonoBehaviour
{
    private QuestGiver _questGiver;

    private void Awake()
    {
        _questGiver = GetComponent<QuestGiver>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.GetComponent<PlayerQuestHolder>()) _questGiver.ForceQUest();
    }
}
