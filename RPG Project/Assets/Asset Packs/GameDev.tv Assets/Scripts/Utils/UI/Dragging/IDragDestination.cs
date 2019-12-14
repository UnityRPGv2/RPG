using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevTV.Core.UI.Dragging
{
    /// <summary>
    /// Components that implement this interfaces can act as the destination for
    /// dragging a `DragItem`.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDragDestination<T> where T : class
    {
        /// <summary>
        /// How many of the given item can be accepted.
        /// </summary>
        /// <param name="item">The item type to potentially be added.</param>
        /// <returns>If there is no limit Int.MaxValue should be returned.</returns>
        int MaxAcceptable(T item);

        /// <summary>
        /// Update the UI and any data to reflect adding the item to this destination.
        /// </summary>
        /// <param name="item">The item type.</param>
        /// <param name="number">The quantity of items.</param>
        void AddItems(T item, int number);
    }
}