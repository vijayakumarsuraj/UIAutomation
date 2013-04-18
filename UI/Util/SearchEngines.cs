using Automation.UI.ElementFinder.SearchEngines;

namespace Automation.UI.Util {

    /// <summary>
    ///     Supported search engines.
    /// </summary>
    public static class SearchEngines {

        /// <summary>
        ///     The default search engine. Uses UI Automation's in-built search methods (i.e. FindFirst and FindAll).
        /// </summary>
        public static readonly SearchEngine Default = new DefaultSearchEngine();

        /// <summary>
        ///     A tree walker search engine. Uses a tree walker to walk the automation tree to find an element.
        ///     Significantly slower than the default search engine and should be used only for matching partially
        ///     on strings.
        /// </summary>
        public static readonly SearchEngine TreeWalker = new TreeWalkerSearchEngine();

    }

}
