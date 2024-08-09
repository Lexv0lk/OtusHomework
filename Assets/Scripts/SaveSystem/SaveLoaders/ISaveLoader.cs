using DI.Contexts;

namespace SaveSystem.SaveLoaders
{
    public interface ISaveLoader
    {
        void LoadState(GameRepository repository, GameContext context);

        void SaveState(GameRepository repository, GameContext context);
    }
}