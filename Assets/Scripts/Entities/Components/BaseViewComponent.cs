using UnityEngine;

namespace Entities.Components
{
    public struct BaseViewComponent
    {
        public Sprite Icon;

        public BaseViewComponent(Sprite icon)
        {
            Icon = icon;
        }
    }
}