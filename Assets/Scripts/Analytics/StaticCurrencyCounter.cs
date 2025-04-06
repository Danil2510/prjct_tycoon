using System;

namespace DefaultNamespace.Analytics
{
    public static class StaticCurrencyCounter
    {
        private static int _totalCurrency;
        
        public static int TotalCurrency => _totalCurrency;

        public static int AddCurrency(int currency)
        {
            if (currency < 0)
                throw new Exception("You can't add negative currency to total counter");

            _totalCurrency += currency;
            return _totalCurrency;
        }
    }
}