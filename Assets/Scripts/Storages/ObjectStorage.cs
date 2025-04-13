using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.SaveLoadSystem;
using UnityEngine;

[Serializable]
public class ObjectStorageSaved
{
    public int Amount;
}

public class ObjectStorage : Storage, ISaveLoaded
{
    [SerializeField] string Name;
    [SerializeField] public int MaxCapacity;
    [SerializeField] private string _flatKey;

    private ObjectStorageSaved _saved;
    
    public string FlatKey => _flatKey;
    
    private IEnumerator Start()
    {
        while (GlobalGameState.IsInitialized == false)
            yield return null;
        
        if (SaveLoad.HasKey(_flatKey))
            SaveLoad.Load<ObjectStorageSaved>(_flatKey, OnLoaded);
        else
            _saved = new ObjectStorageSaved();
        
        ValueAdded += ValueChanged;
        ValueSpended += ValueChanged;
    }

    private void OnLoaded(ObjectStorageSaved data)
    {
        _saved = data;
        EarnSmt(data.Amount);
    }
    
    private void ValueChanged(int _)
    {
        _saved.Amount = Smthng;
        SaveLoad.Save<ObjectStorageSaved>(_flatKey, _saved);
    }

    private void OnDestroy()
    {
        ValueAdded -= ValueChanged;
        ValueSpended -= ValueChanged;
    }

    private void Update()
    {
        if (Smthng > MaxCapacity)
        {
            LostSmt(Smthng - MaxCapacity);
            Smthng = MaxCapacity;
        }
    }

    public void LostEverything()
    {
        ValueSpended?.Invoke(Smthng);
        Smthng = 0;
    }

    public void NewMaxCup(int ToAdd)
    {
        MaxCapacity += ToAdd;
    }

}
