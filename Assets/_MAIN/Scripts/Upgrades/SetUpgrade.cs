using System;
using UnityEngine;

[Serializable] class SetUpgrade : Upgrade
{
    [SerializeField] private GameObject[] ToActive;
    [SerializeField] private GameObject[] ToDisable;

    public override void Do()
    {
        foreach (GameObject go in ToActive)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in ToDisable)
        {
            go.SetActive(false);
        }
    }
}
