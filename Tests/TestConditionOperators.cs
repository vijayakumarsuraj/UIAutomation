using System.Windows.Automation;

using Automation.UI;
using Automation.UI.Tree.QueryParts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UI.Tests {
    
    /// <summary>
    /// Tests the various condition operators.
    /// </summary>
    [TestClass]
    public class TestConditionOperators {

        /// <summary>
        ///     Gets a query set to search children using a tree walker engine.
        /// </summary>
        /// <returns></returns>
        private QueryWherePart Query() {
            return UITree.Query(AutomationElement.RootElement).UsingTreeWalkerEngine().FindChildren();
        }

        /// <summary>
        ///     Tests the 'And' condition.
        /// </summary>
        [TestMethod]
        public void TestAndCondition() {
            const string nameToSearch1 = "UIAutomation";
            const string nameToSearch2 = "Visual Studio";
            // Fire the query.
            var window = Query().Where()
                .Name().StartsWith(nameToSearch1)
                .And()
                .Name().EndsWith(nameToSearch2)
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);
            var currentName = window.Element.Current.Name;
            Assert.IsTrue(currentName.StartsWith(nameToSearch1) && currentName.EndsWith(nameToSearch2));
        }

        /// <summary>
        ///     Tests the 'Not' condition.
        /// </summary>
        [TestMethod]
        public void TestNotCondition() {
            const string nameToSearch = "Visual Studio";
            // Fire the query.
            var window = Query().Where()
                .Not().Name().StartsWith(nameToSearch)
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);
            Assert.IsFalse(window.Element.Current.Name.StartsWith(nameToSearch));
        }

        /// <summary>
        ///     Tests the 'Or' condition.
        /// </summary>
        [TestMethod]
        public void TestOrCondition() {
            const string nameToSearch1 = "UIAutomation";
            const string nameToSearch2 = "Visual Studio";
            // Fire the query.
            var window = Query().Where()
                .Name().StartsWith(nameToSearch1)
                .Or()
                .Name().EndsWith(nameToSearch2)
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);
            var currentName = window.Element.Current.Name;
            Assert.IsTrue(currentName.StartsWith(nameToSearch1) || currentName.EndsWith(nameToSearch2));
        }

    }

}
