using UnityEngine;

public class UpgradeProduct : MonoBehaviour
{
    [SerializeField] float reload;
    private float rel;
    private PoolView currentObj;
    [SerializeField] Texture _fup;
    [SerializeField] Texture _sup;
    [SerializeField] Texture _tup;

    private void Update()
    {
        rel-=Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PoolView>() != null && rel<=0)
        {
            currentObj = other.GetComponent<PoolView>();
            if (currentObj.level < 3)
            {
                currentObj.SetLevelTextures(_fup, _sup, _tup);
                currentObj.Upgrade();
                rel = reload;
            }
        }
    }
}
