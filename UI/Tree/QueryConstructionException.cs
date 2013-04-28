using System;

namespace Automation.UI.Tree {

    /// <summary>
    ///     Thrown if there is an exception while constructing a query.
    /// </summary>
    public class QueryConstructionException : Exception {

        /// <summary>
        ///     New query construction exception.
        /// </summary>
        public QueryConstructionException() {}

        /// <summary>
        ///     New query construction exception.
        /// </summary>
        /// <param name="message">The detail message.</param>
        public QueryConstructionException(string message) : base(message) {}

        /// <summary>
        ///     New query construction exception.
        /// </summary>
        /// <param name="message">The detail message.</param>
        /// <param name="inner">The cause of the exception.</param>
        public QueryConstructionException(string message, Exception inner) : base(message, inner) {}

    }

}
