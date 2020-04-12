using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaDemo.Messages;

namespace AkkaDemo.Actors
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _userDictionary;

        public UserCoordinatorActor()
        {
            _userDictionary = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
                {
                    CreateChildUserIfNotExists(message.UserId);
                    IActorRef childActorRef = _userDictionary[message.UserId];
                    childActorRef.Tell(message);
                });

            Receive<StopMovieMessage>(message =>
                {
                    CreateChildUserIfNotExists(message.UserId);
                    IActorRef childActorRef = _userDictionary[message.UserId];
                    childActorRef.Tell(message);
                });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (_userDictionary.ContainsKey(userId))
            {
                return;
            }

            string userName = $"User{userId}";
            IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), userName);
            _userDictionary.Add(userId, newChildActorRef);
            int userCount = _userDictionary.Count;
            ColorConsole.WriteLineCyan($"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {userCount})");
        }


        #region Lifecycle methods

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan($"UserCoordinatorActor PreRestart because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan($"UserCoordinatorActor PostRestart because: {reason}");
            base.PostRestart(reason);
        } 

        #endregion
    }
}