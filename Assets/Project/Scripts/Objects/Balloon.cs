using UnityEngine;

namespace Project.Scripts.Objects
{
    public class Balloon : MonoBehaviour
    {
        [SerializeField] private GameObject _ball;
        [SerializeField] private Rigidbody _ballRb;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private GameObject _rope;

        public void Activate()
        {
            _ball.transform.SetParent(null);
            _ballRb.useGravity = true;
            _ballRb.AddForce(Vector3.down*_fallSpeed, ForceMode.Impulse);

            _rope.SetActive(false);
        }
    }
}
