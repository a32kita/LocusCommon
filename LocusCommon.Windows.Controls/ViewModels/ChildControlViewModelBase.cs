using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LocusCommon.Windows.ViewModels
{
    /// <summary>
    /// IChildControlViewModel の実装を抽象クラスとして提供します。
    /// </summary>
    public abstract class ChildControlViewModelBase : ExtendedViewModelBase, IChildControlViewModel
    {
        // 公開プロパティ

        /// <summary>
        /// このビューモデルでバインドのターゲットとなる UIElement を取得します。
        /// </summary>
        public UIElement Target
        {
            get => this.GetBindingValue<UIElement>(nameof(this.Target));
            set => this.SetBindingValue(nameof(this.Target), value);
        }


        // コンストラクタ

        /// <summary>
        /// 新しい ChildControlViewModelBase クラスのインスタンスを初期化します。
        /// </summary>
        public ChildControlViewModelBase()
        {
            this.Target = null;
        }
    }
}
