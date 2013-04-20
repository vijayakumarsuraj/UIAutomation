using System.Windows.Automation;

namespace Automation.UI.Tree.QueryParts {

    /// <summary>
    ///     Represents the scope part of a query.
    /// </summary>
    public class QueryScopePart : QueryPart {

        /// <summary>
        ///     New query scope part.
        /// </summary>
        /// <param name="query">The query that is being built.</param>
        internal QueryScopePart(Query query) : base(query) {}

        /// <summary>
        ///     Sets the scope of the query to 'Children'.
        /// </summary>
        /// <returns>The where part of the query.</returns>
        public QueryWherePart FindChildren() {
            Query.Scope = TreeScope.Children;

            return new QueryWherePart(Query);
        }

        /// <summary>
        ///     Sets the scope of the query to 'Descendants'.
        /// </summary>
        /// <returns>The where part of the query.</returns>
        public QueryWherePart FindDescendants() {
            Query.Scope = TreeScope.Descendants;
            return new QueryWherePart(Query);
        }

    }

}
