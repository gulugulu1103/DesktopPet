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

namespace DesktopPet.Views
{
    /// <summary>
    /// RoundButton.xaml 的交互逻辑
    /// </summary>
    public partial class RoundButton : Button
    {
        public RoundButton()
        {
            InitializeComponent();
        }



        public SolidColorBrush AccentColor
        {
            get { return (SolidColorBrush)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AccentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AccentColorProperty =
            DependencyProperty.Register("AccentColor", typeof(SolidColorBrush), typeof(RoundButton));


    }
}
