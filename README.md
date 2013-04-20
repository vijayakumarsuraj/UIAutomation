UI Automation
============

A simple wrapper around Microsoft's UI Automation library.


Using the query builder
-----------------------

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
