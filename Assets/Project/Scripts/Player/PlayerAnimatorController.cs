using System;
using Project.Scripts.Player;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    [SerializeField] private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerJumpController _jumpController;
    private PlayerGroundChecker _groundChecker;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _jumpController = GetComponent<PlayerJumpController>();
        _groundChecker = GetComponent<PlayerGroundChecker>();

        _jumpController.OnJump += PlayJumpAnimation;
    }

    private void Update()
    {
        _animator.SetBool(IsRunning, _playerMovement.IsMoving);
        _animator.SetBool(IsGrounded, _groundChecker.IsGrounded);
    }

    private void PlayJumpAnimation()
    {
        _animator.Play("Jump");
    }

    private void OnDisable()
    {
        _jumpController.OnJump -= PlayJumpAnimation;
    }
}
