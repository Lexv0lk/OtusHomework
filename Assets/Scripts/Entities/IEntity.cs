using System.Collections.Generic;

namespace Entities
{
    public interface IEntity
    {
        void Add(object component);
        bool TryGet<T>(out T element);
        T Get<T>();
        IReadOnlyList<object> GetAll();
    }
}