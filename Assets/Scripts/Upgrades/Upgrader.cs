using System;
using DefaultNamespace.SaveLoadSystem;
using UnityEngine;

[Serializable]
public class UpgradesCounter
{
    public int UpgradesOpen;
}

public class Upgrader : MonoBehaviour, ISaveLoaded
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private bool AudioPlay;
    [SerializeField] private int levelOfUpgrade;
    [SerializeField] private SetUpgrade[] set_upgrades;
    [SerializeField] private string _flatKey;

    private UpgradesCounter _upgradesCounter;
    public Storage storage;

    public string FlatKey => _flatKey;

    private void Awake()
    {
        _upgradesCounter = new UpgradesCounter();
        
        if (SaveLoad.HasKey(_flatKey))
        {
            Debug.Log("Has key");
            SaveLoad.Load<UpgradesCounter>(_flatKey, OnLoaded);
        }

    }

    private void OnLoaded(UpgradesCounter upgradesCounter)
    {
        Debug.Log("On Loaded");
        _upgradesCounter = upgradesCounter;
        for (int i = 0; i < _upgradesCounter.UpgradesOpen + 1; i++)
            set_upgrades[i].Do();

        levelOfUpgrade = _upgradesCounter.UpgradesOpen;
    }

    public void AddLevel()
    {
        AddLevelWithoutSave();
        _upgradesCounter.UpgradesOpen = levelOfUpgrade;
        SaveLoad.Save<UpgradesCounter>(_flatKey, _upgradesCounter);
    }

    private void AddLevelWithoutSave()
    {
        if (set_upgrades[levelOfUpgrade + 1].cost <= storage.Smthng)
        {
            if (levelOfUpgrade < set_upgrades.Length - 1)
                levelOfUpgrade++;
            if (AudioPlay) { m_AudioSource.Play(); }

            set_upgrades[levelOfUpgrade].Do();
            storage.LostSmt(set_upgrades[levelOfUpgrade].cost);
        }
    }

}