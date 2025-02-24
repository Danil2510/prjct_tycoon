using UnityEngine;

public class EatEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Eater eater;
    [SerializeField] private Color flvlv;
    [SerializeField] private Color slvlv;
    [SerializeField] private Color tlvlv;

    void OnEnable()
    {
        eater.ObjEated += EffectPlay;
    }
    void OnDisable()
    {
        eater.ObjEated -= EffectPlay;
    }

    void EffectPlay(int �)
    {
        ParticleSystem.MainModule main = _particleSystem.main;
        if (� == 1)
        {
            main.startColor = flvlv;
        }
        if (� == 2)
        {
            main.startColor = slvlv;
        }
        if (� == 3)
        {
            main.startColor = tlvlv;
        }


        if (� == 52)
        {
            main.startColor = Color.red;
        }
        _particleSystem.Play();
    }
}
