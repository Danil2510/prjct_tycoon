using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class ADV_rewarded : MonoBehaviour
{
    public string idAdv;
    private int rewardCount = 0;
    [SerializeField] Bank[] banks;
    [SerializeField] private GameObject _faderDelayer;
    [SerializeField] private TMP_Text _delayerText;

    private readonly WaitForSeconds _waiter = new WaitForSeconds(1f);
    
    public void SetReward()
    {
        rewardCount += 1;
        AudioListener.volume = 1f;
        foreach (var bank in banks) 
        {
            bank.HigherPriceByHalf();
        }
    }

    public void ShowRewardAdv_UseCallback()
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
        YG2.RewardedAdvShow(idAdv, () => { SetReward(); });
    }
}
