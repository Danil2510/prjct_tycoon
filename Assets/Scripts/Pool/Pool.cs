using System;
using System.Collections.Generic;

public abstract class Pool
{
    protected List<PoolView> _pools;
    protected Action<PoolView> _returner;

    public Pool(Action<PoolView> returner)
    {
        _pools = new List<PoolView>();
        _returner = returner;

    }

    public abstract PoolView Give();
}
