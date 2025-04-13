using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.SaveLoadSystem;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BankSaveData
{
    public int ObjectPrice;
}

public class Bank : MonoBehaviour, ISaveLoaded
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
    [SerializeField] string _flatKey;

    private BankSaveData _saved;
    public string FlatKey => _flatKey;
    
    private float _maxPriceTimer;
    private bool _maxPriceAd;

    private const float MaxPriceTime = 20f;

    public void SetMaxPrice()
        => _maxPriceAd = true;

    private IEnumerator Start()
    {
        while (GlobalGameState.IsInitialized == false)
            yield return null;
        
        if (SaveLoad.HasKey(_flatKey))
            SaveLoad.Load<BankSaveData>(_flatKey, OnLoaded);
        else
        {
            _saved = new BankSaveData();
            SetAveragePrice();
        }
    }

    private void OnLoaded(BankSaveData data)
    {
        _saved = data;
        ObjectPrice = _saved.ObjectPrice;
    }

    private void SetAveragePrice()
    {
        ObjectPrice = Mathf.Clamp(MinPrice + (MaxPrice - MinPrice) / 2, MinPrice, MaxPrice);
        _saved.ObjectPrice = ObjectPrice;
    }

    private void Update()
    {
        if (GlobalGameState.IsInitialized == false)
            return;
        
        if (_maxPriceAd)
        {
            ObjectPrice = MaxPrice;
            _maxPriceTimer += Time.deltaTime;
            if (_maxPriceTimer >= MaxPriceTime)
            {
                _maxPriceTimer = 0f;
                _maxPriceAd = false;
                SetAveragePrice();
            }

            return;
        }
        
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
                ObjectPrice = Mathf.Clamp(ObjectPrice, MinPrice, MaxPrice);
            }
            else
            {
                ObjectPrice -= (i =Random.Range(AddMin, AddMax));
                ObjectPrice = Mathf.Clamp(ObjectPrice, MinPrice, MaxPrice);
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
        
        _saved.ObjectPrice = ObjectPrice;
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