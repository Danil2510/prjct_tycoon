using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform[] TPposes;
    [SerializeField] Transform player;

    public void TeleportTo(int topos)
    {
        player.position = TPposes[topos].position;
    }
}
