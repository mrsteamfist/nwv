using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NativeWebView.HTML.Base
{
    /// <summary>
    /// Event argument of a collection has changed
    /// Base off of <see cref="System.ComponentModel.PropertyChangedEventArgs"/>
    /// </summary>
    /// <typeparam name="T">The type of the collection</typeparam>
    public class CollectionChangedArgs<T> : PropertyChangedEventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="propertyName">Name of the property changed</param>
        public CollectionChangedArgs(String propertyName)
            : base(propertyName)
        {
            ItemsAdded = new List<T>();
            ItemsRemoved = new List<T>();
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="propertyName">Name of the property changed</param>
        /// <param name="itemsAdded">List of new items</param>
        public CollectionChangedArgs(String propertyName, IEnumerable<T> itemsAdded)
            : base(propertyName)
        {
            ItemsAdded = itemsAdded;
            ItemsRemoved = new List<T>();
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="propertyName">Name of the property changed</param>
        /// <param name="itemsAdded">Items added</param>
        /// <param name="itemsRemoved">Items removed</param>
        public CollectionChangedArgs(String propertyName, IEnumerable<T> itemsAdded, IEnumerable<T> itemsRemoved)
            : base(propertyName)
        {
            ItemsAdded = itemsAdded;
            ItemsRemoved = itemsRemoved;
        }
        /// <summary>
        /// List of items added to the collection changed
        /// </summary>
        public IEnumerable<T> ItemsAdded { get; set; }
        /// <summary>
        /// List of items removed from the collection changed
        /// </summary>
        public IEnumerable<T> ItemsRemoved { get; set; }
    }
}
