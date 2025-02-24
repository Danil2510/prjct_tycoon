using UnityEngine;
using TMPro;

public class ChangeValue : MonoBehaviour
{
    [SerializeField] private Storage storage;
    [SerializeField] private TMP_Text smtTxt;
    private int RealNum;
    [SerializeField] private string ToAdd;

    void OnEnable()
    {
        storage.ValueSpended += SpendSmt;
        storage.ValueAdded += AddSmt;
    }
    void OnDisable()
    {
        storage.ValueSpended -= SpendSmt;
        storage.ValueAdded -= AddSmt;
    }

    void AddSmt(int val)
    {
        RealNum += val;
        smtTxt.text = ToAdd + RealNum.ToString();
    }
    void SpendSmt(int val)
    {
        RealNum -= val;
        smtTxt.text = ToAdd + RealNum.ToString();
    }
}
