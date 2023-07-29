using Ruinum.Core.Interfaces;
using Ruinum.Core.Systems;
using UnityEngine;

namespace Ruinum.Core
{
    public abstract class Executable : MonoBehaviour, IExecute
    {
        public abstract void Execute();

        public virtual void Start()
        {
            ExecuteSystem.Singleton.AddExecute(this);
        }

        public virtual void OnDestroy()
        {
            ExecuteSystem.Singleton.RemoveExecute(this);
        }
    }
}