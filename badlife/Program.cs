using System;
using System.Threading.Tasks;

using Autofac;

namespace badlife
{
    /// <summary>
    /// Implements Conway's Game Of Life badly
    /// https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life on a torus
    /// </summary>
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            int returnValue = 0;

            try
            {
                Container = BuildContainer();

                var (game, parser) = await InitializeGame(args[0]);

                int counter = 1;

                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                {
                    WriteGameState(game, parser, counter);
                    
                    await Task.Delay(1000);

                    game.Evolve();

                    counter++;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);

                Console.ReadKey();

                returnValue = -1;
            }
            
            return returnValue;
        }

        /// <summary>
        /// Create the game and display parser
        /// </summary>
        /// <returns></returns>
        static async Task<(IGame, IDisplayParser<int,char>)> InitializeGame(string fileName)
        {
            var textLoader = Container.Resolve<ITextLoader>();

            var displayLines = await textLoader.LoadFile(fileName);

            var game = Container.Resolve<IGame>();
            
            game.Initialize(displayLines);

            var parser = Container.Resolve<IDisplayParser<int, char>>();

            return (game, parser);
        }

        /// <summary>
        /// Write the Game State to the console
        /// </summary>
        /// <param name="game"></param>
        /// <param name="parser"></param>
        /// <param name="counter"></param>
        static void WriteGameState(IGame game, IDisplayParser<int, char> parser, int counter)
        {
            Console.Clear();

            Console.WriteLine("Press Esc Key to exit.....");
            Console.WriteLine();

            var displayValues = parser.GetDisplayValues(game.Values);

            foreach (char[] line in displayValues)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
            Console.WriteLine($"Iteration: {counter}");

        }

        static IContainer Container { get; set; }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            var parser = DisplayParser<int, char>
                .Create(((c) => c == '*' ? 1 : 0, (state) => state == 1 ? '*' : '_'));

            builder.RegisterInstance<IDisplayParser<int, char>>(parser);
            builder.RegisterType<GameRules>()
                .As<IGameRules>().InstancePerLifetimeScope();
            builder.RegisterType<Board>().InstancePerDependency();
            builder.RegisterType<BoardFactory>()
                .As<IBoardFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Game>().As<IGame>().InstancePerLifetimeScope();
            builder.RegisterType<TextLoader>().As<ITextLoader>().InstancePerLifetimeScope();

            return builder.Build();
        }

    }
}
