using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Serialization.Ini
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class IniSectionConverter<TValueType>
    {
        // プロパティ
        
        /// <summary>
        /// このコンバーターがサポートする対象の型を取得します。
        /// </summary>
        public Type ValueType
        {
            get => typeof(TValueType);
        }

        /// <summary>
        /// このコンバータが INI データの読み込みをサポートしているかどうかを示す値を取得します。
        /// </summary>
        public abstract bool CanRead
        {
            get;
        }

        /// <summary>
        /// このコンバータが INI データの書き出しをサポートしているかどうかを示す値を取得します。
        /// </summary>
        public abstract bool CanWrite
        {
            get;
        }


        // コンストラクタ

        /// <summary>
        /// 新しい <see cref="IniSectionConverter{TValueType}"/> クラスのインスタンスを初期化します。
        /// </summary>
        public IniSectionConverter()
        {
            // 実装なし
        }


        // 公開メソッド

        /// <summary>
        /// INIデータ へ <see cref="TValueType"/> を変換する機能を実装します。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="target"></param>
        public abstract void WriteIni(IniSection section, TValueType target);

        /// <summary>
        /// INIデータ を <see cref="TValueType"/> へ変換する機能を実装します。
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public abstract TValueType ReadIni(IniSection section);
    }
}
