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

    void EffectPlay(int ø)
    {
        ParticleSystem.MainModule main = _particleSystem.main;
        if (ø == 1)
        {
            main.startColor = flvlv;
        }
        if (ø == 2)
        {
            main.startColor = slvlv;
        }
        if (ø == 3)
        {
            main.startColor = tlvlv;
        }


        if (ø == 52)
        {
            main.startColor = Color.red;
        }
        _particleSystem.Play();
    }
}
