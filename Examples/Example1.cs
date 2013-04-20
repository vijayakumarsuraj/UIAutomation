using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace Automation.UI.Examples {

    public class Example1 {

        public static void Main(string[] args) {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            var example = new Example1();
            example.QueryBuilderExample();
        }

        public void QueryBuilderExample() {
            var root = AutomationElement.RootElement;
            var titleBar = UITree.Query(root)
                // Query 1.
                                 .UsingTreeWalkerEngine().FindChildren().Where()
                                 .NameContains("Microsoft Visual Studio").And().TypeIs(ControlType.Window)
                                 .Select()
                // Use the first result of query 1.
                                 .FirstResultThenQuery()
                // Query 2
                                 .UsingTreeWalkerEngine().FindChildren().Where()
                                 .TypeIs(ControlType.TitleBar)
                                 .Select()
                // Return the first result of query 2.
                                 .FirstResult();

            Console.WriteLine(titleBar.Current.Name);
        }

    }

}
