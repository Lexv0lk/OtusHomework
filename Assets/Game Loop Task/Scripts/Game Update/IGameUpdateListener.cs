namespace ShootEmUp.GameUpdate
{
    public interface IGameUpdateListener
    {
        
    }

    public interface IGameSimpleUpdateListener : IGameUpdateListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameUpdateListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
}