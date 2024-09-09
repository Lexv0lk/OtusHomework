using UnityEngine;

namespace Entities.Components
{
    public struct PresentationDataComponent
    {
        public Sprite Icon;

        public PresentationDataComponent(Sprite icon)
        {
            Icon = icon;
        }
    }
}