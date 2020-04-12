using System;
using Akka.Actor;

namespace AkkaDemo.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlaybackStatisticsActor : ReceiveActor
    {
        #region Lifecycle methods

        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PreRestart because: {reason.Message}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor PostRestart because: {reason.Message}");
            base.PostRestart(reason);
        }

        #endregion
    }
}