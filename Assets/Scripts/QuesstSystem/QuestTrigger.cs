using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestTrigger : MonoBehaviour
{
    public PlayerQuestHolder player;
    [Tooltip("Must match quest key")]
    public string triggerKey;

    [Header("Physics")]
    private Collider _collider;
    [SerializeField] private bool _inTrigger;

    private InputActions interactActions;
    private PlayerInput _input;

    private bool _interacted;

    protected virtual void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        interactActions = new InputActions();
        interactActions.Enable();
        interactActions.Player.Interact.performed += AdvanceQuest;
        interactActions.Disable();
    }
    
    public void AdvanceQuest()
    {
        if (_interacted) return;
        if (!_inTrigger) return;
        if (player.quest == null) return;
        if (player.quest.Goal.GoalType.ToString() != InteractableType.ToString()) return;
        if(player.quest.questKey != triggerKey) return;
        Debug.Log("Quest Advanced");
        player.quest.Goal.StageCompleted();
        _interacted = true;
    }

    private void AdvanceQuest(InputAction.CallbackContext ctx)
    {
        if (triggerKey == "Rocket" & CollectingZone.CanFlyOut)
        {
            GameManager.Instance.SetStatement(2);
        }

        if (_interacted) return;
        if(!_inTrigger) return;
        if(player.quest == null) return;
        if(player.quest.Goal.GoalType == GoalType.Gather) return;
        if(player.quest.Goal.GoalType.ToString() != InteractableType.ToString()) return;
        Debug.Log("Quest Advanced");
        player.quest.Goal.StageCompleted();
        _interacted = true;
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

   public InteractableType InteractableType;
}
public enum InteractableType
{
    Interact,
    Gather
}
