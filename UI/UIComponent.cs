using System;
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

        /// <summary>
        ///     Executes the specified action using a pattern for this component.
        /// </summary>
        /// <typeparam name="T">The type of the pattern.</typeparam>
        /// <param name="actions">The actions to execute.</param>
        /// <returns>This UI component.</returns>
        public UIComponent Execute<T>(Action<T> actions) where T : BasePattern {
            actions(GetPattern<T>());
            return this;
        }

        /// <summary>
        ///     Return an pattern for this component.
        /// </summary>
        /// <typeparam name="T">The type of the returned pattern.</typeparam>
        /// <returns>The required pattern.</returns>
        private T GetPattern<T>() where T : BasePattern {
            // First check if a field named 'Pattern' exists for the provided generic type.
            var patternType = typeof(T);
            var patternField = patternType.GetField("Pattern");
            if (patternField == null)
                throw new NotSupportedException("The pattern '" + patternType + "' is not supported");
            // Then get the value of this static field and use that to get the required pattern.
            var pattern = (AutomationPattern) patternField.GetValue(null);
            return (T) Element.GetCurrentPattern(pattern);
        }

    }

}
