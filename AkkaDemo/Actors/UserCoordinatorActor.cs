using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaDemo.Messages;

namespace AkkaDemo.Actors
{
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
            ColorConsole.WriteCyan($"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {userCount})");
        }


        #region Lifecycle methods

        protected override void PreStart()
        {
            ColorConsole.WriteCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteCyan("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteCyan($"UserCoordinatorActor PreRestart because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteCyan($"UserCoordinatorActor PostRestart because: {reason}");
            base.PostRestart(reason);
        } 

        #endregion
    }
}