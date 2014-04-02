using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;

namespace Ore.OpenCL
{
    /// <summary>
    /// Manages OpenCL devices and runs scripts.
    /// </summary>
    public class OpenCLManager
    {
        // the platform to run the calculations on
        private ComputePlatform platform;
        // platform properties
        private ComputeContextPropertyList properties;
        // context to run calculations in
        private ComputeContext context;

        /// <summary>
        /// The ComputeContext being used for calculation.
        /// </summary>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Create a new OpenCLManager
        /// </summary>
        public OpenCLManager()
        {
            // future plans: software backups for non-OpenCL devices
            if (ComputePlatform.Platforms.Count == 0)
                throw new Exception("Device does not support OpenCL.");
            // just choose the first one for now
            platform = ComputePlatform.Platforms[0];
            properties = new ComputeContextPropertyList(platform);
            context = new ComputeContext(platform.Devices, properties, null, IntPtr.Zero);
        }

        /// <summary>
        /// Run a script.
        /// </summary>
        /// <typeparam name="T">The type that will be returned.</typeparam>
        /// <param name="size">The size of the data.</param>
        /// <param name="scripts">One or more scripts to be run.</param>
        /// <returns>The return value of the script.</returns>
        public T[] Run<T>(ref T[] data, int size, OpenCLScript script) where T : struct
        {
            ComputeCommandQueue commands = new ComputeCommandQueue(context, context.Devices[0], ComputeCommandQueueFlags.None);
            ComputeEventList eventList = new ComputeEventList();
            commands.Execute(script.Kernel, null, new long[] { size }, new long[] { size }, eventList);
            commands.ReadFromBuffer((ComputeBuffer<T>)script.OutputBuffer, ref data, true, eventList);
            return data;
        }
    }
}
