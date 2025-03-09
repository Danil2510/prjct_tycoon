using System.Threading;
using UnityEngine;

public class Debuger : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Storage[] storage;
    [SerializeField] GameObject look;
    [SerializeField] GameObject g;
    bool lick = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            storage[0].EarnSmt(50000);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            storage[0].EarnSmt(999999999);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            storage[1].EarnSmt(10);
            storage[2].EarnSmt(10);
            storage[3].EarnSmt(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
        }
    }
}
