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
    /// このアプリケーションで使用するViewModelの基礎となるクラスを提供します。
    /// このクラスは継承専用で、直接のインスタンス化は出来ません。
    /// </summary>
    public abstract class ExtendedViewModelBase : ViewModelBase
    {
        // 非公開フィールド
        private Dictionary<string, Object> propertyValues;


        // コンストラクタ

        /// <summary>
        /// 新しい InternalViewModelBase クラスのインスタンスを初期化します。このコンストラクタは使用できません。
        /// </summary>
        public ExtendedViewModelBase()
        {
            this.propertyValues = new Dictionary<string, object>();
        }


        // 限定公開メソッド

        /// <summary>
        /// プロパティ辞書からプロパティの値を取得します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propertyName">バインディングプロパティ名</param>
        /// <returns></returns>
#if NET45||NET461
        protected T GetBindingValue<T>(string propertyName)
#else
        protected T GetBindingValue<T>(string propertyName)
#endif
        {
            if (!this.propertyValues.ContainsKey(propertyName))
                return default(T);

            T result;
            try
            {
                result = (T)this.propertyValues[propertyName];
            }
            catch (InvalidCastException)
            {
                //throw new InvalidCastException("ViewModelでプロパティの値の取得に失敗しました。無効な型が指定されました。");
                return default(T);
            }

            return result;
        }

        /// <summary>
        /// プロパティ辞書へプロパティの値を記録し、RaisePropertyChangedを実行します。
        /// </summary>
        /// <param name="propertyName">バインディングプロパティ名</param>
        /// <param name="value"></param>
        protected void SetBindingValue(string propertyName, Object value)
        {
            this.propertyValues[propertyName] = value;
            this.RaisePropertyChanged(propertyName);
        }
    }
}

#endif