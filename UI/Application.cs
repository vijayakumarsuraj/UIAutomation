using System.Diagnostics;
using System.Windows.Automation;

namespace Automation.UI {

    /// <summary>
    /// Represents the application being automated.
    /// </summary>
    public class Application {

        /// <summary>
        /// Launches the application with the specified executable.
        /// </summary>
        /// <param name="executable">The executable to launch.</param>
        /// <returns>The application.</returns>
        public static Application Launch(string executable) {
            var info = new ProcessStartInfo(executable);
            var process = Process.Start(info);
            var pid = process.Id;

            return new Application(pid);
        }

        /// <summary>
        /// The UI components that make up the root windows of this application.
        /// </summary>
        public UIComponents Roots { get; private set; }

        /// <summary>
        /// The process id of this application.
        /// </summary>
        public int ProcessId { get; private set; }

        /// <summary>
        /// New application bound to the specified process id.
        /// </summary>
        /// <param name="processId">The process id.</param>
        private Application(int processId) {
            ProcessId = processId;
        }

    }

}
