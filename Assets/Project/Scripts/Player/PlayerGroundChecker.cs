using System;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.Player
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        [SerializeField] private PlayerJumpController _jumpController;

        public bool IsGrounded { get; private set; } = false;

        public UnityAction OnGrounded;

        private int _currentCountOfTriggers;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Ground>()) return;
            _currentCountOfTriggers++;

            if (_currentCountOfTriggers == 1)
            {
                OnGrounded?.Invoke();
                IsGrounded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.GetComponent<Ground>()) return;
            _currentCountOfTriggers--;

            if (_currentCountOfTriggers == 0)
            {
                IsGrounded = false;
            }
        }

        private void OnPlayerJump()
        {
            IsGrounded = false;
        }

        private void OnEnable()
        {
            _jumpController.OnJump += OnPlayerJump;
        }
        
        private void OnDisable()
        {
            _jumpController.OnJump -= OnPlayerJump;
        }
    }
}
