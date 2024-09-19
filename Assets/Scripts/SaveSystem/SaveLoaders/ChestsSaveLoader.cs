using System;
using System.Collections.Generic;
using System.Linq;
using Chests;
using Chests.Configs;
using Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class ChestsSaveLoader : SaveLoader<ChestListSave, ChestsController>
    {
        private readonly ChestConfigList _chestConfigList;

        public ChestsSaveLoader(ChestConfigList chestConfigList)
        {
            _chestConfigList = chestConfigList;
        }
        
        protected override ChestListSave GetSaveData(ChestsController service)
        {
            var chests = service.CurrentChests;
            List<ChestSave> chestSaves = new List<ChestSave>();

            foreach (var chest in chests)
            {
                ChestSave chestSave = new ChestSave
                {
                    Id = chest.Config.Id,
                    CreateTime = chest.CreateTime.ToString(),
                };
                
                chestSaves.Add(chestSave);
            }

            return new ChestListSave
            {
                ChestSaves = chestSaves.ToArray()
            };
        }

        protected override void SetSaveData(ChestsController service, ChestListSave data)
        {
            List<Chest> chests = new List<Chest>();

            foreach (var chestSave in data.ChestSaves)
            {
                var chestConfig = _chestConfigList.GetConfig(chestSave.Id);
                chests.Add(new Chest(chestConfig, DateTime.Parse(chestSave.CreateTime)));
            }
            
            service.SetupChests(chests);
        }
    }
}