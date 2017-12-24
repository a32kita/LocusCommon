using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Serialization.Ini
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class IniSectionConverter
    {
        // プロパティ


        // コンストラクタ

        /// <summary>
        /// 新しい <see cref="IniSectionConverter"/> クラスのインスタンスを初期化します。
        /// </summary>
        public IniSectionConverter()
        {
            // 実装なし
        }


        // 公開メソッド

        /// <summary>
        /// INIデータへ任意の型のインスタンスを変換する機能を実装します。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="target"></param>
        public abstract void WriteIni(IniSection section, Object target);

        /// <summary>
        /// INIデータを任意の型のインスタンスへ変換する機能を実装します。
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public abstract Object ReadIni(IniSection section);
    }
}
