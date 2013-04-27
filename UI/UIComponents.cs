using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Automation;

using Automation.UI.Util;

namespace Automation.UI {

    public class UIComponents : IEnumerable<UIComponent> {

        /// <summary>
        ///     New UI component collection.
        /// </summary>
        /// <param name="elements">The elements to wrap in UI components.</param>
        public UIComponents(IEnumerable elements) {
            Components = new List<UIComponent>();
            // Wrap each element in a UI component.
            foreach (AutomationElement element in elements)
                Components.Add(new UIComponent(element));

            Count = Components.Count;
        }

        /// <summary>
        ///     The components that make up this collection.
        /// </summary>
        private List<UIComponent> Components { get; set; }

        /// <summary>
        ///     Get the number of components in this collection.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///     Get an enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<UIComponent> GetEnumerator() {
            return Components.GetEnumerator();
        }

        /// <summary>
        ///     Get an enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        ///     Executes the specified action using a pattern for all components in this collection.
        /// </summary>
        /// <typeparam name="T">The type of the pattern.</typeparam>
        /// <param name="actions">The actions to execute.</param>
        /// <returns>This UI component collection.</returns>
        public UIComponents Execute<T>(Action<T, UIComponent> actions) where T : BasePattern {
            foreach (var component in Components)
                actions(AutomationElementHelper.GetPattern<T>(component.Element), component);

            return this;
        }

        /// <summary>
        ///     Executes the specified action using a pattern for all components in this collection.
        /// </summary>
        /// <typeparam name="T">The type of the pattern.</typeparam>
        /// <param name="actions">The actions to execute.</param>
        /// <returns>This UI component collection.</returns>
        public UIComponents Execute<T>(Action<T> actions) where T : BasePattern {
            foreach (var component in Components)
                actions(AutomationElementHelper.GetPattern<T>(component.Element));

            return this;
        }

    }

}
