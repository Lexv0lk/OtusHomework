using EventBus.Events;
using Models;

namespace EventBus.Handlers.Logic
{
    public class DestroyEventHandler : BaseHandler<DestroyEvent>
    {
        private readonly TeamsSetup _teamsSetup;

        public DestroyEventHandler(EventBus eventBus, TeamsSetup teamsSetup) : base(eventBus)
        {
            _teamsSetup = teamsSetup;
        }

        protected override void OnHandleEvent(DestroyEvent evt)
        {
            if (_teamsSetup.RedTeam.Remove(evt.Entity))
                return;

            _teamsSetup.BlueTeam.Remove(evt.Entity);
        }
    }
}