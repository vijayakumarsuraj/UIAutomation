using System;
using System.Windows.Automation;

using Automation.UI.ElementFinder.Operators;

namespace Automation.UI.ElementFinder {

    /// <summary>
    ///     Provides a collection of convenient methods for building a Query.
    /// </summary>
    public class QueryBuilder {

        /// <summary>
        ///     New query that will search relative to the specified automation element.
        /// </summary>
        /// <param name="root">The root element.</param>
        /// <param name="scope">The scope of the search.</param>
        public QueryBuilder(AutomationElement root, TreeScope scope) {
            Query = new Query {Root = root, Scope = scope};

            UnaryOperator = null;
            BinaryOperator = null;
            QueryState = State.Initialsed;
        }

        /// <summary>
        ///     New query that will search relative to the specified automation element.
        /// </summary>
        /// <param name="root">The root element.</param>
        public QueryBuilder(AutomationElement root) : this(root, TreeScope.Children) {}

        /// <summary>
        ///     The underlying query object.
        /// </summary>
        private Query Query { get; set; }

        /// <summary>
        ///     The active binary operator.
        /// </summary>
        private IBinaryOperator BinaryOperator { get; set; }

        /// <summary>
        ///     The current state of this query.
        /// </summary>
        private State QueryState { get; set; }

        /// <summary>
        ///     The active unary operator.
        /// </summary>
        private IUnaryOperator UnaryOperator { get; set; }

        /// <summary>
        ///     Ends this builder. No futher operations will be allowed on it.
        /// </summary>
        /// <returns></returns>
        public Query End() {
            if (QueryState <= State.SpecifyingConditions)
                throw new InvalidOperationException("Cannot End - No conditions have been specified");
            QueryState = State.Ended;

            return Query;
        }

        /// <summary>
        ///     Syntactic sugar to make the query look more like a SELECT WHERE statement.
        /// </summary>
        /// <returns></returns>
        public QueryBuilder Where() {
            if (QueryState >= State.Initialsed)
                throw new InvalidOperationException("Cannot use Where - Only allowed be used at the start of a query.");
            QueryState = State.Ready;

            return this;
        }

        #region Search operators

        /// <summary>
        ///     Sets the active binary operator to a logical "and".
        /// </summary>
        /// <returns>This query object.</returns>
        public QueryBuilder And() {
            SetBinaryOperator(Util.Operators.And);

            return this;
        }

        /// <summary>
        ///     Sets the active binary operator to a logical "or".
        /// </summary>
        /// <returns>This query object.</returns>
        public QueryBuilder Or() {
            SetBinaryOperator(Util.Operators.Or);

            return this;
        }

        /// <summary>
        ///     Sets the active unary operator to a logical "not".
        /// </summary>
        /// <returns>This query object.</returns>
        public QueryBuilder Not() {
            SetUnaryOperator(Util.Operators.Not);

            return this;
        }

        /// <summary>
        ///     Sets the active unary operator to a logical "not".
        /// </summary>
        /// <returns>This query object.</returns>
        public QueryBuilder IsNot() {
            return Not();
        }

        #endregion

        #region Search criteria

        /// <summary>
        ///     Adds a property condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>This query.</returns>
        public QueryBuilder Is(AutomationProperty property, object value) {
            ApplyCondition(new PropertyCondition(property, value));

            return this;
        }

        /// <summary>
        ///     Adds a starts with condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>This query.</returns>
        public QueryBuilder StartsWith(AutomationProperty property, string value) {
            ApplyCondition(new StringPropertyCondition(property, value, Util.Matchers.StartsWith));

            return this;
        }

        /// <summary>
        ///     Adds an ends with condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>This query.</returns>
        public QueryBuilder EndsWith(AutomationProperty property, string value) {
            ApplyCondition(new StringPropertyCondition(property, value, Util.Matchers.EndsWith));

            return this;
        }

