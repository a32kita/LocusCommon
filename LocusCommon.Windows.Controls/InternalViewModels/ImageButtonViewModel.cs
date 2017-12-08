using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

using LocusCommon.Windows.ViewModels;

namespace LocusCommon.Windows.Controls.InternalViewModels
{
    using self = ImageButtonViewModel;

    /// <summary>
    /// 
    /// </summary>
#if DEBUG == false
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
#endif
    public class ImageButtonViewModel : ExtendedViewModelBase
    {
        // 非公開静的フィールド
        private static Brush backgroundBrush;
        private static Brush borderBrush;
        private static Brush mouseOverColor;


        // 公開プロパティ

        /// <summary>
        /// Image用のImageSourceを取得または設定します．
        /// </summary>
        public ImageSource ImageSource
        {
            get => this.GetBindingValue<ImageSource>(nameof(this.ImageSource));
            set => this.SetBindingValue(nameof(this.ImageSource), value);
        }

        /// <summary>
        /// Imageのストレッチを取得または設定します．
        /// </summary>
        public Stretch ImageStretch
        {
            get => this.GetBindingValue<Stretch>(nameof(this.ImageStretch));
            set => this.SetBindingValue(nameof(this.ImageStretch), value);
        }

        /// <summary>
        /// コントロールの Border の太さを示す Thickness を取得または設定します．
        /// </summary>
        public Thickness BorderThickness
        {
            get => this.GetBindingValue<Thickness>(nameof(this.BorderThickness));
            set => this.SetBindingValue(nameof(this.BorderThickness), value);
        }

        /// <summary>
        /// コントロールの Border の色を示す Brush を取得または設定します．
        /// </summary>
        public Brush BorderBrush
        {
            get => this.GetBindingValue<Brush>(nameof(this.BorderBrush));
            set => this.SetBindingValue(nameof(this.BorderBrush), value);
        }

        /// <summary>
        /// コントロールの背景色を表す Brush を取得または設定します。
        /// </summary>
        public Brush Background
        {
            get => this.GetBindingValue<Brush>(nameof(this.Background));
            set => this.SetBindingValue(nameof(this.Background), value);
        }

        /// <summary>
        /// マウスカーソルがコントロール上にあるときに重ねる色を表す Brush を取得または設定します。
        /// </summary>
        public Brush MouseOverColor
        {
            get => this.GetBindingValue<Brush>(nameof(this.MouseOverColor));
            set => this.SetBindingValue(nameof(this.MouseOverColor), value);
        }

        /// <summary>
        /// ハイライトパネルの色を表す Brush を取得または設定します。
        /// </summary>
        public Brush HilightPanelBrush
        {
            get => this.GetBindingValue<Brush>(nameof(this.HilightPanelBrush));
            set => this.SetBindingValue(nameof(this.HilightPanelBrush), value);
        }

        /// <summary>
        /// ボタンに表示するテキストを取得または設定します．
        /// </summary>
        public string Text
        {
            get => this.GetBindingValue<string>(nameof(this.Text));
            set => this.SetBindingValue(nameof(this.Text), value);
        }

        /// <summary>
        /// テキストに適用する FontSize を示 double 型の値を取得または設定します．
        /// </summary>
        public double FontSize
        {
            get => this.GetBindingValue<double>(nameof(this.FontSize));
            set => this.SetBindingValue(nameof(this.FontSize), value);
        }


        // コンストラクタ

        /// <summary>
        /// 新しい ImageButtonViewModel クラスのインスタンスを初期化します．
        /// </summary>
        public ImageButtonViewModel()
        {
            this.ImageSource = null;
            this.ImageStretch = Stretch.Uniform;
            this.BorderThickness = new Thickness(1);
            this.BorderBrush = Brushes.Transparent;
            this.Background = self.backgroundBrush;
            this.MouseOverColor = self.mouseOverColor;
            this.HilightPanelBrush = new SolidColorBrush(Color.FromArgb(60, 255, 255, 255));
            this.Text = "Button";
            this.FontSize = 14;
        }


        // 静的コンストラクタ

        /// <summary>
        /// ImageButtonViewModel クラスを初期化します。
        /// </summary>
        static ImageButtonViewModel()
        {
            self.backgroundBrush = new SolidColorBrush(Color.FromArgb(100, 150, 150, 150));
            self.borderBrush = new SolidColorBrush(Color.FromArgb(100, 200, 200, 200));
            self.mouseOverColor = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
        }
    }
}
