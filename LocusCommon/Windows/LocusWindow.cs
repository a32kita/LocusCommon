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
        
        public bool IsClosed
        {
            get { return (bool)GetValue(IsClosedProperty); }
            set { SetValue(IsClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClosed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.Register("IsClosed", typeof(bool), typeof(LocusWindow), new PropertyMetadata(false, (d, e) =>
            {
                var instance = (LocusWindow)d;
                if ((bool)e.NewValue)
                {
                    try
                    {
                        instance.Close();
                    }
                    catch { }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }));




        // コンストラクタ

        /// <summary>
        /// 新しい <see cref="LocusWindow"/> クラスのインスタンスを初期化します。
        /// </summary>
        public LocusWindow()
        {
            this.Loaded += LocusWindow_Loaded;
            this.Closed += LocusWindow_Closed;
        }


        // 非公開メソッド

        private void LocusWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.IsClosed = false;
        }

        private void LocusWindow_Closed(object sender, EventArgs e)
        {
            this.IsClosed = true;
        }
    }
}

#endif