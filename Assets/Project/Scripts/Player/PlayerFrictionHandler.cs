using UnityEngine;
using UnityEngine.Rendering.UI;

namespace Project.Scripts.Player
{
    public class PlayerFrictionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerJumpController _jumpController;
        [SerializeField] private PlayerGroundChecker _groundChecker;
        [SerializeField] private Collider _collider;

        private void EnableFrictions()
        {
            _collider.material.staticFriction = 1;
            _collider.material.dynamicFriction = 1;
        }
    
        private void DisableFrictions()
        {
            _collider.material.staticFriction = 0;
            _collider.material.dynamicFriction = 0;
        }
    
        private void OnEnable()
        {
            _jumpController.OnJump += DisableFrictions;
            _groundChecker.OnGrounded += EnableFrictions;
        }

        private void OnDisable()
        {
            _jumpController.OnJump -= DisableFrictions;
            _groundChecker.OnGrounded -= EnableFrictions;
        }
    }
}
