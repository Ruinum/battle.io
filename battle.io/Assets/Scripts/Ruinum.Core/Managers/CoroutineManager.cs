using System.Collections;
using UnityEngine;

namespace Ruinum.Core
{
    public class CoroutineManager : BaseSingleton<CoroutineManager>
    {
        public void RunCoroutine(IEnumerator coroutine)
        {
            var coroutineObject = new GameObject($"Coroutine: {coroutine}");
            DontDestroyOnLoad(coroutineObject);

            var runner = coroutineObject.AddComponent<CoroutineRunner>();

            runner.StartCoroutine(runner.MonitorRunning(coroutine));
        }
    }
}