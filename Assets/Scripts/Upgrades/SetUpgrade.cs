using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] class SetUpgrade : Upgrade
{
    [SerializeField] private GameObject[] ToActive;
    [SerializeField] private GameObject[] ToDisable;

    [SerializeField] private UnityEvent _toDo;
    
    public override void Do()
    {
        if (ToActive != null)
        {
            for (int i = 0; i < ToActive.Length; i++)
            {
                var toActive = ToActive[i];
                if (toActive)
                    toActive.SetActive(true);
            }    
        }

        if (ToDisable != null)
        {
            for (int i = 0; i < ToDisable.Length; i++)
            {
                var toDisable = ToDisable[i];
                if (toDisable)
                    toDisable.SetActive(false);
            }    
        }
        
        _toDo?.Invoke();
    }
}
