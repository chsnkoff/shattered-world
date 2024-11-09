using System;
using Project.Scripts.Camera;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.Player
{
    public class PlayerJumpController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private PlayerGroundChecker _groundChecker;
        [SerializeField] private PlayerDebuffManager _debuffManager;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;
        [SerializeField] private float _forcedFallForce;
        [SerializeField] private float _comboDelay;
        [SerializeField] private CameraShakeEffectConfig _defaultJumpCameraShakeEffectConfig;
        [SerializeField] private CameraShakeEffectConfig _forcedJumpCameraShakeEffectConfig;
        
        private bool _forcedFall;
        private bool _forcedFallSpaceComboOpen;
        private bool _forcedFallShiftComboOpen;

        private bool _doubleJump;
        
        public bool IsGrounded => _groundChecker.IsGrounded;
        public UnityAction OnJump;

        private void Update()
        {
            ApplyAcceleration();
            
            if (Input.GetKeyDown(KeyCode.Space) && _groundChecker.IsGrounded)
            {
                Jump();
                return;
            }

            if (_groundChecker.IsGrounded) return;
            switch (_forcedFall)
            {
                case false when !_doubleJump && Input.GetKeyDown(KeyCode.Space) && !_forcedFallShiftComboOpen:
                    Jump();
                    
                    _doubleJump = true;

                    return;
                case false when IsForcedFallCombo():
                    ForceFall();
                    break;
            }
        }

        private void Jump()
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(Vector3.up * (_jumpForce * _debuffManager.GetDebuffsValueByType(Debuff.DebuffType.JumpForce)), ForceMode.Impulse);
                
            OnJump?.Invoke();
        }

        private void ForceFall()
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(Vector3.down * (_forcedFallForce * _debuffManager.GetDebuffsValueByType(Debuff.DebuffType.JumpForce)), ForceMode.Impulse);
            
            _forcedFall = true;
        }

        private void ApplyAcceleration()
        {
            _rb.AddForce(Vector3.up * (Physics.gravity.y * (_gravityScale - 1) * Time.deltaTime * _debuffManager.GetDebuffsValueByType(Debuff.DebuffType.Gravity)), ForceMode.Acceleration);
        }
        
        private bool IsForcedFallCombo()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_forcedFallSpaceComboOpen)
                {
                    return true;
                }
                
                _forcedFallShiftComboOpen = true;
                Invoke(nameof(ResetForcedFallCombo), _comboDelay);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_forcedFallShiftComboOpen)
                {
                    return true;
                }
                
                _forcedFallSpaceComboOpen = true;
                Invoke(nameof(ResetForcedFallCombo), _comboDelay);
            }

            return false;
        }

        private void ResetForcedFallCombo()
        {
            _forcedFallSpaceComboOpen = false;
            _forcedFallShiftComboOpen = false;
        }

        private void OnGrounded()
        {
            CameraShake.Instance?.ShakeCamera(_forcedFall
                ? _forcedJumpCameraShakeEffectConfig
                : _defaultJumpCameraShakeEffectConfig);
            _forcedFall = false;
            _doubleJump = false;
            ResetForcedFallCombo();
        }

        private void OnEnable()
        {
            _groundChecker.OnGrounded += OnGrounded;
        }

        private void OnDisable()
        {
            _groundChecker.OnGrounded -= OnGrounded;
        }
    }
}
