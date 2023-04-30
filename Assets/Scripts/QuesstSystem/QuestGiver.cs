using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerQuestHolder player;

    [Header("Physics")]
    private Collider _collider;
    private bool _inTrigger;

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
        Debug.Log("Accepted");
        quest.isActive = true;
        player.quest = quest;
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
