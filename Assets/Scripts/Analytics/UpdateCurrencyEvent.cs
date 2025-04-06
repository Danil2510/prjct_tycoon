using Unity.Services.Analytics;

namespace DefaultNamespace.Analytics
{
    public class UpdateCurrencyEvent : Event
    {
        public UpdateCurrencyEvent() : base("UpdateCurrency")
        {
        }
        
        public int TotalMoney { set => SetParameter("TotalMoney", value); }
    }
}