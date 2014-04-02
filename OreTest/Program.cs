using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ore;
using Ore.OpenCL;

namespace OreTest
{
    class Program
    {
        static string source = @"
kernel void VectorAdd(
    global  read_only float* a,
    global  read_only float* b,
    global write_only float* c )
{
    int index = get_global_id(0);
    c[index] = a[index] + b[index];
}
";
        static void Main(string[] args)
        {
            /*Window window = new Window("Ore Test", 800, 600);
            Engine engine = new Engine();
            engine.Window = window;
            while (window.IsOpen())
            {
                engine.Render();
            }*/
            int count = 10;
            float[] arrA = new float[count];
            float[] arrB = new float[count];
            float[] arrC = new float[count];
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                arrA[i] = (float)(rand.NextDouble() * 100);
                arrB[i] = (float)(rand.NextDouble() * 100);
            }
            OpenCLManager manager = new OpenCLManager();
            OpenCLScript script = new OpenCLScript(manager.Context, "VectorAdd", source);
            Cloo.ComputeBuffer<float> bufA = new Cloo.ComputeBuffer<float>(manager.Context, Cloo.ComputeMemoryFlags.CopyHostPointer | Cloo.ComputeMemoryFlags.ReadOnly, arrA);
            Cloo.ComputeBuffer<float> bufB = new Cloo.ComputeBuffer<float>(manager.Context, Cloo.ComputeMemoryFlags.CopyHostPointer | Cloo.ComputeMemoryFlags.ReadOnly, arrB);
            Cloo.ComputeBuffer<float> bufC = new Cloo.ComputeBuffer<float>(manager.Context, Cloo.ComputeMemoryFlags.WriteOnly, arrC);
            script.SetArguments<float>(bufA, bufB, bufC);
            script.OutputBuffer = bufC;
            manager.Run(ref arrC, count, script);
            for (int i = 0; i < count; i++)
                Console.WriteLine("{0} + {1} = {2}", arrA[i], arrB[i], arrC[i]);
            Console.ReadKey();
        }
    }
}
