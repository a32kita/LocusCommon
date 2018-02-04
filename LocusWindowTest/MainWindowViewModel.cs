using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using LocusCommon.Windows.ViewModels;

namespace LocusWindowTest
{
    /// <summary>
    /// <see cref="MainWindow"/> に対する ViewModel を定義します。
    /// </summary>
    public class MainWindowViewModel : ExtendedViewModelBase
    {
        // 非公開フィールド


        // 公開プロパティ


        // 公開プロパティ :: バインディング

        /// <summary>
        /// 
        /// </summary>
        public bool Binding_IsClosed
        {
            get => this.GetBindingValue<bool>();
            set => this.SetBindingValueQuick(value);
        }


        // 公開プロパティ :: コマンド

        /// <summary>
        /// 
        /// </summary>
        public ICommand CloseButtonCommand
        {
            get => this.GetBindingValue<ICommand>();
            private set => this.SetBindingValueQuick(value);
        }


        // コンストラクタ

        /// <summary>
        /// 新しい <see cref="MainWindowViewModel"/> クラスのインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            this.CloseButtonCommand = new DelegateCommand(param => this.Binding_IsClosed = true);

            this.PropertyChanged += MainWindowViewModel_PropertyChanged;
        }


        // 非公開メソッド
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Binding_IsClosed):
                    if (this.Binding_IsClosed == false)
                        return;
                    System.Windows.MessageBox.Show("Closed!!");
                    break;
            }
        }

    }
}
