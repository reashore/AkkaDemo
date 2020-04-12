using System;
using System.Threading;
using Akka.Actor;
using AkkaDemo.Actors;
using AkkaDemo.Messages;

namespace AkkaDemo
{
    internal class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        private static void Main()
        {
            ColorConsole.WriteGray("Creating MovieStreamingActorSystem");
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");            

            ColorConsole.WriteGray("Creating actor supervisory hierarchy");
            _movieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            while (true)
            {
                Thread.Sleep(500);
                Console.WriteLine();
                //Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteGray("Enter command");
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
                    ColorConsole.WriteGray("Actor system shutdown");
                    break;
                }
            }

            Environment.Exit(1);
        }
    }
}
