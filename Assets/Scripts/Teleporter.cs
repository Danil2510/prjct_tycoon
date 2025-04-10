using System.Collections.Generic;
using DefaultNamespace.Analytics;
using Unity.Services.Analytics;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform[] TPposes;
    [SerializeField] Transform player;

    public void TeleportTo(int topos)
    {
        player.position = TPposes[topos].position;

        if (topos == 1)
        {
            Floor2ReachedEvent floor2Event = new Floor2ReachedEvent();
            AnalyticsService.Instance.RecordEvent(floor2Event);
        }
        else if (topos == 2)
        {
            var floor3Event = new Floor3ReachedEvent();
            AnalyticsService.Instance.RecordEvent(floor3Event);
        }
    }
}