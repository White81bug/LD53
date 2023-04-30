using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Debug.Log("animator enabled");
    }

    private void OnEnable()
    {
        ScriptManager.Instance.PlayerCollect.OnTakeObject.AddListener(PickingUpAnimation);
    }

    private void OnDisable()
    {
        ScriptManager.Instance.PlayerCollect.OnTakeObject.RemoveListener(PickingUpAnimation);
    }

    public void WalkingAnimation(bool state)
    {
        _animator.SetBool("IsWalking", state);
    }

    public void PickingUpAnimation(GameObject _)
    {
        Debug.Log("hi");
        _animator.SetTrigger("Pickup");
    }
}
