using UnityEngine;
using YG;

public class ADV_Interstital : MonoBehaviour
{
    [SerializeField] MoneyStorage money;
    float timetoadd = 180;
    [SerializeField] float timelast = 180;

    void Update()
    {
        if (timelast > 0)
        {
            timelast -= Time.deltaTime;
        }
        else 
        {
            ShowADD();
            AddReward();
            timelast = timetoadd;
        }
    }

    void ShowADD()
    {
        YG2.InterstitialAdvShow();
    }
    void AddReward()
    {
        money.EarnSmt(money.Smthng / 10);
    }
}
