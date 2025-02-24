using System;

public interface IPooled
{
    Action<PoolView> Returner { get; set; }
    void ReturnToPool();
}
