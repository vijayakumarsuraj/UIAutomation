UI Automation
============

A simple wrapper around Microsoft's UI Automation library.



Using the query builder
-----------------------


#### Using separate queries

    var root = AutomationElement.RootElement;
    var window = UITree.Query(root)
        // Query 1.
        .UsingTreeWalkerEngine().FindChildren().Where()
        .NameContains("Microsoft Visual Studio").And().TypeIs(ControlType.Window)
        .Select().FirstResult()
        // Return the first result of query 1.
        .Element;

    var titleBar = UITree.Query(window)
        // Query 2
        .UsingTreeWalkerEngine().FindChildren().Where()
        .TypeIs(ControlType.TitleBar)
        .Select().FirstResult()
        // Return the first result of query 2.
        .Element;


### Using continuous queries

    var root = AutomationElement.RootElement;
    var titleBar = UITree
        .Query(root).UsingTreeWalkerEngine()
        // Query 1.
        .FindChildren().Where()
        .NameContains("Microsoft Visual Studio").And().TypeIs(ControlType.Window)
        .Select().FirstResult()
        // Continue with another query using the first result of query 1.
        .Query().UsingTreeWalkerEngine()
            // Query 2
            .FindChildren().Where()
            .TypeIs(ControlType.TitleBar)
            .Select().FirstResult()
            // Return the first result of query 2.
            .Element;
