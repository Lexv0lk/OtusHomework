using System;
using Atomic.Objects;

namespace Game.Scripts.Fabrics
{
    public interface IBulletFabric
    {
        event Action<IAtomicEntity> CreatedBullet;
        IAtomicEntity GetBullet();
    }
}