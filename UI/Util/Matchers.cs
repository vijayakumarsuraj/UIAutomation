using Automation.UI.Tree;
using Automation.UI.Tree.Matchers;

namespace Automation.UI.Util {

    /// <summary>
    ///     Supported matchers.
    /// </summary>
    public static class Matchers {

        /// <summary>
        ///     Gets a matcher that will check if the actual value "starts with" the expected value.
        /// </summary>
        public static readonly Matcher<string> StartsWith = new StartsWithMatcher();

        /// <summary>
        ///     Gets a matcher that will check if the actual value "ends with" the expected value.
        /// </summary>
        public static readonly Matcher<string> EndsWith = new EndsWithMatcher();

        /// <summary>
        ///     Gets a matcher that will check if the actual value "contains" the expected value.
        /// </summary>
        public static readonly Matcher<string> Contains = new ContainsMatcher();

        /// <summary>
        ///     Gets a matcher that will check if the actual value matches the expected regular expression.
        /// </summary>
        public static readonly Matcher<string> Regex = new RegexMatcher();

    }

}
