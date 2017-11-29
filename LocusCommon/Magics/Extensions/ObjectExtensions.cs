using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LocusCommon.Magics.Extensions
{
    /// <summary>
    /// Object に対するリフレクション補助の拡張メソッドを実装します。
    /// </summary>
    public static class ObjectExtensions
    {
        // 非公開静的フィールド


        // 公開静的プロパティ


        // 静的コンストラクタ

        /// <summary>
        /// ObjectExtensions クラスのインスタンスを初期化します。
        /// </summary>
        static ObjectExtensions()
        {

        }


        // 公開静的メソッド

        /// <summary>
        /// 非公開フィールドの値を取得します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetNonPublicField<T>(this Object instance, string fieldName)
        {
            Object result = instance.GetType().GetField(fieldName, BindingFlags.NonPublic).GetValue(instance);
            if (result is T == false)
                throw new InvalidCastException();
            return (T)result;
        }
    }
}
