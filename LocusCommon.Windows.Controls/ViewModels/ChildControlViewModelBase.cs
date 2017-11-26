using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// このビューモデルでバインドのターゲットとなるコントロールを取得します。
        /// </summary>
        public Control Target
        {
            get => this.GetBindingValue<Control>(nameof(this.Target));
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
