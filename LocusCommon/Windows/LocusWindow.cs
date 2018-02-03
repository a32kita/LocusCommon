#if NET35||NET40||NET45||NET461

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace LocusCommon.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public class LocusWindow : Window
    {
        // 非公開フィールド



        // 公開プロパティ


        // 依存関係プロパティ
        
        public bool DoClose
        {
            get { return (bool)GetValue(DoCloseProperty); }
            set { SetValue(DoCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DoClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoCloseProperty =
            DependencyProperty.Register("DoClose", typeof(bool), typeof(LocusWindow), new PropertyMetadata(false, (d, e) =>
            {
            }));

        
        // コンストラクタ

        /// <summary>
        /// 新しい <see cref="LocusWindow"/> クラスのインスタンスを初期化します。
        /// </summary>
        public LocusWindow()
        {
            
        }


        // 非公開メソッド
    }
}

#endif