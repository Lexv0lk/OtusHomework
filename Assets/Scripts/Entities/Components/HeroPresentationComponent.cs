using UI;

namespace Entities.Components
{
    public struct HeroPresentationComponent
    {
        public HeroView View;

        public HeroPresentationComponent(HeroView view)
        {
            View = view;
        }
    }
}