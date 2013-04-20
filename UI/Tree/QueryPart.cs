namespace Automation.UI.Tree {

    /// <summary>
    ///     Represents a part of a query.
    /// </summary>
    public abstract class QueryPart {

        /// <summary>
        ///     New query part.
        /// </summary>
        /// <param name="query">The query that is being built.</param>
        protected QueryPart(Query query) {
            Query = query;
        }

        /// <summary>
        ///     The query that is being built.
        /// </summary>
        internal Query Query { get; private set; }

    }

}
