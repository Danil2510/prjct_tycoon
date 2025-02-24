using System;

public class StandartFactory : Factory
{
    public StandartFactory(PoolView prefab, Action<PoolView> returner) : base(prefab, returner)
    {
        _prefab = prefab;
    }
}
