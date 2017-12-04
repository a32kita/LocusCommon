﻿using System;
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

namespace LocusCommon.Windows.Controls.BasicMaterials
{
    using self = HilightPanel;

    /// <summary>
    /// HilightPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class HilightPanel : UserControl
    {
        // 依存関係プロパティ
        

        public Brush NormalBrush
        {
            get { return (Brush)GetValue(NormalBrushProperty); }
            set { SetValue(NormalBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NormalBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalBrushProperty =
            DependencyProperty.Register("NormalBrush", typeof(Brush), typeof(self), new PropertyMetadata(Brushes.Transparent));



        public Brush HilightBrush
        {
            get { return (Brush)GetValue(HilightBrushProperty); }
            set { SetValue(HilightBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HilightBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HilightBrushProperty =
            DependencyProperty.Register("HilightBrush", typeof(Brush), typeof(self), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 255, 255, 255))));
        

        // コンストラクタ

        /// <summary>
        /// 新しい HilightPanel クラスのインスタンスを初期化します。
        /// </summary>
        public HilightPanel()
        {
            InitializeComponent();

            //this.MainRectangle.Fill = this.NormalBrush;
            this.MouseEnter += (sender, e) => this._setMainRectangleBrush(this.HilightBrush);
            this.MouseLeave += (sender, e) => this._setMainRectangleBrush(this.NormalBrush);
        }


        // 非公開メソッド

        private int count = 0;
        private void _setMainRectangleBrush(Brush brush)
        {
            this.MainRectangle.Fill = brush;

#if DEBUG
            if (this.HilightBrush == this.NormalBrush)
                Console.Error.WriteLine("Warning: this.HilightBrush == this.NormalBrush");

            string brushName = "undefined";
            if (brush == this.HilightBrush)
                brushName = "hilight";
            else if (brush == this.NormalBrush)
                brushName = "normal";
            Console.Error.WriteLine("Color changed ({0}, {1})", ++count, brushName);
#endif
        }


        // 静的コンストラクタ

        /// <summary>
        /// HilightPanel クラスのインスタンスを初期化します。
        /// </summary>
        static HilightPanel()
        {
        }
    }
}