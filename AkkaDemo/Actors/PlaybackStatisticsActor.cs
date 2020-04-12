using System;
using Akka.Actor;

namespace AkkaDemo.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
        }

        #region Lifecycle methods

        protected override void PreStart()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteWhite($"PlaybackStatisticsActor PreRestart because: {reason.Message}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteWhite($"PlaybackStatisticsActor PostRestart because: {reason.Message}");
            base.PostRestart(reason);
        }

        #endregion
    }
}