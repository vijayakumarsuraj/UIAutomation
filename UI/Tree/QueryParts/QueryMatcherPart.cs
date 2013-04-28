using System.Windows.Automation;

using Automation.UI.Tree.SearchEngines;

namespace Automation.UI.Tree.QueryParts {

    /// <summary>
    ///     Represents the matcher part of the conditions of a query.
    /// </summary>
    public class QueryMatcherPart : QueryConditionPart.QueryConditionPartPart {

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
            if (!(Query.Engine is TreeWalkerSearchEngine))
                throw new QueryConstructionException("Cannot apply matcher - Search engine is not a TreeWalker");

            ConditionPart.ApplyCondition(new StringPropertyCondition(Property, value, matcher));
            return new QueryOperatorPart(ConditionPart);
        }

        /// <summary>
        ///     Applies the required property condition in the condition part of the query.
        /// </summary>
        /// <param name="value">The expected value for the condition.</param>
        /// <returns>THe operator part of the query.</returns>
        private QueryOperatorPart ApplyMatcher(object value) {
            ConditionPart.ApplyCondition(new PropertyCondition(Property, value));
            return new QueryOperatorPart(ConditionPart);
        }

    }

}