UI Automation
============

A simple wrapper around Microsoft's UI Automation library.



Using the query builder
-----------------------


#### Using separate queries

    var root = AutomationElement.RootElement;
    var window = UITree.Query(root).UsingTreeWalkerEngine()
        // Query 1.
        .FindChildren().Where()
            .NameContains("Microsoft Visual Studio")
            .And()
            .TypeIs(ControlType.Window)
        .Select().FirstResult()
        // Get the underlying automation element of query 1.
        .Element;

    var titleBar = UITree.Query(window).UsingDefaultEngine()
        // Query 2
        .FindChildren().Where()
            .TypeIs(ControlType.TitleBar)
        .Select().FirstResult()
        // Get the underlying automation element of query 2.
        .Element;


### Using continuous queries

    var root = AutomationElement.RootElement;
    var titleBar = UITree
        .Query(root).UsingTreeWalkerEngine()
        // Query 1.
        .FindChildren().Where()
            .NameContains("Microsoft Visual Studio")
            .And()
            .TypeIs(ControlType.Window)
        .Select().FirstResult()
            // Continue with another query using the first result of query 1.
            .Query().UsingDefaultEngine()
            // Query 2
            .FindChildren().Where()
                .TypeIs(ControlType.TitleBar)
            .Select().FirstResult()
        // Get the underlying automation element of query 2.
        .Element;



Using UI Automation's patterns
------------------------------


### Executing commands with automation patterns

    var root = AutomationElement.RootElement;
    var window = UITree.Query(root).UsingTreeWalkerEngine()
        // Query 1.
        .FindChildren().Where()
        .NameContains("Microsoft Visual Studio").And().TypeIs(ControlType.Window)
        .Select().FirstResult();

    // Execute using the 'Window' pattern.
    window.Execute<WindowPattern>(p => p.SetWindowVisualState(WindowVisualState.Minimized));

