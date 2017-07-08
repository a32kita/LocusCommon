using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon
{
    /// <summary>
    /// Locusライブラリの定義クラスであることを示し、追加情報を付与します。
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class)]
    public sealed class LocusAttribute : Attribute
    {
        // 非公開フィールド
        private string purpose;

        
        // 公開プロパティ

        /// <summary>
        /// クラスの使用目的を定義します。
        /// </summary>
        public string Purpose
        {
            get { return this.purpose; }
            set { this.purpose = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいLocusAttributeクラスのインスタンスを初期化します。
        /// </summary>
        public LocusAttribute()
        {
        }
    }
}
