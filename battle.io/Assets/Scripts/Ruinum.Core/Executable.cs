using UnityEngine;

namespace Ruinum.Core
{
    public abstract class Executable : MonoBehaviour, IExecute
    {
        public abstract void Execute();

        public virtual void Start()
        {
            GameManager.Singleton.AddExecuteObject(this);
        }

        public virtual void OnDestroy()
        {
            GameManager.Singleton.RemoveExecuteObject(this);
        }
    }
}