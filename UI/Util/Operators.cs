using Automation.UI.Tree.Operators;

namespace Automation.UI.Util {

    /// <summary>
    ///     Supported operators.
    /// </summary>
    internal static class Operators {

        /// <summary>
        ///     Operator that joins conditions using a logical "and".
        /// </summary>
        internal static readonly IBinaryOperator And = new AndOperator();

        /// <summary>
        ///     Operator that joins conditions using a logical "or".
        /// </summary>
        internal static readonly IBinaryOperator Or = new OrOperator();

        /// <summary>
        ///     Operator that applies a logical "not" to a condition.
        /// </summary>
        internal static readonly IUnaryOperator Not = new NotOperator();

    }

}
