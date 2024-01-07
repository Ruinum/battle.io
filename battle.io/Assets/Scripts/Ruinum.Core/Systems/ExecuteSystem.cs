using Ruinum.Core.Interfaces;
using System.Collections.Generic;

namespace Ruinum.Core.Systems
{
    public class ExecuteSystem : System<ExecuteSystem> 
    {
        private List<IExecute> _executes = new List<IExecute>();

        public override void Initialize() { }
        public override void Execute()
        {
            for (var i = 0; i < _executes.Count; i++)
            {
                var updatable = _executes[i];
                updatable.Execute();
            }
        }

        public void AddExecute(IExecute executeGameObject)
        {
            _executes.Add(executeGameObject);
        }

        public void RemoveExecute(IExecute executeGameObject)
        {
            _executes.Remove(executeGameObject);
        }

        public void ClearAllExecuteObjects()
        {
            _executes.Clear();
        }
    }
}