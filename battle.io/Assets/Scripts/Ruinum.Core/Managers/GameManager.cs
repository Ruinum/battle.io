using System.Collections.Generic;
using UnityEngine;


namespace Ruinum.Core
{
    public class GameManager : BaseSingleton<GameManager>
    {
        private List<IExecute> _executes = new List<IExecute>();

        private void Start()
        {
            Application.targetFrameRate = 90;
        }

        private void Update()
        {
            for (var i = 0; i < _executes.Count; i++)
            {
                var updatable = _executes[i];
                updatable.Execute();
            }
        }

        public void AddExecuteObject(IExecute executeGameObject)
        {
            _executes.Add(executeGameObject);
        }

        public void RemoveExecuteObject(IExecute executeGameObject)
        {
            _executes.Remove(executeGameObject);
        }

        public void ClearAllExecuteObjects()
        {
            _executes.Clear();
        }

        private void OnDestroy()
        {
            ClearAllExecuteObjects();
        }
    }
}