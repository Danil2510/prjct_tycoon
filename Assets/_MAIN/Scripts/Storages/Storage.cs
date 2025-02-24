using System;
using UnityEngine;

public abstract class Storage : MonoBehaviour
{
    public int Smthng = 0;

    public Action<int> ValueAdded;
    public Action<int> ValueSpended;

    public void EarnSmt(int val)
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
