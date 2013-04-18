using System.Windows.Automation;

using Automation.UI.ElementFinder.SearchEngines;

namespace Automation.UI.ElementFinder {

    /// <summary>
    ///     Represents a query that is used to walk the UI automation tree.
    /// </summary>
    public class Query {

        /// <summary>
        ///     The root element of this query. All searches will be relative to this element.
        /// </summary>
        public AutomationElement Root { get; internal set; }

        /// <summary>
        ///     The conditions of this query.
        /// </summary>
        public Condition Conditions { get; internal set; }

        /// <summary>
        ///     The scope of the search.
        /// </summary>
        public TreeScope Scope { get; internal set; }

        /// <summary>
        ///     Creates a new query for the specified root with scope 'Children'.
        /// </summary>
        /// <param name="root">The root element.</param>
        /// <returns>The query builder.</returns>
        public static QueryBuilder SelectChildren(AutomationElement root) {
            return new QueryBuilder(root, TreeScope.Children);
        }

        /// <summary>
        ///     Creates a new query for the specified root with scope 'Descendants'.
        /// </summary>
        /// <param name="root">The root element.</param>
        /// <returns>The query builder.</returns>
        public static QueryBuilder SelectDescendants(AutomationElement root) {
            return new QueryBuilder(root, TreeScope.Descendants);
        }

        /// <summary>
        ///     Use the tree walker search engine to query for results.
        /// </summary>
        /// <returns>The tree walker search engine.</returns>
        public SearchEngine UsingTreeWalkerEngine() {
            return Util.SearchEngines.TreeWalker;
        }

        /// <summary>
        ///     Use the default search engine to query for results.
        /// </summary>
        /// <returns>The default search engine.</returns>
        public SearchEngine UsingDefaultEngine() {
            return Util.SearchEngines.Default;
        }

    }

}
