using UnityEngine;
using UnityEngine.Events;

public class OnTriggerExitAdapt : MonoBehaviour
{
    [SerializeField]UnityEvent _actionCompleted;

    private void OnTriggerExit(Collider other)
    {
        _actionCompleted?.Invoke();
    }
}
