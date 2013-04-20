using System.Windows.Automation;

namespace Automation.UI.Tree.Operators {

    /// <summary>
    ///     Represents a binary operator that can join two conditions using a logical "or".
    /// </summary>
    internal class OrOperator : IBinaryOperator {

        /// <summary>
        ///     Joins the two conditions using an OrCondition.
        /// </summary>
        /// <param name="left">The left hand condition.</param>
        /// <param name="right">The right hand condition.</param>
        /// <returns>The new condition.</returns>
        public Condition Join(Condition left, Condition right) {
            return new OrCondition(left, right);
        }

    }

}
