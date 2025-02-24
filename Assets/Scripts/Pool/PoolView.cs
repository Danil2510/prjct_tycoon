using UnityEngine;
using System;

public class PoolView : MonoBehaviour, IPooled
{
    public int level = 1;
    Texture fup;
    Texture sup;
    Texture tup;
    public Action<PoolView> Returner {  get; set; }
    public int Price;

    public void ReturnToPool()
    {
        if (level > 1)
        {
            level = 0; 
            Upgrade();
        }
        Returner?.Invoke(this);
    }

    public void Consume()
    {
        ReturnToPool();
    }

    public void SetPrice(int price)
    {
        Price = price;
    }

    public void Upgrade() 
    {
        level++;
        Price = Price * 2;
        if (level == 1)
        {
            GetComponentInChildren<MeshRenderer>().material.mainTexture = fup;
        }
        else if (level == 2)
        {
            GetComponentInChildren<MeshRenderer>().material.mainTexture = sup;
        }
        else if (level == 3)
        {
            GetComponentInChildren<MeshRenderer>().material.mainTexture = tup;
        }
    }

    public void SetLevelTextures(Texture one, Texture two, Texture three)
    {
        fup = one;
        sup = two;
        tup = three;
    }
}
