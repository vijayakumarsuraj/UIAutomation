using System.Windows.Automation;

using Automation.UI;
using Automation.UI.Tree.QueryParts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UI.Tests {

    /// <summary>
    /// Tests the various methods of the query interface.
    /// </summary>
    [TestClass]
    public class TestQuery {

        /// <summary>
        ///     Gets a query set to search children using a tree walker engine.
        /// </summary>
        /// <returns></returns>
        private QueryWherePart Query() {
            return UITree.Query(AutomationElement.RootElement).UsingTreeWalkerEngine().FindChildren();
        }

        /// <summary>
        ///     Test the 'AllResults' method.
        /// </summary>
        [TestMethod]
        public void TestAllResults() {
            // Fire the query.
            var windows = Query().Where()
                .Name().StartsWith("UIAutomation")
                .And()
                .Name().EndsWith("Visual Studio")
                .Select().AllResults();
            // Validate the result.
            Assert.IsNotNull(windows);
            Assert.IsTrue(windows.Count > 0);
        }

    }

}
