using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

using LocusCommon.Windows.ViewModels;

namespace LocusCommon.Windows.Controls.InternalViewModels
{
    /// <summary>
    /// 
    /// </summary>
#if Release
    [EditorBrowsable(EditorBrowsableState.Never)]
#endif
    public class ImageButtonViewModel : ExtendedViewModelBase
    {
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
            this.BorderThickness = new Thickness(1);
            this.BorderBrush = Brushes.Transparent;
            this.Text = "Button";
            this.FontSize = 14;
        }
    }
}
