using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevTV.Core.UI.Dragging
{
    /// <summary>
    /// Acts both as a source and destination for dragging. If we are dragging
    /// between two containers then it is possible to swap items.
    /// </summary>
    /// <typeparam name="T">The type that represents the item being dragged.</typeparam>
    public interface IDragContainer<T> : IDragDestination<T>, IDragSource<T> where T : class
    {
    }
}