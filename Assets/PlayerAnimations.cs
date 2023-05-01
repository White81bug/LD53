using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        ScriptManager.Instance.MovementControllerRedone.CanMove = false;
        _animator.SetBool("IsPickuping", true);
        StartCoroutine(PickingUp());
    }

    private IEnumerator PickingUp()
    {
        yield return new WaitForSeconds(3.33f); // CONST
        Debug.Log("end of wait");
        _animator.SetBool("IsPickuping", false);
        ScriptManager.Instance.MovementControllerRedone.CanMove = true;
    }
}
