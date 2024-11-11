using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void TurnLeverOn()
    {
        _animator.Play("Lever");
    }
}
