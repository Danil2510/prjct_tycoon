using UnityEngine;

public class UpgradeProduct : MonoBehaviour
{
    private PoolView currentObj;
    [SerializeField] Texture _fup;
    [SerializeField] Texture _sup;
    [SerializeField] Texture _tup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PoolView>() != null)
        {
            currentObj = other.GetComponent<PoolView>();
            if (currentObj.level < 3)
            {
                currentObj.SetLevelTextures(_fup, _sup, _tup);
                currentObj.Upgrade();
            }
        }
    }
}
