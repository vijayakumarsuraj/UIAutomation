using System.Diagnostics;

namespace Automation.UI.Tree {

    /// <summary>
    ///     Represents a matcher that works with string objects.
    /// </summary>
    internal abstract class StringMatcher : Matcher<string> {

        /// <summary>
        ///     Writes a trace message.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <param name="expected">The expected start value.</param>
        protected override void WriteTrace(string actual, string expected) {
            Trace.WriteLine("Checking '" + actual + "'" + this + "'" + expected + "'", "UIAutomation-" + GetType().Name);
        }

    }

}
