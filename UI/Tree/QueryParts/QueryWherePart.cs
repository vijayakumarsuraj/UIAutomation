namespace Automation.UI.Tree.QueryParts {

    /// <summary>
    ///     Represents the where part of a query.
    /// </summary>
    public class QueryWherePart : QueryPart {

        /// <summary>
        ///     New query where part.
        /// </summary>
        /// <param name="query">The query that is being built.</param>
        internal QueryWherePart(Query query) : base(query) {}

        /// <summary>
        ///     Does nothing. Syntactic sugar to lead into the conditions part of the query.
        /// </summary>
        /// <returns>The conditions part of the query.</returns>
        public QueryConditionPart Where() {
            return new QueryConditionPart(Query);
        }

    }

}
