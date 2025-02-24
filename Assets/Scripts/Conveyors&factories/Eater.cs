using System;
using UnityEngine;

public class Eater : MonoBehaviour
{
    [SerializeField] private Storage bank;
    public Action<int> ObjEated;
    float c;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PoolView>() != null)
        {
            ObjEated?.Invoke(other.GetComponent<PoolView>().level);
            bank.EarnSmt(other.GetComponent<PoolView>().Price);
            other.GetComponent<PoolView>().Consume();
        }
        if (other.name == "Player")
        {
            ObjEated?.Invoke(52);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        c-=Time.deltaTime;
        if (other.name == "Player" && c<0)
        {
            ObjEated?.Invoke(52);
            c = 1.5f;
        }
    }
}
