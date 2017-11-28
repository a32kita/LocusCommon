using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using LocusCommon.Windows.ViewModels;

namespace GuiTestWPF.Net45
{
    /// <summary>
    /// MainWindow のビューモデルを定義します。
    /// </summary>
    public class MainWindowViewModel : ExtendedViewModelBase
    {
        // 非公開フィールド

        private DelegateCommand alphaTestBlockButtonCommand;


        // 公開プロパティ :: バインディングプロパティ

        /// <summary>
        /// 
        /// </summary>
        public bool AlphaTestBlockCheckBoxChecked
        {
            get => this.GetBindingValue<bool>(nameof(this.AlphaTestBlockCheckBoxChecked));
            set => this.SetBindingValue(nameof(this.AlphaTestBlockCheckBoxChecked), value);
        }

        /// <summary>
        /// 
        /// </summary>
        public IChildControlViewModel BravoTestBlockChildViewModel
        {
            get => this.GetBindingValue<IChildControlViewModel>(nameof(this.BravoTestBlockChildViewModel));
            set => this.SetBindingValue(nameof(this.BravoTestBlockChildViewModel), value);
        }


        // 公開プロパティ :: コマンド

        /// <summary>
        /// AlphaTestBlock の Button 用のコマンドを取得します。
        /// </summary>
        public ICommand AlphaTestBlockButtonCommand
        {
            get => this.alphaTestBlockButtonCommand;
        }


        // コンストラクタ

        /// <summary>
        /// 新しい MainWindowViewModel クラスのインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            var alphaTestBlockButtonCommandParam = new DelegateCommandInitializeParameter()
            {
                Command = param => MessageBox.Show("hello, world!!"),
                CanExecute = param => this.AlphaTestBlockCheckBoxChecked
            };

            this.alphaTestBlockButtonCommand = new DelegateCommand(alphaTestBlockButtonCommandParam);
            this.BravoTestBlockChildViewModel = new TestChildViewModel();
        }


        // 内部クラス

        private class TestChildViewModel : ChildControlViewModelBase
        {
            public TestChildViewModel()
            {
                this.Target = new Grid()
                {
                    Background = Brushes.Blue,
                    Height = 10
                };
            }
        }
    }
}
