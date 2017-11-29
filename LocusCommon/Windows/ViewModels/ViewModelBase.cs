#if NET40||NET45||NET461

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LocusCommon.Windows.ViewModels
{
    /// <summary>
    /// ViewModel の基礎となるクラスを提供します。このクラスはインスタンス化できません。
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        // 非公開フィールド
        private Dictionary<string, string> errorMessages;

        private PropertyChangedEventHandler propertyChanged;


        // 公開イベント

        /// <summary>
        /// プロパティ値が変更されたことをクライアントに通知します。
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged
        {
            add => this.propertyChanged += value;
            remove => this.propertyChanged -= value;
        }


        // 公開プロパティ

        /// <summary>
        /// このオブジェクトに関する問題を示すエラー メッセージを取得します。
        /// </summary>
        public virtual string Error
        {
            get => this._getError();
        }


        // 公開プロパティ :: インターフェイスの実装

        /// <summary>
        /// 指定した名前を持つプロパティのエラー メッセージを取得します。
        /// </summary>
        /// <param name="index">エラー メッセージを取得するプロパティの名前。</param>
        /// <returns></returns>
        string IDataErrorInfo.this[string index]
        {
            get => this._getIndexer(index);
        }


        // コンストラクタ

        /// <summary>
        /// 新しい ViewModelBase クラスのインスタンスを初期化します。
        /// </summary>
        public ViewModelBase()
        {
            this.errorMessages = new Dictionary<string, string>();
            this.propertyChanged = null;
        }


        // 非公開メソッド

        private string _getError()
        {
            return this.errorMessages.Count > 0 ? "HasError" : null;
        }

        private string _getIndexer(string index)
        {
            if (this.errorMessages.ContainsKey(index))
                return this.errorMessages[index];
            return null;
        }


        // 限定公開メソッド

        /// <summary>
        /// プロパティが変化したことを通知します。
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            this.propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// エラーメッセージを設定します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        protected virtual void SetError(string propertyName, string errorMessage)
        {
            this.errorMessages[propertyName] = errorMessage;
        }

        /// <summary>
        /// エラーメッセージを削除します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected virtual void ClearError(string propertyName)
        {
            if (this.errorMessages.ContainsKey(propertyName))
                this.errorMessages.Remove(propertyName);
        }
    }
}

#endif