using DefaultNamespace.Analytics;
using Unity.Services.Analytics;

public class MoneyStorage : Storage
{
    public override void EarnSmt(int val)
    {
        base.EarnSmt(val);
        var updateCurrencyEvent = new UpdateCurrencyEvent() {
            TotalMoney = StaticCurrencyCounter.AddCurrency(val)
        };
        AnalyticsService.Instance.RecordEvent(updateCurrencyEvent);
    }
}