        /// <summary>
        ///     Adds a contains condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>This query.</returns>
        public QueryBuilder Contains(AutomationProperty property, string value) {
            ApplyCondition(new StringPropertyCondition(property, value, Util.Matchers.Contains));

            return this;
        }

        #endregion

        #region Search shortcuts

        /// <summary>
        ///     Adds an automation id is condition.
        /// </summary>
        /// <param name="id">The expected automation id.</param>
        /// <returns>This query.</returns>
        public QueryBuilder IdIs(string id) {
            return Is(AutomationElement.AutomationIdProperty, id);
        }

        /// <summary>
        ///     Adds a name is condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>This query.</returns>
        public QueryBuilder NameIs(string name) {
            return Is(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a name starts with condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>This query.</returns>
        public QueryBuilder NameStartsWith(string name) {
            return StartsWith(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a name ends with condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>This query.</returns>
        public QueryBuilder NameEndsWith(string name) {
            return EndsWith(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a name contains condition.
        /// </summary>
        /// <param name="name">The expected name.</param>
        /// <returns>This query.</returns>
        public QueryBuilder NameContains(string name) {
            return Contains(AutomationElement.NameProperty, name);
        }

        /// <summary>
        ///     Adds a control type is condition.
        /// </summary>
        /// <param name="type">The expected control type.</param>
        /// <returns>This query.</returns>
        public QueryBuilder TypeIs(ControlType type) {
            return Is(AutomationElement.ControlTypeProperty, type);
        }

        #endregion

        #region Scope methods

        /// <summary>
        ///     Modifies the scope of this query to the immediate children of the root element.
        /// </summary>
        /// <returns>This query.</returns>
        public QueryBuilder SelectChildren() {
            SetScope(TreeScope.Children);

            return this;
        }

        /// <summary>
        ///     Modifies the scope of this query to the all descendants of the root element.
        /// </summary>
        /// <returns>This query.</returns>
        public QueryBuilder SelectDescendents() {
            SetScope(TreeScope.Descendants);

            return this;
        }

        #endregion

        #region Applying operators

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
            if (QueryState <= State.SpecifyingConditions)
                throw new InvalidOperationException("Cannot set operator - No conditions have been specified");
            if (QueryState == State.Ended)
                throw new InvalidOperationException("Cannot set operator - Query has been ended");

            BinaryOperator = op;
        }

        /// <summary>
        ///     Updates the active unary operator.
        /// </summary>
        /// <param name="op">The new operator.</param>
        private void SetUnaryOperator(IUnaryOperator op) {
            if (UnaryOperator != null)
                throw new InvalidOperationException("Cannot set operator - Another unary operator is already active");
            if (QueryState <= State.SpecifyingConditions)
                throw new InvalidOperationException("Cannot set operator - No conditions have been specified");
            if (QueryState == State.Ended)
                throw new InvalidOperationException("Cannot set operator - Query has been ended");

            UnaryOperator = op;
        }

        #endregion

        #region Miscellaneous

        /// <summary>
        ///     Applies the specified property condition to this query.
        /// </summary>
        /// <param name="condition">The condition to apply.</param>
        private void ApplyCondition(Condition condition) {
            if (QueryState == State.Ended)
                throw new InvalidOperationException("Cannot apply condition - Query has been ended");
            // Update the state.
            QueryState = State.SpecifyingConditions;
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
        ///     Updates the scope of this query.
        /// </summary>
        /// <param name="scope">The new scope.</param>
        private void SetScope(TreeScope scope) {
            if (QueryState >= State.SpecifiedScope)
                throw new InvalidOperationException("Cannot modify scope - Only allowed be used at the start of a query");
            QueryState = State.SpecifiedScope;
            Query.Scope = scope;
        }

        #endregion

        /// <summary>
        ///     Represents the different states the builder can be in.
        /// </summary>
        private enum State {

            Initialsed,
            SpecifiedScope,
            Ready,
            SpecifyingConditions,
            Ended

        }

    }

}
