using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void WalkingAnimation(bool state)
    {
        _animator.SetBool("IsWalking", state);
    }

    public void PickingUpAnimation()
    {
        _animator.SetTrigger("Pickup");
    }
}
