using System.Windows.Automation;

namespace Automation.UI.Tree.Operators {

    /// <summary>
    ///     Represents a binary operator that can be applied to two conditions.
    /// </summary>
    internal interface IBinaryOperator : IOperator {

        /// <summary>
        ///     Joins the two conditions using this operator.
        /// </summary>
        /// <param name="left">The left hand condition.</param>
        /// <param name="right">The right hand condition.</param>
        /// <returns>The new condition.</returns>
        Condition Join(Condition left, Condition right);

    }

}
