using System.Windows.Automation;

using Automation.UI.Tree;
using Automation.UI.Tree.QueryParts;

namespace Automation.UI {

    /// <summary>
    ///     Convenience methods for working with UI Automation's element tree.
    /// </summary>
    public static class UITree {

        /// <summary>
        ///     Start constructing an query for finding an automation element.
        /// </summary>
        /// <param name="root">The root element of the query.</param>
        /// <returns>The engine part of the query.</returns>
        public static QueryEnginePart Query(AutomationElement root) {
            return new QueryEnginePart(new Query(root));
        }

    }

}
