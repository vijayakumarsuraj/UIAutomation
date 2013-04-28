using System.Windows.Automation;

namespace Automation.UI.Tree.QueryParts {

    /// <summary>
    ///     Represents the conditions part of a query.
    /// </summary>
    public class QueryConditionPart : QueryPart {

        /// <summary>
        ///     New query condition part.
        /// </summary>
        /// <param name="query">The query that is being built.</param>
        internal QueryConditionPart(Query query)
            : base(query) {
            UnaryOperator = null;
            BinaryOperator = null;
        }

        /// <summary>
        ///     The active binary operator.
        /// </summary>
        private IBinaryOperator BinaryOperator { get; set; }

        /// <summary>
        ///     The active unary operator.
        /// </summary>
        private IUnaryOperator UnaryOperator { get; set; }

        #region Searchable properties

        /// <summary>
        ///     Search using the automation id property.
        /// </summary>
        /// <returns>The matcher part of the query.</returns>
        public QueryMatcherPart Id() {
            return Property(AutomationElement.AutomationIdProperty);
        }

        /// <summary>
        ///     Search using the name property.
        /// </summary>
        /// <returns>The matcher part of the query.</returns>
        public QueryMatcherPart Name() {
            return Property(AutomationElement.NameProperty);
        }

        /// <summary>
        ///     Search using the name property.
        /// </summary>
        /// <returns>The matcher part of the query.</returns>
        public QueryMatcherPart Type() {
            return Property(AutomationElement.ControlTypeProperty);
        }

        /// <summary>
        ///     Search using an arbitrary property.
        /// </summary>
        /// <param name="property">The property to match.</param>
        /// <returns>The matcher part of the query.</returns>
        public QueryMatcherPart Property(AutomationProperty property) {
            return new QueryMatcherPart(property, this);
        }

        #endregion

        #region Unary operators

        /// <summary>
        ///     Sets the active unary operator to a logical "not".
        /// </summary>
        /// <returns>This condition part.</returns>
        public QueryConditionPart Not() {
            SetUnaryOperator(Util.Operators.Not);

            return this;
        }

        #endregion

        #region Miscellaneous
        
        /// <summary>
        ///     Updates the active binary operator.
        /// </summary>
        /// <param name="op">The new operator.</param>
        internal void SetBinaryOperator(IBinaryOperator op) {
            if (BinaryOperator != null)
                throw new QueryConstructionException("Cannot set operator - Another binary operator is already active");

            BinaryOperator = op;
        }

        /// <summary>
        ///     Updates the active unary operator.
        /// </summary>
        /// <param name="op">The new operator.</param>
        internal void SetUnaryOperator(IUnaryOperator op) {
            if (UnaryOperator != null)
                throw new QueryConstructionException("Cannot set operator - Another unary operator is already active");

            UnaryOperator = op;
        }

        /// <summary>
        ///     Applies the specified property condition to this query.
        /// </summary>
        /// <param name="condition">The condition to apply.</param>
        internal void ApplyCondition(Condition condition) {
            // Apply the active unary condition.
            condition = ApplyUnaryOperator(condition);
            // If the current active condition is null, not need to apply the binary operator.
            // If it isn't null, apply the binary operator.
            if (Query.Conditions == null) Query.Conditions = condition;
            else Query.Conditions = JoinWithBinaryOperator(Query.Conditions, condition);
            // Reset the operators for the next condition.
            UnaryOperator = null;
            BinaryOperator = null;
        }

        /// <summary>
        ///     Applies the active unary operator to the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The new condition after the operator is applied.</returns>
        private Condition ApplyUnaryOperator(Condition condition) {
            return UnaryOperator == null ? condition : UnaryOperator.Apply(condition);
        }

        /// <summary>
        ///     Applies the active binary operator to the specified conditions.
        /// </summary>
        /// <param name="left">The left hand condition.</param>
        /// <param name="right">The right hand condition.</param>
        /// <returns>The new condition after the operator is applied.</returns>
        private Condition JoinWithBinaryOperator(Condition left, Condition right) {
            return BinaryOperator == null ? Util.Operators.And.Join(left, right) : BinaryOperator.Join(left, right);
        }

        #endregion

        /// <summary>
        ///     Represents a part of the conditions of a query.
        /// </summary>
        public abstract class QueryConditionPartPart : QueryPart {

            /// <summary>
            ///     New query operator part.
            /// </summary>
            /// <param name="part">The condition part that is wrapping this.</param>
            protected internal QueryConditionPartPart(QueryConditionPart part)
                : base(part.Query) {
                ConditionPart = part;
            }

            /// <summary>
            ///     The condition part that is wrapping this query part.
            /// </summary>
            protected QueryConditionPart ConditionPart { get; private set; }

        }

    }

}
