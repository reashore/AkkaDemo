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
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem");
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");            

            ColorConsole.WriteLineGray("Creating actor supervisory hierarchy");
            _movieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                Thread.Sleep(500);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("enter a command and hit enter");
                
                string command = Console.ReadLine();

                if (command == null)
                {
                    continue;
                }

                string[] commandArray = command.Split(',');

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(commandArray[1]);
                    string movieTitle = commandArray[2];

                    PlayMovieMessage message = new PlayMovieMessage(movieTitle, userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(commandArray[1]);                    

                    StopMovieMessage message = new StopMovieMessage(userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command == "exit")
                {
                    //MovieStreamingActorSystem.Shutdown();
                    _movieStreamingActorSystem.Terminate();
                    //MovieStreamingActorSystem.AwaitTermination();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

            } while (true);
        }
    }
}
