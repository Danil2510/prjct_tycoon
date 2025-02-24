using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] float TimeBefDestroy;

    private void Update()
    {
        TimeBefDestroy-= Time.deltaTime; 
        if (TimeBefDestroy < 0)
        {
            Destroy(gameObject);
        }
    }
}
