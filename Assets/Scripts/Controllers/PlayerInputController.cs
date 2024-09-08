using DI;

namespace Controllers
{
    public class PlayerInputController
    {
        private readonly TeamViewSetup _teamViewSetup;

        public PlayerInputController(TeamViewSetup teamViewSetup)
        {
            _teamViewSetup = teamViewSetup;
        }
    }
}