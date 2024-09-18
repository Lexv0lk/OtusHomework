using System;
using System.Collections.Generic;
using System.Linq;
using Chests;
using Common;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class ChestsSaveLoader : SaveLoader<ChestListSave, ChestsController>
    {
        protected override ChestListSave GetSaveData(ChestsController service)
        {
            var chests = service.CurrentChests;
            List<ChestSave> chestSaves = new List<ChestSave>();

            foreach (var chest in chests)
            {
                ChestSave chestSave = new ChestSave
                {
                    Name = chest.Name,
                    IconTexture = Convert.ToBase64String(chest.Icon.texture.GetRawTextureData()),
                    IconTextureSize = new int2(chest.Icon.texture.width, chest.Icon.texture.height),
                    CloseDuration = chest.CloseDuration,
                    CreateTime = chest.CreateTime.ToString(),
                    Rewards = chest.Rewards.ToArray()
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
                Texture2D texture = new Texture2D(chestSave.IconTextureSize.X, chestSave.IconTextureSize.Y);
                texture.LoadImage(Convert.FromBase64String(chestSave.IconTexture));
                Sprite icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                
                chests.Add(new Chest(chestSave.Name, icon, chestSave.CloseDuration,
                    chestSave.Rewards, DateTime.Parse(chestSave.CreateTime)));
            }
        }
    }
}