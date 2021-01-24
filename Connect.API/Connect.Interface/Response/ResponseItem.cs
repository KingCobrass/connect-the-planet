using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Response
{
    /// <summary>
    /// Generic items model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseItem<T> : IResponseItem<T>
    {
        public ResponseItem()
        {
            this.Items = new List<T>();
        }

        public ResponseItem(T t)
        {
            this.Items = new List<T>();
            this.Items.Add(t);
        }

        public ResponseItem(List<T> ts)
        {
            this.Items = ts;
        }
        /// <summary>
        /// Item collection
        /// </summary>
        public List<T> Items { get; set; }
        /// <summary>
        /// Number of Item in the system
        /// </summary>
        public int NumberOfItem
        {
            get
            {
                return Items.Count;
            }
        }
        /// <summary>
        /// Number of Item in the current response
        /// </summary>
        public int NumberOfCurrentItem
        {
            get
            {
                return Items.Count;
            }
        }
    }
}
