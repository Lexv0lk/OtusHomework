using DI.Contexts;

namespace SaveSystem.SaveLoaders
{
    public abstract class SaveLoader<TData, TService> : ISaveLoader
    {
        public void LoadState(GameRepository repository, GameContext context)
        {
            TService service = context.GetService<TService>();
            
            if (repository.TryGetData(out TData data))
                SetSaveData(service, data);
            else
                SetDefaultData(service);
        }

        public void SaveState(GameRepository repository, GameContext context)
        {
            TService service = context.GetService<TService>();
            TData data = GetSaveData(service);
            repository.SetData(data);
        }

        protected abstract TData GetSaveData(TService service);
        protected abstract void SetSaveData(TService service, TData data);

        protected virtual void SetDefaultData(TService service)
        {
            
        }
    }
}