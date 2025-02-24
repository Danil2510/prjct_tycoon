using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private bool AudioPlay;
    [SerializeField] private int levelOfUpgrade;
    public Storage storage;
    [SerializeField] private SetUpgrade[] set_upgrades;

    public void AddLevel()
    {
        if (set_upgrades[levelOfUpgrade+1].cost <= storage.Smthng)
        {
            if (levelOfUpgrade < set_upgrades.Length - 1)
                levelOfUpgrade++;
            if (AudioPlay) { m_AudioSource.Play(); }

            set_upgrades[levelOfUpgrade].Do();
            storage.LostSmt(set_upgrades[levelOfUpgrade].cost);
        }
    }
}
