using System;

namespace Automation.UI.Tree {

    /// <summary>
    ///     Thrown when the requested element could not be found.
    /// </summary>
    public class ElementNotFoundException : UIException {

        /// <summary>
        ///     New element not found exception.
        /// </summary>
        public ElementNotFoundException() {}

        /// <summary>
        ///     New element not found exception.
        /// </summary>
        /// <param name="message">The detail message.</param>
        public ElementNotFoundException(string message) : base(message) {}

        /// <summary>
        ///     New element not found exception.
        /// </summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause of this exception.</param>
        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException) {}

    }

}
