using Ruinum.Core.Interfaces;

namespace Ruinum.Core.Systems
{
    public abstract class System<T> : ISystem where T: class
    {
        public static T Singleton { get; private set; }

        public void InitializeSystem()
        {
            Singleton = this as T;
            Initialize();
        }

        public abstract void Initialize();
        public abstract void Execute();
    }
}
