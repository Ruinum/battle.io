using Ruinum.Core.Interfaces;

namespace Ruinum.Core.Systems
{
    public abstract class System<T> : ISystem where T: class
    {
        public static T Singleton { get; private set; }

        public void Initialize()
        {
            Singleton = this as T;
            Init();
        }

        public abstract void Init();
        public abstract void Execute();
    }
}
