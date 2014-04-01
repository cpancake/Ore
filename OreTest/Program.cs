using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ore;

namespace OreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Window window = new Window("Ore Test", 800, 600);
            Engine engine = new Engine();
            engine.Window = window;
            while (window.IsOpen())
            {
                engine.Render();
            }
        }
    }
}
