using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon
{
    /// <summary>
    /// Locusライブラリの定義クラスであることを示し、追加情報を付与します。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LocusAttribute : Attribute
    {
        // 非公開フィールド
        private string purpose;
        private string libraryTarget;

        
        // 公開プロパティ

        /// <summary>
        /// クラスの使用目的を定義します。
        /// </summary>
        public string Purpose
        {
            get { return this.purpose; }
            set { this.purpose = value; }
        }

        /// <summary>
        /// ライブラリのターゲットフレームワークを取得します．
        /// </summary>
        public string LibraryTarget
        {
            get { return this.libraryTarget; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいLocusAttributeクラスのインスタンスを初期化します。
        /// </summary>
        public LocusAttribute()
        {
            this.libraryTarget = "unknown";
#if NET20
            this.libraryTarget = "2.0";
#endif

#if NET35
            this.libraryTarget = "3.5";
#endif

#if NET40
            this.libraryTarget = "4.0";
#endif

#if NET45
            this.libraryTarget = "4.5";
#endif

#if NET461
            this.libraryTarget = "4.6.1";
#endif
        }
    }
}
