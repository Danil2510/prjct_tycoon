using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class ADV_Interstital : MonoBehaviour
{
    [SerializeField] MoneyStorage money;
    float timetoadd = 180;
    [SerializeField] float timelast = 180;
    [SerializeField] private GameObject _faderDelayer;
    [SerializeField] private TMP_Text _delayerText;

    private readonly WaitForSeconds _waiter = new WaitForSeconds(1f);
    
    void Update()
    {
        if (timelast > 0)
        {
            timelast -= Time.deltaTime;
        }
        else 
        {
            ShowADD();
        }
    }

    void ShowADD()
    {
        StartCoroutine(AdDelay());
    }

    private IEnumerator AdDelay()
    {
        int time = 3;
        _faderDelayer.SetActive(true);
        _delayerText.text = "Реклама начнется через: 3";
        AudioListener.volume = 0f;
        while (time > 0)
        {
            yield return _waiter;
            time--;
            _delayerText.text = $"Реклама начнется через: {time}";
        }
        
        _faderDelayer.SetActive(false);
        YG2.InterstitialAdvShow();
        
        AddReward();
        timelast = timetoadd;
    }

    void AddReward()
    {
        AudioListener.volume = 1f;
        money.EarnSmt(money.Smthng / 10);
    }
}
