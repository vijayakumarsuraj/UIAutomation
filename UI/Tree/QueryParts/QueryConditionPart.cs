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

        /// <summary>
        ///     Represents the matcher part of the conditions of a query.
        /// </summary>
        public class QueryMatcherPart : QueryConditionPartPart {

            /// <summary>
            ///     New query matcher part.
            /// </summary>
            /// <param name="property">The property that will be matched.</param>
            /// <param name="part">The condition part that is wrapping this.</param>
            internal QueryMatcherPart(AutomationProperty property, QueryConditionPart part)
                : base(part) {
                Property = property;
            }

            /// <summary>
            ///     The property that this matcher will check.
            /// </summary>
            private AutomationProperty Property { get; set; }

            #region Matchers

            /// <summary>
            ///     Adds a property condition.
            /// </summary>
            /// <param name="value">The expected value.</param>
            /// <returns>The operator part of the query.</returns>
            public QueryOperatorPart Is(object value) {
                return ApplyMatcher(value);
            }

            /// <summary>
            ///     Adds a starts with condition.
            /// </summary>
            /// <param name="value">The expected value.</param>
            /// <returns>The operator part of the query.</returns>
            public QueryOperatorPart StartsWith(string value) {
                return ApplyMatcher(value, Util.Matchers.StartsWith);
            }

            /// <summary>
            ///     Adds an ends with condition.
            /// </summary>
            /// <param name="value">The expected value.</param>
            /// <returns>The operator part of the query.</returns>
            public QueryOperatorPart EndsWith(string value) {
                return ApplyMatcher(value, Util.Matchers.EndsWith);
            }

            /// <summary>
            ///     Adds a contains condition.
            /// </summary>
            /// <param name="value">The expected value.</param>
            /// <returns>The operator part of the query.</returns>
            public QueryOperatorPart Contains(string value) {
                return ApplyMatcher(value, Util.Matchers.Contains);
            }

            /// <summary>
            ///     Adds a regex condition.
            /// </summary>
            /// <param name="pattern">The regeular expression to match.</param>
            /// <returns>The operator part of the query.</returns>
            public QueryOperatorPart Matches(string pattern) {
                return ApplyMatcher(pattern, Util.Matchers.Regex);
            }

            #endregion

            /// <summary>
            ///     Applies the required string property condition in the condition part of the query.
            /// </summary>
            /// <param name="value">The expected value for the condition.</param>
            /// <param name="matcher">The matcher to use to check the condition.</param>
            /// <returns>THe operator part of the query.</returns>
            private QueryOperatorPart ApplyMatcher(string value, Matcher<string> matcher) {
                ConditionPart.ApplyCondition(new StringPropertyCondition(Property, value, matcher));
                return ConditionPart.OperatorPart;
            }

            /// <summary>
            ///     Applies the required property condition in the condition part of the query.
            /// </summary>
            /// <param name="value">The expected value for the condition.</param>
            /// <returns>THe operator part of the query.</returns>
            private QueryOperatorPart ApplyMatcher(object value) {
                ConditionPart.ApplyCondition(new PropertyCondition(Property, value));
                return ConditionPart.OperatorPart;
            }

        }

        /// <summary>
        ///     Represents the operator part of the conditions of a query.
        /// </summary>
        public class QueryOperatorPart : QueryConditionPartPart {

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

        }

    }

}
