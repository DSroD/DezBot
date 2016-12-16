using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_DezBot.ConsoleWorks
{
    static class ColoredConsole
    {
        public static void WriteLine(IConvertible value, ConsoleColor bgcolor = ConsoleColor.Black, ConsoleColor fgcolor = ConsoleColor.White)
        {
            ConsoleColor pbc = Console.BackgroundColor;
            ConsoleColor pfc = Console.ForegroundColor;

            Console.BackgroundColor = bgcolor;
            Console.ForegroundColor = fgcolor;

            Console.WriteLine(value);

            Console.BackgroundColor = pbc;
            Console.ForegroundColor = pfc;
        }

        public static void Write(IConvertible value, ConsoleColor bgcolor = ConsoleColor.Black, ConsoleColor fgcolor = ConsoleColor.White)
        {
            ConsoleColor pbc = Console.BackgroundColor;
            ConsoleColor pfc = Console.ForegroundColor;

            Console.BackgroundColor = bgcolor;
            Console.ForegroundColor = fgcolor;

            Console.Write(value);

            Console.BackgroundColor = pbc;
            Console.ForegroundColor = pfc;
        }

        public static IConvertible ReadLine(ConsoleColor bgcolor = ConsoleColor.Black, ConsoleColor fgcolor = ConsoleColor.White)
        {
            ConsoleColor pbc = Console.BackgroundColor;
            ConsoleColor pfc = Console.ForegroundColor;

            Console.BackgroundColor = bgcolor;
            Console.ForegroundColor = fgcolor;

            IConvertible r = Console.ReadLine();

            Console.BackgroundColor = pbc;
            Console.ForegroundColor = pfc;

            return r;
        }
    }
}
