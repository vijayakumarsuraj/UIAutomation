using System.Text.RegularExpressions;
using System.Windows.Automation;

using Automation.UI.Tree.QueryParts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.UI.Tests {

    /// <summary>
    /// Tests the various condition matchers.
    /// </summary>
    [TestClass]
    public class TestConditionMatchers {

        /// <summary>
        ///     Gets a query set to search children using a tree walker engine.
        /// </summary>
        /// <returns></returns>
        private QueryWherePart Query() {
            return UITree.Query(AutomationElement.RootElement).UsingTreeWalkerEngine().FindChildren();
        }
        
        /// <summary>
        ///     Tests the 'Contains' matcher.
        /// </summary>
        [TestMethod]
        public void TestContainsMatcher() {
            const string nameToSearch = "Microsoft Visual";
            // Fire the query.
            var window = Query().Where()
                .Name().Contains(nameToSearch)
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);
            Assert.IsTrue(window.Element.Current.Name.Contains(nameToSearch));
        }

        /// <summary>
        ///     Tests the 'EndsWith' matcher.
        /// </summary>
        [TestMethod]
        public void TestEndsWithMatcher() {
            const string nameToSearch = "Visual Studio";
            // Fire the query.
            var window = Query().Where()
                .Name().EndsWith(nameToSearch)
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);
            Assert.IsTrue(window.Element.Current.Name.EndsWith(nameToSearch));
        }
        
        /// <summary>
        ///     Tests the 'Regex' matcher.
        /// </summary>
        [TestMethod]
        public void TestRegexMatcher() {
            const string nameToSearch = @"[\w]+ - Microsoft Visual Studio";
            // Fire the query.
            var window = Query().Where()
                .Name().Matches(nameToSearch)
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);
            Assert.IsTrue(new Regex(nameToSearch).IsMatch(window.Element.Current.Name));
        }

        /// <summary>
        ///     Tests the 'StartsWith' matcher.
        /// </summary>
        [TestMethod]
        public void TestStartsWithMatcher() {
            const string nameToSearch = "UIAutomation";
            // Fire the query.
            var window = Query().Where()
                .Name().StartsWith(nameToSearch)
                .Select().FirstResult();
            // Validate the result.
            Assert.IsNotNull(window);
            Assert.IsTrue(window.Element.Current.Name.StartsWith(nameToSearch));
        }

    }

}
