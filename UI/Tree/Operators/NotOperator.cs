using System.Windows.Automation;

namespace Automation.UI.Tree.Operators {

    /// <summary>
    ///     Represents a unary operator that applies a logical "not" to a condition.
    /// </summary>
    internal class NotOperator : IUnaryOperator {

        /// <summary>
        ///     Applies the "not" operator on this condition and returns a new condition.
        /// </summary>
        /// <param name="condition">The condition to apply the operator on.</param>
        /// <returns>The "not" condition.</returns>
        public Condition Apply(Condition condition) {
            return new NotCondition(condition);
        }

    }

}
