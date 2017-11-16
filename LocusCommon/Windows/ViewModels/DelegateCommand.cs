using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LocusCommon.Windows.ViewModels
{
    /// <summary>
    /// 汎用のコマンドインターフェイスを実装するクラスを定義します．
    /// </summary>
    public class DelegateCommand : ICommand
    {
        // 非公開フィールド
        private Action<Object> command;
        private Func<Object, bool> canExecute;


        // 公開イベント

        /// <summary>
        /// コマンドを実行するかどうかに影響するような変更があった場合に発生します。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        // コンストラクタ

        /// <summary>
        /// 実行可能条件を指定して、新しい DelegateCommand クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        public DelegateCommand(Action<Object> command, Func<Object, bool> canExecute)
        {
            if (canExecute == null)
                canExecute = param => true;

            this.command = command;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// 新しい DelegateCommand クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="command"></param>
        public DelegateCommand(Action<Object> command)
            : this(command, null)
        {
            // 実装なし
        }


        // 公開メソッド

        /// <summary>
        /// 現在の状態でコマンドが実行可能かどうかを決定するメソッドです。コンストラクタの exexute パラメータにより定義されます。
        /// </summary>
        /// <param name="parameter">コマンドにより使用されるデータです。 コマンドにデータを渡す必要がない場合は、このオブジェクトを null に設定できます。</param>
        public void Execute(object parameter)
        {
            this.command(parameter);
        }

        /// <summary>
        /// コマンドが起動される際に呼び出すメソッドです。コンストラクタの canExexute パラメータにより定義されます。
        /// </summary>
        /// <param name="parameter">コマンドにより使用されるデータです。 コマンドにデータを渡す必要がない場合は、このオブジェクトを null に設定できます。</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }
    }
}
