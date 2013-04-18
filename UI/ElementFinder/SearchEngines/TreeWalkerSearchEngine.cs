using System;
using System.Collections;
using System.Windows.Automation;

using Automation.UI.Util;

namespace Automation.UI.ElementFinder.SearchEngines {

    /// <summary>
    ///     Implementation of the search engine interface that uses a tree walker to find matching nodes.
    ///     The tree walker will search the UI automation tree in a breadth first order.
    /// </summary>
    internal class TreeWalkerSearchEngine : SearchEngine {

        /// <summary>
        ///     Calls the specified callback for each child of the specified node.
        /// </summary>
        /// <param name="root">The root element.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>The first non-null return value from the callback.</returns>
        private static object ExecuteWithChildren(AutomationElement root, WithElementCallback callback) {
            var children = root.FindAll(TreeScope.Children, Condition.TrueCondition);
            // Iterate over all the children and call the callback with each one.
            // Return the first call that returns a non-null value.
            foreach (AutomationElement child in children) {
                var result = callback(child);
                if (result != null)
                    return result;
            }
            // None of the calls returned a value, so return the default value.
            return null;
        }

        /// <summary>
        ///     Calls the specified callback for each descendant of the specified node.
        /// </summary>
        /// <param name="root">The root element.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>The first non-null return value from the callback.</returns>
        private static object ExecuteWithDescendants(AutomationElement root, WithElementCallback callback) {
            var children = root.FindAll(TreeScope.Children, Condition.TrueCondition);
            // Check descendants in breadth-first order (the assumption is that the required element
            // is more likely to be closer to the root element).
            var queue = new Queue(children);
            while (queue.Count > 0) {
                var child = (AutomationElement) queue.Dequeue();
                var result = callback(child);
                if (result != null)
                    return result;
                // Queue all grand children for processing.
                var grandChildren = child.FindAll(TreeScope.Children, Condition.TrueCondition);
                foreach (AutomationElement grandChild in grandChildren)
                    queue.Enqueue(grandChild);
            }
            // None of the descendants matched, so return null.
            return null;
        }

        /// <summary>
        ///     Gets the first result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="timeout">The maximum amount of time to wait for the required element to become available.</param>
        /// <returns>The automation element.</returns>
        public override AutomationElement GetFirstResult(Query query, TimeSpan timeout) {
            var root = query.Root;
            var scope = query.Scope;
            var conditions = query.Conditions;
            // The callback for return matched elements.
            var returnMatchedChild = new WithElementCallback(child => ConditionHelper.IsMeetsRequirements(conditions, child) ? child : null);
            // Execute based on scope.
            switch (scope) {
                case TreeScope.Children:
                    return (AutomationElement) ExecuteWithChildren(root, returnMatchedChild);
                case TreeScope.Descendants:
                    return (AutomationElement) ExecuteWithDescendants(root, returnMatchedChild);
                default:
                    throw new NotSupportedException("Scope '" + scope + "' is not supported for TreeWalker search engines");
            }
        }

        /// <summary>
        ///     Gets all the automation elements found using the condition specified in the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="timeout">The maximum amount of time to wait for at least one element to become available.</param>
        /// <returns>The collection of automation elements.</returns>
        public override IEnumerable GetAllResults(Query query, TimeSpan timeout) {
            var root = query.Root;
            var scope = query.Scope;
            var conditions = query.Conditions;
            var results = new ArrayList();
            // The callback for saving all matched elements.
            var saveMatchingChildren = new WithElementCallback(child => {
                if (ConditionHelper.IsMeetsRequirements(conditions, child))
                    results.Add(child);
                return null;
            });
            // Execute based on scope.
            switch (scope) {
                case TreeScope.Children:
                    ExecuteWithChildren(root, saveMatchingChildren);
                    return results;
                case TreeScope.Descendants:
                    ExecuteWithDescendants(root, saveMatchingChildren);
                    return results;
                default:
                    throw new NotSupportedException("Scope '" + scope + "' is not supported for TreeWalker search engines");
            }
        }

        /// <summary>
        ///     Callback for working with an automation element.
        /// </summary>
        /// <param name="child">The element.</param>
        /// <returns>A non-null value will stop execution and return this value.</returns>
        private delegate object WithElementCallback(AutomationElement child);

    }

}
