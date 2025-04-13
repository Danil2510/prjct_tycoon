using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Analytics;
using DefaultNamespace.SaveLoadSystem;
using Unity.Services.Analytics;
using UnityEngine;

[Serializable]
public class MoneyData
{
    public int Money;
}

public class MoneyStorage : Storage, ISaveLoaded
{
    [SerializeField] private string _flatKey;

    private MoneyData _moneyData;
    public string FlatKey => _flatKey;

    private IEnumerator Start()
    {
        while (GlobalGameState.IsInitialized == false)
            yield return null;
        
        if (SaveLoad.HasKey(_flatKey))
            SaveLoad.Load<MoneyData>(_flatKey, Onloaded);
        else
            _moneyData = new MoneyData();

        ValueAdded += ValueChanged;
        ValueSpended += ValueChanged;
    }

    private void ValueChanged(int _)
    {
        _moneyData.Money = Smthng;
        SaveLoad.Save<MoneyData>(_flatKey, _moneyData);
    }

    private void Onloaded(MoneyData data)
    {
        _moneyData = data;
        EarnSmt(data.Money);
    }

    private void OnDestroy()
    {
        ValueAdded -= ValueChanged;
        ValueSpended -= ValueChanged;
    }

    public override void EarnSmt(int val)
    {
        base.EarnSmt(val);
        var updateCurrencyEvent = new UpdateCurrencyEvent() {
            TotalMoney = StaticCurrencyCounter.AddCurrency(val)
        };
        AnalyticsService.Instance.RecordEvent(updateCurrencyEvent);
    }

}
