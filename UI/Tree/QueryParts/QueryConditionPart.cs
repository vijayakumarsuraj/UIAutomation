using System;
using System.Windows.Automation;

using Automation.UI.Tree.Operators;

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
            OperatorPart = new QueryOperatorPart(this);
            UnaryOperator = null;
            BinaryOperator = null;
        }

        /// <summary>
        ///     The operator part of the conditions.
        /// </summary>
        private QueryOperatorPart OperatorPart { get; set; }

        /// <summary>
        ///     The active binary operator.
        /// </summary>
        private IBinaryOperator BinaryOperator { get; set; }

        /// <summary>
        ///     The active unary operator.
        /// </summary>
        private IUnaryOperator UnaryOperator { get; set; }

        #region Search criteria

        /// <summary>
        ///     Adds a property condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart Is(AutomationProperty property, object value) {
            ApplyCondition(new PropertyCondition(property, value));

            return OperatorPart;
        }

        /// <summary>
        ///     Adds a starts with condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart StartsWith(AutomationProperty property, string value) {
            ApplyCondition(new StringPropertyCondition(property, value, Util.Matchers.StartsWith));

            return OperatorPart;
        }

        /// <summary>
        ///     Adds an ends with condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart EndsWith(AutomationProperty property, string value) {
            ApplyCondition(new StringPropertyCondition(property, value, Util.Matchers.EndsWith));

            return OperatorPart;
        }

        /// <summary>
        ///     Adds a contains condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart Contains(AutomationProperty property, string value) {
            ApplyCondition(new StringPropertyCondition(property, value, Util.Matchers.Contains));

            return OperatorPart;
        }

        #endregion

        #region Search shortcuts

        /// <summary>
        ///     Adds an automation id is condition.
        /// </summary>
        /// <param name="id">The expected automation id.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart IdIs(string id) {
            return Is(AutomationElement.AutomationIdProperty, id);
        }

        /// <summary>
        ///     Adds a name is condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart NameIs(string name) {
            return Is(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a name starts with condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart NameStartsWith(string name) {
            return StartsWith(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a name ends with condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart NameEndsWith(string name) {
            return EndsWith(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a name contains condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart NameContains(string name) {
            return Contains(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a control type is condition.
        /// </summary>
        /// <param name="type">The expected control type.</param>
        /// <returns>The operator part of the query.</returns>
        public QueryOperatorPart TypeIs(ControlType type) {
            return Is(AutomationElement.ControlTypeProperty, type);
        }

        #endregion

        #region Miscellaneous

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

        /// <summary>
        ///     Updates the active binary operator.
        /// </summary>
        /// <param name="op">The new operator.</param>
        private void SetBinaryOperator(IBinaryOperator op) {
            if (BinaryOperator != null)
                throw new InvalidOperationException("Cannot set operator - Another binary operator is already active");

            BinaryOperator = op;
        }

        /// <summary>
        ///     Updates the active unary operator.
        /// </summary>
        /// <param name="op">The new operator.</param>
        private void SetUnaryOperator(IUnaryOperator op) {
            if (UnaryOperator != null)
                throw new InvalidOperationException("Cannot set operator - Another unary operator is already active");

            UnaryOperator = op;
        }

        /// <summary>
        ///     Applies the specified property condition to this query.
        /// </summary>
        /// <param name="condition">The condition to apply.</param>
        private void ApplyCondition(Condition condition) {
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

        #endregion

        /// <summary>
        ///     Represents the operator part of the conditions of a query.
        /// </summary>
        public class QueryOperatorPart : QueryPart {

            /// <summary>
            ///     New query operator part.
            /// </summary>
            /// <param name="part">The condition part that is wrapping this.</param>
            internal QueryOperatorPart(QueryConditionPart part)
                : base(part.Query) {
                ConditionPart = part;
            }

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

            /// <summary>
            ///     Sets the active unary operator to a logical "not" and the active binary operator to a logical "and".
            /// </summary>
            /// <returns>This condition part.</returns>
            public QueryConditionPart AndNot() {
                ConditionPart.SetUnaryOperator(Util.Operators.Not);
                ConditionPart.SetBinaryOperator(Util.Operators.And);

                return ConditionPart;
            }

            /// <summary>
            ///     Sets the active unary operator to a logical "not" and the active binary operator to a logical "or".
            /// </summary>
            /// <returns>This condition part.</returns>
            public QueryConditionPart OrNot() {
                ConditionPart.SetUnaryOperator(Util.Operators.Not);
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
                    throw new InvalidOperationException("Cannot select - No conditions have been specified");

                return Query;
            }

            #endregion

            /// <summary>
            ///     The condition part that is wrapping this operator part.
            /// </summary>
            private QueryConditionPart ConditionPart { get; set; }

        }

    }

}
