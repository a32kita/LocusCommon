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

using LocusCommon.Windows.Controls.InternalViewModels;

namespace LocusCommon.Windows.Controls
{
    using self = ImageButton;

    /// <summary>
    /// ImageButton.xaml の相互作用ロジック
    /// </summary>
    public partial class ImageButton : UserControl
    {
        // 限定公開プロパティ

        /// <summary>
        /// 現在の DataContext を ImageButtonViewModel として取得します．
        /// </summary>
        public ImageButtonViewModel ViewModel
        {
            get => this.DataContext as ImageButtonViewModel;
        }


        // 依存関係プロパティ

        /// <summary>
        /// ImageSourceを取得または設定します．
        /// </summary>
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(self), new PropertyMetadata(null, self.OnImageSourcePropertyChanged));
        
        private static void OnImageSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vm = self.getViewModel(d);
            if (vm != null)
                vm.ImageSource = (ImageSource)e.NewValue;
        }


        /// <summary>
        /// 表示するテキストを取得または設定します．
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(self), new PropertyMetadata("Button", self.OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vm = self.getViewModel(d);
            if (vm != null)
                vm.Text = (string)e.NewValue;
        }
        

        /// <summary>
        /// ハイライトパネルの色を表す Brush を取得または設定します。
        /// </summary>
        public Brush HilightPanelBrush
        {
            get { return (Brush)GetValue(HilightPanelBrushProperty); }
            set { SetValue(HilightPanelBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HilightPanelBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HilightPanelBrushProperty =
            DependencyProperty.Register("HilightPanelBrush", typeof(Brush), typeof(self), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(60, 255, 255, 255))));

        private static void OnHilightPanelBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vm = self.getViewModel(d);
            if (vm != null)
                vm.HilightPanelBrush = (Brush)e.NewValue;
        }



        // コンストラクタ

        /// <summary>
        /// 新しい ImageButton クラスのインスタンスを初期化します．
        /// </summary>
        public ImageButton()
        {
            // コンポーネントのロード
            InitializeComponent();
        }


        // 静的コンストラクタ

        /// <summary>
        /// ImageButton クラスのインスタンスを初期化します．
        /// </summary>
        static ImageButton()
        {
            // 依存関係プロパティのオーバーライド

            self.BorderThicknessProperty.OverrideMetadata(typeof(self), new FrameworkPropertyMetadata((d, e) =>
            {
                var vm = self.getViewModel(d);
                if (vm != null)
                    vm.BorderThickness = (Thickness)e.NewValue;
            }));

            self.BorderBrushProperty.OverrideMetadata(typeof(self), new FrameworkPropertyMetadata((d, e) =>
            {
                var vm = self.getViewModel(d);
                if (vm != null)
                    vm.BorderBrush = (Brush)e.NewValue;
            }));

            self.FontSizeProperty.OverrideMetadata(typeof(self), new FrameworkPropertyMetadata((d, e) =>
            {
                var vm = self.getViewModel(d);
                if (vm != null)
                    vm.FontSize = (double)e.NewValue;
            }));
        }


        // 非公開静的メソッド

        private static ImageButtonViewModel getViewModel(DependencyObject d)
        {
            return (d as self).Resources["ViewModelKey"] as ImageButtonViewModel;
        }
    }
}
