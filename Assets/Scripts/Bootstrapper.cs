using System;
using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(InitializeServices());

            Debug.unityLogger.logEnabled = false;
        }

        private IEnumerator InitializeServices()
        {
            var task = UnityServices.InitializeAsync();

            while (!task.IsCompleted)
                yield return null;
            
            AnalyticsService.Instance.StartDataCollection();
        }
    }
}