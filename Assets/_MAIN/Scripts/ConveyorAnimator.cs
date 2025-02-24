using UnityEngine;

public class ConveyorAnimator : MonoBehaviour
{
    [SerializeField] Material material;
    private float i;
    [SerializeField] private float j;   

    void Update()
    {
        i-=j;
        material.mainTextureOffset = new Vector2(0, i);
    }
}
