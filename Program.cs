using System;
using System.Text;
using ConsoleApp.Snake;

namespace ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            if (args.Length > 0)
            {
                string mode = args[0];

                if (mode == "--selftest")
                {
                    return SelfTest.Run();
                }

                if (mode == "--demo")
                {
                    int ticks = args.Length > 1 ? ParseInt(args[1], 300) : 300;
                    int seed = args.Length > 2 ? ParseInt(args[2], Environment.TickCount) : Environment.TickCount;
                    return DemoRunner.Run(ticks, seed);
                }

                if (mode == "--help" || mode == "-h" || mode == "/?")
                {
                    PrintHelp();
                    return 0;
                }

                Console.WriteLine("Неизвестный режим: " + mode);
                PrintHelp();
                return 2;
            }

            if (Console.IsInputRedirected || Console.IsOutputRedirected)
            {
                Console.WriteLine("Ввод/вывод перенаправлен — интерактивная игра недоступна.");
                Console.WriteLine("Запускаю демо-режим бота на 300 тиков. Для игры запустите приложение в обычном терминале.");
                Console.WriteLine();
                return DemoRunner.Run(300, Environment.TickCount);
            }

            GameSettings settings = CreateSettings();
            GameEngine engine = new GameEngine(settings, new Random());
            ConsoleRenderer renderer = new ConsoleRenderer(settings);
            GameApp app = new GameApp(engine, renderer);

            try
            {
                Console.CursorVisible = false;
                return app.Run();
            }
            finally
            {
                Console.CursorVisible = true;
                Console.ResetColor();
            }
        }

        private static GameSettings CreateSettings()
        {
            int width = 30;
            int height = 20;

            try
            {
                if (!Console.IsOutputRedirected)
                {
                    width = Math.Min(30, Math.Max(16, (Console.WindowWidth - 2) / 2));
                    height = Math.Min(20, Math.Max(10, Console.WindowHeight - 8));
                }
            }
            catch (System.IO.IOException)
            {
                width = 30;
                height = 20;
            }
            catch (PlatformNotSupportedException)
            {
                width = 30;
                height = 20;
            }

            GameSettings settings = new GameSettings();
            settings.BoardWidth = width;
            settings.BoardHeight = height;
            return settings;
        }

        private static int ParseInt(string text, int fallback)
        {
            int value;
            return int.TryParse(text, out value) ? value : fallback;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("ЗМЕЙКА — консольная классика");
            Console.WriteLine();
            Console.WriteLine("Использование:");
            Console.WriteLine("  dotnet run                 — играть (стрелки/WASD, P — пауза, R — заново, Q — выход)");
            Console.WriteLine("  dotnet run -- --selftest   — самопроверка игровой логики");
            Console.WriteLine("  dotnet run -- --demo [тики] [seed] — демо-режим бота");
        }
    }
}
