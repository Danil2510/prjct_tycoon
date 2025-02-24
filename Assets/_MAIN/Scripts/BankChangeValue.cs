using UnityEngine;
using TMPro;

public class BankChangeValue : MonoBehaviour
{
    [SerializeField] private Bank bank;
    [SerializeField] private TMP_Text smtTxt;
    [SerializeField] private GameObject[] onoff;
    private int RealNum;

    private void Update()
    {
        ChangeValue();
    }
    public void ChangeValue()
    {
        RealNum = bank.ObjectPrice;
        smtTxt.text = RealNum.ToString();
        onoff[0].SetActive(bank.polarity);
        onoff[1].SetActive(!bank.polarity);
    }
}
