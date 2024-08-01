
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class SearchBox : ContentControl
    {
        public event EventHandler RefreshEvent;
      
        public SearchBox()
        {
            InitializeComponent();

        }
        public EventHandler eventHandler
        {
            get { return (EventHandler)GetValue(eventHandlerProperty); }
            set { SetValue(eventHandlerProperty, value); }
        }
        public static readonly DependencyProperty eventHandlerProperty = DependencyProperty.Register("eventHandler", typeof(EventHandler), typeof(SearchBox));

        public void Refresh()
        {
            InvokeRefresh(EventArgs.Empty);
        }

        private void InvokeRefresh(EventArgs e)
        {
            var handler = RefreshEvent;
            if (handler != null)
                handler(txtSearch.Text.Trim(), e);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshEvent += eventHandler;
            Refresh();
        }
    }
}
