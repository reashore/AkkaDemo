using System;
using System.Threading;
using Akka.Actor;
using AkkaDemo.Actors;
using AkkaDemo.Messages;

namespace AkkaDemo
{
    public class AkkaStartup
    {
        private ActorSystem _movieStreamingActorSystem;

        public void Start()
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem");
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            ColorConsole.WriteLineGray("Creating actor supervisory hierarchy");
            _movieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            while (true)
            {
                Thread.Sleep(500);
                Console.WriteLine();
                ColorConsole.WriteGray("Enter command > ");
                string command = Console.ReadLine();

                if (command == null)
                {
                    continue;
                }

                command = command.ToLower();
                string[] commandArray = command.Split(',');

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(commandArray[1]);
                    string movieTitle = commandArray[2];

                    PlayMovieMessage message = new PlayMovieMessage(movieTitle, userId);
                    const string selector = "/user/Playback/UserCoordinator";
                    _movieStreamingActorSystem.ActorSelection(selector).Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(commandArray[1]);

                    StopMovieMessage message = new StopMovieMessage(userId);
                    const string selector = "/user/Playback/UserCoordinator";
                    _movieStreamingActorSystem.ActorSelection(selector).Tell(message);
                }

                if (command == "exit")
                {
                    _movieStreamingActorSystem.Terminate();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    break;
                }
            }

            //return Environment.Exit(1);
        }
    }
}