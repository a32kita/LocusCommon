using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace LocusCommon.Windows.ViewModels
{
    /// <summary>
    /// ChildBindableControl のビューモデルとして利用可能な型であることを保証します。
    /// </summary>
    public interface IChildControlViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// このビューモデルでバインドのターゲットとなるコントロールを取得します。
        /// </summary>
        UIElement Target
        {
            get;
        }
    }
}
