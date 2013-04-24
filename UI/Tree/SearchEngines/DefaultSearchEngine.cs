using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Automation;

namespace Automation.UI.Tree.SearchEngines {

    /// <summary>
    ///     Standard search engine that will use UI Automation's built in methods.
    /// </summary>
    internal class DefaultSearchEngine : SearchEngine {

        /// <summary>
        ///     Gets the first result for the specified query.
        ///     Uses the FindFirst method.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="timeout">The maximum amount of time to wait for the required element to become available.</param>
        /// <returns>The automation element.</returns>
        public override AutomationElement GetFirstResult(Query query, TimeSpan timeout) {
            Debug.WriteLine("FirstResult - " + query, "UIAutomation-SearchEngine-Default");

            var root = query.Root;
            var scope = query.Scope;
            var conditions = query.Conditions;
            return (AutomationElement) ExecuteGetResult(() => root.FindFirst(scope, conditions), timeout);
        }

        /// <summary>
        ///     Gets all the automation elements found using the condition specified in the specified query.
        ///     Uses the FindAll method.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="timeout">The maximum amount of time to wait for at least one element to become available.</param>
        /// <returns>The collection of automation elements.</returns>
        public override IEnumerable GetAllResults(Query query, TimeSpan timeout) {
            Debug.WriteLine("AllResults - " + query, "UIAutomation-SearchEngine-Default");

            var root = query.Root;
            var scope = query.Scope;
            var conditions = query.Conditions;
            return (IEnumerable) ExecuteGetResult(() => {
                var children = root.FindAll(scope, conditions);
                return children.Count > 0 ? children : null;
            }, timeout);
        }

    }

}
