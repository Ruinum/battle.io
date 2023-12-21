using System.Collections;
using UnityEngine;

namespace Ruinum.Core.Systems
{
    public class CoroutineSystem : System<CoroutineSystem>
    {
        public override void Initialize() { }
        public override void Execute() { }

        public void RunCoroutine(IEnumerator coroutine)
        {
            var coroutineObject = new GameObject($"Coroutine: {coroutine}");
            Object.DontDestroyOnLoad(coroutineObject);

            var runner = coroutineObject.AddComponent<CoroutineRunner>();

            runner.StartCoroutine(runner.MonitorRunning(coroutine));
        }
    }
}