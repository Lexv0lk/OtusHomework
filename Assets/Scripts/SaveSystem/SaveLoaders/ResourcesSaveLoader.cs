using System.Linq;
using GameEngine;

namespace SaveSystem.SaveLoaders
{
    public class ResourcesSaveLoader : SaveLoader<ResourcesSave, ResourceService>
    {
        protected override ResourcesSave GetSaveData(ResourceService service)
        {
            var resources = service.GetResources();
            
            return new ResourcesSave()
            {
                SavedStates = resources.Select(GetResourceState).ToArray()
            };
        }

        protected override void SetSaveData(ResourceService service, ResourcesSave data)
        {
            foreach (var savedState in data.SavedStates)
                service.GetResource(savedState.Id).Amount = savedState.Amount;
        }

        private ResourceStateSave GetResourceState(Resource resource)
        {
            return new ResourceStateSave()
            {
                Id = resource.ID,
                Amount = resource.Amount
            };
        }
    }
}