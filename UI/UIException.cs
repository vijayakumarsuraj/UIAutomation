using System;

namespace Automation.UI {

    /// <summary>
    ///     General UI exception.
    /// </summary>
    public class UIException : Exception {

        /// <summary>
        ///     New UI exception.
        /// </summary>
        public UIException() {}

        /// <summary>
        ///     New UI exception.
        /// </summary>
        /// <param name="message">The detail message.</param>
        public UIException(string message) : base(message) {}

        /// <summary>
        ///     New UI exception.
        /// </summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause of this exception.</param>
        public UIException(string message, Exception innerException) : base(message, innerException) {}

    }

}
