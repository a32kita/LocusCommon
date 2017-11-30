using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LocusCommon.Windows.Controls.InternalBehaviors
{
    using self = ImageButtonBehavior;

    /// <summary>
    /// ImageButtonで使用するビヘイビアを定義します。
    /// </summary>
    public class ImageButtonBehavior : DependencyObject
    {
        // 依存関係プロパティ

        /// <summary>
        /// 現在マウスがコントロール上にあるかどうかを示す値を取得します。
        /// </summary>
        public bool IsMouseOver
        {
            get { return (bool)GetValue(IsMouseOverProperty); }
            set { SetValue(IsMouseOverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMouseOver.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.Register("IsMouseOver", typeof(bool), typeof(self), new PropertyMetadata(false));


    }

    
}
