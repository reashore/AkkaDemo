using System;
using Akka.Actor;
using AkkaDemo.Messages;

namespace AkkaDemo.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            _userId = userId;
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ColorConsole.WriteRed($"UserActor {_userId} Error: cannot start playing another movie before stopping existing one"));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            ColorConsole.WriteYellow($"UserActor {_userId} has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => ColorConsole.WriteRed($"UserActor {_userId} Error: cannot stop if nothing is playing"));
            ColorConsole.WriteYellow($"UserActor {_userId} has now become Stopped");
        }
        
        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            ColorConsole.WriteYellow($"UserActor {_userId} is currently watching '{_currentlyWatching}'");
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteYellow($"UserActor {_userId} has stopped watching '{_currentlyWatching}'");
            _currentlyWatching = null;
            Become(Stopped);
        }

        #region Lifecycle methods 

        protected override void PreStart()
        {
            ColorConsole.WriteYellow($"UserActor {_userId} PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteYellow($"UserActor {_userId} PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteYellow($"UserActor {_userId} PreRestart because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteYellow($"UserActor {_userId} PostRestart because: {reason}");
            base.PostRestart(reason);
        } 

        #endregion
    }
}
