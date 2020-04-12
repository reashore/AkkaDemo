using System;
using Akka.Actor;

namespace AkkaDemo.Actors
{
    public class PlaybackActor : ReceiveActor
    {     
        public PlaybackActor()
        {           
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");        
        }

        #region Lifecycle methods

        protected override void PreStart()
        {
            ColorConsole.WriteGreen("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreen("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreen($"PlaybackActor PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreen($"PlaybackActor PostRestart because: {reason}");

            base.PostRestart(reason);
        } 
        #endregion
    }
}
