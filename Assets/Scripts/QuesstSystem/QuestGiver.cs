using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private bool questGiven = false;
    public Quest quest;
    public PlayerQuestHolder player;

   
    private Collider _collider;
   [SerializeField] private bool _inTrigger;

    private InputActions interactActions;
    private PlayerInput _input;

    protected virtual void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        interactActions = new InputActions();
        interactActions.Enable();
       interactActions.Player.Interact.performed += AcceptQuest;
       interactActions.Disable();
      
    }
    
    private void AcceptQuest(InputAction.CallbackContext ctx)
    {
        if(!_inTrigger) return;
        if(player.quest != null) {Debug.Log("Already has quest"); return;}
        if(questGiven) return;
        Debug.Log("Accepted");
        quest.isActive = true;
        player.quest = quest;
        questGiven = true;
        UIManager.Instance.ShowDialogueLine(quest.description);
    }

    public void ForceQUest()
    {
        if(questGiven) return;
        quest.isActive = true;
        player.quest = quest;
        questGiven = true;
        UIManager.Instance.ShowDialogueLine(quest.description);
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerQuestHolder>())
        {
            player = other.GetComponent<PlayerQuestHolder>();
            _inTrigger = true;
            interactActions.Enable();
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerQuestHolder>())
        {
            _inTrigger = false;
            interactActions.Disable();
        }
    }

}
