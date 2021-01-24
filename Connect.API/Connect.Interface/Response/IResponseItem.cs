using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Response
{
    public interface IResponseItem<T>
    {
        /// <summary>
        /// Item collection
        /// </summary>
        List<T> Items { get; set; }
        /// <summary>
        /// Number of Item in the system
        /// </summary>
        int NumberOfItem { get; }
        /// <summary>
        /// Number of Item in the current response
        /// </summary>
        int NumberOfCurrentItem { get; }
    }
}
