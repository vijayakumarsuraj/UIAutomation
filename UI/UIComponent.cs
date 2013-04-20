using System.Windows.Automation;

using Automation.UI.Tree;
using Automation.UI.Tree.QueryParts;

namespace Automation.UI {

    /// <summary>
    ///     The base class for all UI elements.
    ///     This class provides a convenient wrapper for interacting with the UI automation elements.
    /// </summary>
    public class UIComponent {

        /// <summary>
        ///     New UI component.
        /// </summary>
        /// <param name="element">The underlying element wrapped by this component.</param>
        public UIComponent(AutomationElement element) {
            Element = element;
        }

        /// <summary>
        ///     The underlying element wrapped by this component.
        /// </summary>
        public AutomationElement Element { get; protected set; }

        /// <summary>
        ///     Construct a new query for searching under this component.
        /// </summary>
        /// <returns>The engine part of the query.</returns>
        public QueryEnginePart Query() {
            return new QueryEnginePart(new Query(Element));
        }

    }

}
