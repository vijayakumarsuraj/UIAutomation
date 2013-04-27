using System;
using System.Collections;
using System.Windows.Automation;

using Automation.UI.Util;

namespace Automation.UI.Tree {

    /// <summary>
    ///     Represents a query that is used to walk the UI automation tree.
    /// </summary>
    public class Query {

        /// <summary>
        ///     New query that will search relative to the specified element.
        /// </summary>
        /// <param name="root">The root element of the search.</param>
        internal Query(AutomationElement root) {
            Root = root;
        }

        /// <summary>
        ///     The root element of this query. All searches will be relative to this element.
        /// </summary>
        internal AutomationElement Root { get; private set; }

        /// <summary>
        ///     The conditions of this query.
        /// </summary>
        internal Condition Conditions { get; set; }

        /// <summary>
        ///     The scope of the search.
        /// </summary>
        internal TreeScope Scope { get; set; }

        /// <summary>
        ///     The engine to use for the search.
        /// </summary>
        internal SearchEngine Engine { get; set; }

        /// <summary>
        ///     Gets a string that represents this query's condition and scope.
        /// </summary>
        /// <returns>The string representation of this query.</returns>
        public override string ToString() {
            var conditionsString = ConditionHelper.ToString(Conditions);
            var rootString = AutomationElementHelper.ToString(Root);

            return conditionsString + ", Scope=" + Scope + ", Root=" + rootString;
        }

        /// <summary>
        ///     Gets all the results of executing this query.
        /// </summary>
        /// <returns>A collection containing all matched elements.</returns>
        public IEnumerable AllResults() {
            return Engine.GetAllResults(this);
        }

        /// <summary>
        ///     Get the first result of executing this query. If no elements were found, throws an exception.
        /// </summary>
        /// <returns>The first match.</returns>
        public UIComponent FirstResult() {
            var result = Engine.GetFirstResult(this);
            if (result == null)
                throw new ElementNotFoundException("No elements matched the specified query");

            return new UIComponent(result);
        }

        /// <summary>
        ///     Get the first result of executing this query. If no elements were found, throws an exception.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait for the requested element to become available.</param>
        /// <returns>The first match.</returns>
        public UIComponent FirstResult(TimeSpan timeout) {
            var result = Engine.GetFirstResult(this, timeout);
            if (result == null)
                throw new ElementNotFoundException("No elements matched the specified query");

            return new UIComponent(result);
        }

    }

}
