namespace Automation.UI.ElementFinder.Matchers {

    /// <summary>
    ///     A matcher that compares two strings to see if they are equal.
    /// </summary>
    internal class ExactMatch : StringMatcher {

        /// <summary>
        ///     Checks to see if the actual value is equal to the expected value.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <param name="expected">The expected start value.</param>
        /// <returns>True if the values match.</returns>
        public override bool IsMatch(string actual, string expected) {
            return actual.Equals(expected);
        }

        /// <summary>
        ///     Returns the string representation of this match.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString() {
            return "=";
        }

    }

}
