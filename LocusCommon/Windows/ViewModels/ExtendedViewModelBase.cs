#if NET40||NET45||NET461

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;


namespace LocusCommon.Windows.ViewModels
{
#if NET45||NET461
    using CallerMemberNameAttribute = System.Runtime.CompilerServices.CallerMemberNameAttribute;
#endif

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
        
#if NET45||NET461
        /// <summary>
        /// プロパティ辞書からプロパティの値を取得します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propertyName">バインディングプロパティ名。プロパティの中で呼び出しを行っている場合、省略が可能です。</param>
        /// <returns></returns>
        protected T GetBindingValue<T>([CallerMemberName] string propertyName = null)
#else
        /// <summary>
        /// プロパティ辞書からプロパティの値を取得します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propertyName">バインディングプロパティ名</param>
        /// <returns></returns>
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

#if NET45 || NET461
        /// <summary>
        /// プロパティ辞書へプロパティの値を記録し、RaisePropertyChangedを実行します。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName">このパラメータは使用されません。</param>
        protected void SetBindingValueQuick(Object value, [Browsable(false)][CallerMemberName] string propertyName = null)
        {
            this.SetBindingValue(propertyName, value);
        }
#endif
    }
}

#endif