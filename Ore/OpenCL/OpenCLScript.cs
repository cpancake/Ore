using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;

namespace Ore.OpenCL
{
    /// <summary>
    /// Represents an OpenCL script that will be compiled into a program and kernel
    /// </summary>
    public class OpenCLScript
    {
        // the ComputeContext being used for calcuation
        private ComputeContext context;
        // the source of this script
        private string source;
        // the program of this script
        private ComputeProgram program;
        // the kernel of this script
        private ComputeKernel kernel;
        // the index of the next argument to specify
        private int nextArg = 0;

        /// <summary>
        /// The buffer that the output will be written to.
        /// </summary>
        public object OutputBuffer;

        /// <summary>
        /// The ComputeKernel of this script.
        /// </summary>
        public ComputeKernel Kernel
        {
            get { return kernel; }
        }

        /// <summary>
        /// Create an OpenCL script object.
        /// </summary>
        /// <param name="context">The ComputeContext this script is in.</param>
        /// <param name="name">The name of the function to compile.</param>
        /// <param name="source">The script to compile.</param>
        public OpenCLScript(ComputeContext context, string name, string source)
        {
            this.source = source;
            this.context = context;
            program = new ComputeProgram(context, source);
            program.Build(null, null, null, IntPtr.Zero);
            kernel = program.CreateKernel(name);
        }

        /// <summary>
        /// Add multiple ComputeBuffers as inputs to this script.
        /// </summary>
        /// <param name="buffers">The ComputeBuffer objects to add.</param>
        public void SetArguments<T>(params ComputeBuffer<T>[] buffers) where T : struct
        {
            for (; nextArg < buffers.Length; nextArg++)
                kernel.SetMemoryArgument(nextArg, buffers[nextArg]);
        }
    }
}
