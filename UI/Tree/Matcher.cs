namespace Automation.UI.Tree {

    /// <summary>
    ///     Represents a matcher that checks if an automation element matches a particular value.
    /// </summary>
    public abstract class Matcher<T> {

        /// <summary>
        ///     Only allow sub-classes.
        /// </summary>
        internal Matcher() {}

        /// <summary>
        ///     Checks if the specified actual and expected values match.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <returns>True if the values match.</returns>
        public abstract bool IsMatch(T actual, T expected);

        /// <summary>
        ///     Writes a trace message.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <param name="expected">The expected start value.</param>
        protected abstract void WriteTrace(string actual, string expected);

    }

}
