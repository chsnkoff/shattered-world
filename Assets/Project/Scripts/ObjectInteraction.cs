using JetBrains.Annotations;
using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ObjectInteraction : MonoBehaviour
{
    public static readonly UnityEvent OnEnter = new();
    public static readonly UnityEvent OnExit = new();
    
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
        if (!other.GetComponent<Player>()) return;
        OnEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Player>()) return;
        OnExit?.Invoke();
    }

    private void Interact()
    {
        _onInteract?.Invoke();
    }
}
