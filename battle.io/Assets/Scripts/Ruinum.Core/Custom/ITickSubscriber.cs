using System;

namespace Ruinum.Gameplay.Buildings
{
    public interface ITickSubscriber
    {
        Action TickEventSubscriber { get; }
        Action RefreshTickEventSubscriber { get; }
    }
}
