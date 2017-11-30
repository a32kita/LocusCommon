using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

using LocusCommon.Windows.Controls.InternalViewModels;

namespace LocusCommon.Windows.Controls.InternalBehaviors
{
    using self = ImageButtonBehavior;

    /// <summary>
    /// ImageButtonで使用するビヘイビアを定義します。
    /// </summary>
    public class ImageButtonBehavior : DependencyObject
    {
        // 依存関係プロパティ

        #region IsMouseOverProperty

        /// <summary>
        /// IsMouseOverProperty の値を取得します。
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool GetIsMouseOverProperty(DependencyObject d)
        {
            return (bool)d.GetValue(self.IsMouseOverProperty);
        }

        /// <summary>
        /// IsMouseOverProperty へ値を設定します。
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        public static void SetIsMouseOverProperty(DependencyObject d, bool value)
        {
            d.SetValue(self.IsMouseOverProperty, value);
        }

        /// <summary>
        /// 現在マウスがコントロール上にあるかどうかを示す値を取得または設定します。
        /// </summary>
        public static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.RegisterAttached("IsMouseOver", typeof(bool), typeof(self), new PropertyMetadata(false));

        private static void OnIsMouseOverPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as ImageButton;
            if (instance == null)
                return;

            var vm = self.getViewModel(instance);
            if (self.GetIsMouseOverProperty(d))
                // マウスオーバー状態
                vm.HilightPanelBrush = vm.MouseOverColor;
            else
                vm.HilightPanelBrush = Brushes.Transparent;
        }

        #endregion


        // コンストラクタ


        // 非公開静的メソッド

        /// <summary>
        /// 指定した ImageButton のインスタンスの DataContext を ImageButtonViewModel クラスのインスタンスとして取得します。
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        private static ImageButtonViewModel getViewModel(ImageButton instance)
        {
            return instance.DataContext as ImageButtonViewModel;
        }
    }

    
}
