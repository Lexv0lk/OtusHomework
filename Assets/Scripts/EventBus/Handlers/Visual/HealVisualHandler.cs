﻿using Entities.Components;
using EventBus.Events;
using Pipeline.Tasks.Visual;
using Pipeline.Visual;

namespace EventBus.Handlers.Visual
{
    public class HealVisualHandler : VisualHandler<HealEvent>
    {
        public HealVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus, visualPipeline)
        {
        }

        protected override void OnHandleEvent(HealEvent evt)
        {
            var view = evt.Target.Get<HeroPresentationComponent>().View;
            var stats = evt.Target.Get<StatsComponent>();
            VisualPipeline.AddTask(new HealVisualTask(view, stats));
        }
    }
}