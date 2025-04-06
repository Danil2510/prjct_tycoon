using System;
using DefaultNamespace.Analytics;
using Unity.Services.Analytics;
using UnityEngine;

public abstract class Storage : MonoBehaviour
{
    public int Smthng = 0;

    public Action<int> ValueAdded;
    public Action<int> ValueSpended;

    public virtual void EarnSmt(int val)
    {
        Smthng += val;
        ValueAdded?.Invoke(val);
    }
    public void LostSmt(int val)
    {
        Smthng -= val;
        ValueSpended?.Invoke(val);
    }
}
