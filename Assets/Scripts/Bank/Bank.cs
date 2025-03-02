using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] Storage Coins;
    [Header("Object")]
    [SerializeField] ObjectStorage Object;
    [SerializeField] public int ObjectPrice;
    [SerializeField] int MinPrice;
    [SerializeField] int MaxPrice;
    [SerializeField] public bool polarity;
    [Header("Price Change")]
    [SerializeField] float TimeToChange;
    float timeBefChange;
    [SerializeField] int UpChanse;
    [SerializeField] int DownChance;
    [SerializeField] int AddMin;
    [SerializeField] int AddMax;

    private void Update()
    {
        if (timeBefChange > 0)
        {
            timeBefChange -= Time.deltaTime;
        }
        else
        {
            if (ObjectPrice == MinPrice)
            {
                polarity = true;
            }
            else if (ObjectPrice == MaxPrice)
            {
                polarity = false;
            }
            ChangePrices(polarity, 0);
            timeBefChange = TimeToChange;
        }
    }

    public void Exchange()
    {
        Coins.EarnSmt(Object.Smthng * ObjectPrice);
        Object.LostEverything();
    }

    public void ChangePrices(bool UP, int i)
    {
        if (UP)
        {
            if (Random.Range(0, 100) <= UpChanse)
            {
                ObjectPrice += (i =Random.Range(AddMin, AddMax));
                if (ObjectPrice > MaxPrice)
                    { ObjectPrice = MaxPrice; }
            }
            else
            {
                ObjectPrice -= (i =Random.Range(AddMin, AddMax));
                if (ObjectPrice < MinPrice)
                    { ObjectPrice = MinPrice; }
                polarity = false;
            }
        }
        else
        {
            if (Random.Range(0, 100) <= DownChance)
            {
                ObjectPrice -= (i = Random.Range(AddMin, AddMax));
                if (ObjectPrice < MinPrice)
                    { ObjectPrice = MinPrice; }
            }
            else
            {
                ObjectPrice += (i = Random.Range(AddMin, AddMax));
                if (ObjectPrice > MaxPrice)
                    { ObjectPrice = MaxPrice; }
                polarity = true;
            }
        }
    }

    public void HigherPriceByHalf()
    {
        ObjectPrice = ObjectPrice + ObjectPrice / 2;
        if (ObjectPrice > MaxPrice)
        {
            ObjectPrice = MaxPrice;
        }
    }
}