namespace Entities
{
    public interface IEntity
    {
        bool TryGet<T>(out T element);
    }
}