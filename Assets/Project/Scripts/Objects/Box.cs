using UnityEngine;

namespace Project.Scripts.Objects
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private float _activeScale;
        [SerializeField] private GameObject _mesh;
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;
            
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x, _activeScale, gameObject.transform.localScale.z);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;
            
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x, 1, gameObject.transform.localScale.z);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;
            
            _mesh.SetActive(false);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;
            
            _mesh.SetActive(true);
        }
    }
}
