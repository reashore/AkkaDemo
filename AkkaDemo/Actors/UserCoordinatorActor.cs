using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaDemo.Messages;

namespace AkkaDemo.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExists(message.UserId);

                    IActorRef childActorRef = _users[message.UserId];

                    childActorRef.Tell(message);
                });

            Receive<StopMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExists(message.UserId);

                    IActorRef childActorRef = _users[message.UserId];

                    childActorRef.Tell(message);
                });
        }


        private void CreateChildUserIfNotExists(int userId)
        {
            if (_users.ContainsKey(userId))
            {
                return;
            }

            string userName = $"User{userId}";
            IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), userName);
            _users.Add(userId, newChildActorRef);
            int userCount = _users.Count;
            ColorConsole.WriteLineCyan($"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {userCount})");
        }


        #region Lifecycle hooks
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
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreRestart because: {0}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostRestart because: {0}", reason);

            base.PostRestart(reason);
        } 
        #endregion
    }
}