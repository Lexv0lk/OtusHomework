using System;
using Common;
using Rewards;

namespace SaveSystem.SaveLoaders
{
    [Serializable]
    public struct SessionTimeSave
    {
        public string StartTime;
        public string EndTime;
    }

    [Serializable]
    public struct ChestListSave
    {
        public ChestSave[] ChestSaves;
    }

    [Serializable]
    public struct ChestSave
    {
        public string Id;
        public string CreateTime;
    }
}