using UnityEngine;
using YG;

public class ADV_rewarded : MonoBehaviour
{
    public string idAdv;
    private int rewardCount = 0;
    [SerializeField] Bank[] banks;

    public void SetReward()
    {
        rewardCount += 1;
        foreach (var bank in banks) 
        {
            bank.HigherPriceByHalf();
        }
    }

    public void ShowRewardAdv_UseCallback()
    {
        YG2.RewardedAdvShow(idAdv, () => { SetReward(); });
    }
}
