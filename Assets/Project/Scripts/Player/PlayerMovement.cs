using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private UnityEngine.Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
        }
        
        private void Update()
        {
            Move(InputMoveDirection());
            Rotate(InputRotation());
        }

        private void Move(Vector3 direction)
        {
            _rb.velocity = direction * _speed + new Vector3(0, _rb.velocity.y, 0);
        }

        private Vector3 InputMoveDirection()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                var cameraForward = new Vector3(0, 0, 0);
                var cameraRight = new Vector3(0, 0, 0);

                if (_mainCamera)
                {
                    cameraForward = _mainCamera.transform.forward;
                    cameraRight = _mainCamera.transform.right;
                
                    cameraForward.y = 0;
                    cameraRight.y = 0;
                    
                    cameraForward.Normalize();
                    cameraRight.Normalize();
                }
                
                return cameraForward * vertical + cameraRight * horizontal;
            }
            else
            {
                return new Vector3(horizontal, 0, vertical);
            }
        }

        private void Rotate(Quaternion targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        
        private Quaternion InputRotation()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                var direction = new Vector3(horizontal, 0, vertical).normalized;
                var addictionCamera = Quaternion.Euler(0,_mainCamera ? _mainCamera.transform.rotation.eulerAngles.y + 180 : 0,0);
                
                return Quaternion.LookRotation(direction) * addictionCamera;
            }

            return transform.rotation;
        }
    }
}
