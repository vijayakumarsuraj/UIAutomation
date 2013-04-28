namespace Automation.UI.Tree.QueryParts {

    /// <summary>
    ///     Represents the operator part of the conditions of a query.
    /// </summary>
    public class QueryOperatorPart : QueryConditionPart.QueryConditionPartPart {

        /// <summary>
        ///     New query operator part.
        /// </summary>
        /// <param name="part">The condition part that is wrapping this.</param>
        internal QueryOperatorPart(QueryConditionPart part) : base(part) {}

        #region Operators

        /// <summary>
        ///     Sets the active binary operator to a logical "and".
        /// </summary>
        /// <returns>This condition part.</returns>
        public QueryConditionPart And() {
            ConditionPart.SetBinaryOperator(Util.Operators.And);

            return ConditionPart;
        }

        /// <summary>
        ///     Sets the active binary operator to a logical "or".
        /// </summary>
        /// <returns>This condition part.</returns>
        public QueryConditionPart Or() {
            ConditionPart.SetBinaryOperator(Util.Operators.Or);

            return ConditionPart;
        }

        #endregion

        #region Select methods

        /// <summary>
        ///     Return the constructed query.
        /// </summary>
        /// <returns>The query.</returns>
        public Query Select() {
            if (Query.Conditions == null)
                throw new QueryConstructionException("Cannot select - No conditions have been specified");

            return Query;
        }

        #endregion
    }

}