using System.Collections.Generic;

namespace Entities
{
    public interface IEntity
    {
        void Add(object component);
        void Remove<T>();
        void AddRange(params object[] component);
        bool TryGet<T>(out T element);
        T Get<T>();
        void Set<T>(T component);
        IReadOnlyList<object> GetAll();
    }
}