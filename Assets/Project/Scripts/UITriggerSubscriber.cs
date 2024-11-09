using TMPro;
using UnityEngine;

public class UITriggerSubscriber : MonoBehaviour
{
    public static bool IsUIActivated;
    [SerializeField] private TextMeshProUGUI _textMesh;
    private int _triggersCounter;

    private void OnEnable()
    {
        ObjectInteraction.OnEnter.AddListener(OnEnter);
        ObjectInteraction.OnExit.AddListener(OnExit);
    }

    private void OnEnter()
    {
        _triggersCounter++;
        _textMesh.enabled = true;
        IsUIActivated = true;
    }

    private void OnExit()
    {
        _triggersCounter--;
        if (_triggersCounter != 0) return;
        _textMesh.enabled = false;
        IsUIActivated = false;
    }
}
