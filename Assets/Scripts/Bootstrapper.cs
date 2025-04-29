using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public static class GlobalGameState
{
    public static bool IsInitialized;
}
    
    
public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameObject _curtain;
        
    private void Awake()
    {
        _curtain.SetActive(true);
        StartCoroutine(InitializeServices());

        Debug.unityLogger.logEnabled = false;
    }

    private IEnumerator InitializeServices()
    {
        var task = UnityServices.InitializeAsync();

        while (!task.IsCompleted)
            yield return null;
            
        AnalyticsService.Instance.StartDataCollection();

        GlobalGameState.IsInitialized = true;
        _curtain.SetActive(false);
    }
}