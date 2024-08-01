using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Controls
{
    public class ImageButton : Button
    {
        bool valuesChangedBeforeLoaded = false;
        Image imgBlock = null;
       
        public ImageButton()
        {
            this.Loaded += ButtonWithTextAndImage_Loaded;
            this.IsVisibleChanged += BaseButtonWithTextAndImage_IsVisibleChanged;
        }

        private void BaseButtonWithTextAndImage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void ButtonWithTextAndImage_Loaded(object sender, RoutedEventArgs e)
        {
            if (valuesChangedBeforeLoaded)
            {
                Update();
            }
        }

        void Update()
        {
           
            if (null != this.NormalIcon)
                this.UpdateImage(this.NormalIcon);

        }

        /// <summary>
        /// Tempate objs can only be get after loading template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            imgBlock = (Image)this.Template.FindName("PART_btnImage", this);
         
            if (valuesChangedBeforeLoaded)
            {
                Update();
            }
        }

        
        public static readonly DependencyProperty NormalIconProperty = DependencyProperty.Register("NormalIcon", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null, new PropertyChangedCallback(OnNormalIconChanged)));
        public ImageSource NormalIcon
        {
            get { return (ImageSource)GetValue(NormalIconProperty); }
            set { SetValue(NormalIconProperty, value); }
        }
        private static void OnNormalIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ImageButton btn = d as ImageButton;
            btn.valuesChangedBeforeLoaded = true;
            btn.UpdateImage((ImageSource)args.NewValue);
        }

        private void UpdateImage(ImageSource newValue)
        {
            if (this.IsLoaded && null != imgBlock)
            {
                imgBlock.Source = newValue;

            }
        }
    }
}
