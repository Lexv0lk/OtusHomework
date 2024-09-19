using System.Collections.Generic;

namespace Chests.Configs
{
    public class ChestConfigList
    {
        private readonly Dictionary<string, ChestConfig> _configs = new();

        public ChestConfigList(ChestConfig[] configs)
        {
            foreach (var config in configs)
                _configs.Add(config.Id, config);
        }

        public ChestConfig GetConfig(string id)
        {
            return _configs[id];
        }
    }
}