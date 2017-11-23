using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Windows.ViewModels
{
    /// <summary>
    /// DelegateCommand の初期化用パラメータクラスを提供します。
    /// </summary>
    public class DelegateCommandInitializeParameter
    {
        // 非公開フィールド
        private Action<Object> command;
        private Func<Object, bool> canExecute;


        // 公開プロパティ
        
        /// <summary>
        /// 
        /// </summary>
        public Action<Object> Command
        {
            get => this.command;
            set => this.command = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public Func<Object, bool> CanExecute
        {
            get => this.canExecute;
            set => this.canExecute = value;
        }


        // コンストラクタ

        /// <summary>
        /// 新しい DelegateCommandInitializeParameter クラスのインスタンスを初期化します。
        /// </summary>
        public DelegateCommandInitializeParameter()
        {
            this.command = null;
            this.canExecute = null;
        }
    }
}
