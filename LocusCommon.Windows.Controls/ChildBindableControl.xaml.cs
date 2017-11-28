using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LocusCommon.Windows.ViewModels;

namespace LocusCommon.Windows.Controls
{
    using self = ChildBindableControl;

    /// <summary>
    /// ChildBindableControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ChildBindableControl : UserControl
    {
        // 依存関係プロパティ

        public IChildControlViewModel ChildControl
        {
            get { return (IChildControlViewModel)GetValue(ChildControlProperty); }
            set { SetValue(ChildControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChildControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildControlProperty =
            DependencyProperty.Register("ChildControl", typeof(IChildControlViewModel), typeof(self), new PropertyMetadata(null, self.ChildControlPropertyChanged));
        
        private static void ChildControlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (self)d;
            var oldValue = (IChildControlViewModel)e.OldValue;
            var newValue = (IChildControlViewModel)e.NewValue;
            
            if (oldValue?.Target != null)
                instance.MainGrid.Children.Remove(oldValue.Target);

            if (newValue?.Target != null)
                instance.MainGrid.Children.Add(newValue.Target);
        }


        // コンストラクタ

        /// <summary>
        /// 新しい ChildBindableControl クラスのインスタンスを初期化します。
        /// </summary>
        public ChildBindableControl()
        {
            InitializeComponent();
        }
    }
}
