using System.Windows.Automation;

namespace Automation.UI.ElementFinder.Operators {

    /// <summary>
    ///     Represents a unary operator that can be applied to a single condition.
    /// </summary>
    internal interface IUnaryOperator : IOperator {

        /// <summary>
        ///     Applies the unary operator on this condition and returns a new condition.
        /// </summary>
        /// <param name="condition">The condition to apply the operator on.</param>
        /// <returns>The new condition.</returns>
        Condition Apply(Condition condition);

    }

}
