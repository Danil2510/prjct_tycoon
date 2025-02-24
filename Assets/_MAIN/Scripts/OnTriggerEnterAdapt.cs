using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterAdapt : MonoBehaviour
{
    [SerializeField]UnityEvent _actionCompleted;

    private void OnTriggerEnter(Collider other)
    {
        _actionCompleted?.Invoke();
    }
}
