using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Automation.UI.Tree.Matchers {

    /// <summary>
    ///     A matcher that compares two strings to see if the actual value matches the expected regular expression.
    /// </summary>
    internal class RegexMatcher : StringMatcher {

        /// <summary>
        ///     Checks to see if the actual value matches the specified expected regular expression.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <param name="expected">The expected start value.</param>
        /// <returns>True if the values match.</returns>
        public override bool IsMatch(string actual, string expected) {
            Trace.WriteLine("Checking '" + actual + "'" + this + "'" + expected + "'", "UIAutomation-RegexMatcher");

            return Regex.IsMatch(actual, expected);
        }

        /// <summary>
        ///     Returns the string representation of this match.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString() {
            return "=~";
        }

    }

}
