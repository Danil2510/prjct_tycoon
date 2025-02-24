using UnityEngine;

public class TextRotation : MonoBehaviour
{
    public Transform Player;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        transform.forward = Player.transform.forward;
    }
}
