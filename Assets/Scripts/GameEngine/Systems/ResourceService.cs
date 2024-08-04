using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class ResourceService
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
    }
}