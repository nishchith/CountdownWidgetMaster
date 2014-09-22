using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using System.Collections;
using System.Collections.Specialized;

namespace GenericCountdown.Commons
{
    public class SmartListPicker : ListPicker
    {
        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        public new IList SelectedItems
        {
            get
            {
                return (IList)GetValue(SelectedItemsProperty);
            }
            set
            {
                base.SetValue(SelectedItemsProperty, value);
            }
        }

    }
}
