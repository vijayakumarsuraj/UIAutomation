using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace Automation.UI.Examples {

    public class Example1 {

        public static void Main(string[] args) {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            var example = new Example1();
            example.QueryExample();
            example.QueryBuilderExample();
            example.PatternExample();
        }

        private void QueryExample() {
            var root = AutomationElement.RootElement;
            var window = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().FirstResult();

            var titleBar = UITree.Query(window).UsingDefaultEngine()
                // Query 2
                .FindChildren().Where()
                .Type().Is(ControlType.TitleBar)
                .Select().FirstResult();

            Console.WriteLine(titleBar.Element.Current.Name);
        }

        private void QueryBuilderExample() {
            var root = AutomationElement.RootElement;
            var titleBar = UITree
                .Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().FirstResult()
                // Continue with another query using the first result of query 1.
                .Query().UsingDefaultEngine()
                // Query 2
                .FindChildren().Where()
                .Type().Is(ControlType.TitleBar)
                .Select().FirstResult();

            Console.WriteLine(titleBar.Element.Current.Name);
        }

        private void PatternExample() {
            var root = AutomationElement.RootElement;
            var window = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 1.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().FirstResult();

            var windows = UITree.Query(root).UsingTreeWalkerEngine()
                // Query 2.
                .FindChildren().Where()
                .Name().Contains("Microsoft Visual Studio")
                .And()
                .Type().Is(ControlType.Window)
                .Select().AllResults();

            // Execute using the 'Window' pattern.
            window.Execute<WindowPattern>(p => p.SetWindowVisualState(WindowVisualState.Minimized));
            // Can also be called on a collection of UI components.
            windows.Execute<WindowPattern>((p, c) => p.SetWindowVisualState(WindowVisualState.Minimized));

            // Or, using the .Pattern() method (only available on single components).
            window.Pattern<WindowPattern>().SetWindowVisualState(WindowVisualState.Minimized);
        }

    }

}
