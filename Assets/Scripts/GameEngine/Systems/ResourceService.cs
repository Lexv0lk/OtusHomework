using System.Collections.Generic;
using System.Linq;
using DI.Contexts;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class ResourceService : IGameService
    {
        private Dictionary<string, Resource> sceneResources = new();

        public ResourceService(List<Resource> resources)
        {
            this.sceneResources = resources.ToDictionary(it => it.ID);
        }

        public IEnumerable<Resource> GetResources()
        {
            return this.sceneResources.Values;
        }

        public Resource GetResource(string id)
        {
            return sceneResources[id];
        }
    }
}