using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    
    //field for player

    public void AcceptQuest()
    {
        quest.isActive = true;
        //Assign quest to player
        //player.quest = quest;
    }
}
