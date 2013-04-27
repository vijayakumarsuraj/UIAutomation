using System;
using System.Windows.Automation;

using Automation.UI.Tree;
using Automation.UI.Tree.QueryParts;
using Automation.UI.Util;

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

        /// <summary>
        ///     Executes the specified action using a pattern for this component.
        /// </summary>
        /// <typeparam name="T">The type of the pattern.</typeparam>
        /// <param name="actions">The actions to execute.</param>
        /// <returns>This UI component.</returns>
        public UIComponent Execute<T>(Action<T> actions) where T : BasePattern {
            actions(AutomationElementHelper.GetPattern<T>(Element));
            return this;
        }

        /// <summary>
        ///     Executes the specified action using a pattern for this component.
        /// </summary>
        /// <typeparam name="T">The type of the pattern.</typeparam>
        /// <param name="actions">The actions to execute.</param>
        /// <returns>This UI component.</returns>
        public UIComponent Execute<T>(Action<T, UIComponent> actions) where T : BasePattern {
            actions(AutomationElementHelper.GetPattern<T>(Element), this);
            return this;
        }

    }

}
