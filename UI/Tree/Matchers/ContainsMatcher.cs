namespace Automation.UI.Tree.Matchers {

    /// <summary>
    ///     A matcher that compares two strings to see if the actual value contains the expected value.
    /// </summary>
    internal class ContainsMatcher : StringMatcher {

        /// <summary>
        ///     Checks to see if the actual value contains the specified expected value.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <param name="expected">The expected start value.</param>
        /// <returns>True if the values match.</returns>
        public override bool IsMatch(string actual, string expected) {
            WriteTrace(actual, expected);

            return actual.Contains(expected);
        }

        /// <summary>
        ///     Returns the string representation of this match.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString() {
            return "=Contains->";
        }

    }

}
