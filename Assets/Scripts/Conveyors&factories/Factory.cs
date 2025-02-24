using UnityEngine;
using System;

public abstract class Factory : Pool
{
    protected PoolView _prefab;
    private int OBjprice;
    private Transform Spawnpos;

    public Factory(PoolView prefab, Action<PoolView> returner) : base(returner)
    {
        _prefab = prefab;
    }

    public override PoolView Give()
    {
        if (_pools.Count > 0)
        {
            var pool = _pools[0];
            pool.gameObject.SetActive(true);
            pool.gameObject.transform.position = Spawnpos.position;
            pool.gameObject.transform.rotation = Spawnpos.rotation;
            pool.SetPrice(OBjprice);
            pool.GetComponent<Rigidbody>().Sleep();
            _pools.RemoveAt(0);

            return pool;
        }
        else
        {
            var position = Spawnpos.position;
            var poolView = GameObject.Instantiate(_prefab, position, Quaternion.identity);
            poolView.SetPrice(OBjprice);
            poolView.Returner += ReturnToPool;
            poolView.Returner += _returner;

            return poolView;
        }  
    }

    private void ReturnToPool(PoolView poolView) 
    {
        _pools.Add(poolView);
    }

    public void SetPrice(int p)
    {
        OBjprice = p;   
    }
    public void SetSpawnpos(Transform p) 
    {
        Spawnpos = p;
    }
}
