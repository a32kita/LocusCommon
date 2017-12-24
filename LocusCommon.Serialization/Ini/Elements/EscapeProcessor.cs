using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Serialization.Ini.Elements
{
    using self = EscapeProcessor;

    /// <summary>
    /// Ini 向けのエスケープ処理を提供します。
    /// </summary>
    public static class EscapeProcessor
    {
        // 非公開静的フィールド
        private static Dictionary<string, string> escapeTable;


        // 静的コンストラクタ

        /// <summary>
        /// <see cref="EscapeProcessor"/> クラスを初期化します。
        /// </summary>
        static EscapeProcessor()
        {
            self.escapeTable = new Dictionary<string, string>()
            {
                //[""] = "",
                ["\\\\"] = "\\",
            };
        }


        // 公開静的メソッド

        /// <summary>
        /// エスケープ表現を使用した文字列へ変換します。
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public static string GetEscapeString(string rawString)
        {
            return rawString;
        }

        /// <summary>
        /// エスケープ表現を解決し、生文字列へ変換します。
        /// </summary>
        /// <param name="escapeString"></param>
        /// <returns></returns>
        public static string GetRawString(string escapeString)
        {
            return escapeString;
        }
    }
}
