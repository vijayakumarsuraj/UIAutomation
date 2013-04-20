namespace Automation.UI.Tree.QueryParts {

    /// <summary>
    ///     Represents the engine part of a query.
    /// </summary>
    public class QueryEnginePart : QueryPart {

        /// <summary>
        ///     New query engine part.
        /// </summary>
        /// <param name="query">The query that is being built.</param>
        internal QueryEnginePart(Query query) : base(query) {}

        /// <summary>
        ///     Updates the query to use the TreeWalker search engine.
        /// </summary>
        /// <returns>The scope part of the query.</returns>
        public QueryScopePart UsingTreeWalkerEngine() {
            Query.Engine = Util.SearchEngines.TreeWalker;

            return new QueryScopePart(Query);
        }

        /// <summary>
        ///     Updates the query to use the Default search engine.
        /// </summary>
        /// <returns>The scope part of the query.</returns>
        public QueryScopePart UsingDefaultEngine() {
            Query.Engine = Util.SearchEngines.Default;

            return new QueryScopePart(Query);
        }

    }

}
