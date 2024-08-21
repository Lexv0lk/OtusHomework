namespace Game.Scripts.Tech
{
    public static class MoveAPI
    {
        public const string MOVE_DIRECTION = "MoveDirection";
    }
    
    public static class LifeAPI
    {
        public const string TAKE_DAMAGE = "TakeDamage";
        public const string IS_DEAD = "IsDead";
    }
    
    public static class ShootAPI
    {
        public const string SHOOT_REQUEST = "ShootRequest";
    }
    
    public static class TransformAPI
    {
        public const string POSITION = "Position";
        public const string FORWARD_DIRECTION = "ForwardDirection";
        public const string GAME_OBJECT = "GameObject";
    }

    public static class PhysicsAPI
    {
        public const string COLLIDE_EVENT = "CollideEvent";
    }
}