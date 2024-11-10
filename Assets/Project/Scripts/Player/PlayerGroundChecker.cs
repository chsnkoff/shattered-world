using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.Player
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _raycastOrigins;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private float _maxSlopeAngle;

        public bool IsGrounded { get; private set; } = false;
        private float? _minSlopeAngle = null;

        public UnityAction OnGrounded;

        private void Update()
        {
            _minSlopeAngle = null;
            
            foreach (var rayOrigin in _raycastOrigins)
            {
                if (!Physics.Raycast(rayOrigin.transform.position, Vector3.down, out var hit, 0.3f, _groundLayerMask)) continue;
                
                
                var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                _minSlopeAngle = _minSlopeAngle == null || slopeAngle < _minSlopeAngle ? slopeAngle : _minSlopeAngle;
                
                if (IsGrounded) return;
                //if (!IsSlopeAngleAllowed()) return;
                
                IsGrounded = true;
                OnGrounded?.Invoke();
                
                return;
            }
            
            IsGrounded = false;
        }

        public bool IsSlopeAngleAllowed()
        {
            return _minSlopeAngle <= _maxSlopeAngle || _minSlopeAngle == null;
        }
        
    }
}
