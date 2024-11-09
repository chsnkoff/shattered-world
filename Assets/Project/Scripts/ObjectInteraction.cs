using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ObjectInteraction : MonoBehaviour
{
    public static UnityEvent OnEnter = new();
    public static UnityEvent OnExit = new();
    
    [SerializeField] private UnityEvent _onInteract;
    
    private void Awake()
    {
        InitTrigger();
    }

    private void Update()
    {
        if (UITriggerSubscriber.IsUIActivated && Input.GetKeyDown(KeyCode.E)) Interact();
    }

    private void InitTrigger()
    {
        var triggerCollider = GetComponent<BoxCollider>();
        if (!triggerCollider) triggerCollider = gameObject.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player entered trigger");
        OnEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player exited trigger");
        OnExit?.Invoke();
    }

    private void Interact()
    {
        _onInteract?.Invoke();
        Debug.Log("Player interacted");
    }
}
