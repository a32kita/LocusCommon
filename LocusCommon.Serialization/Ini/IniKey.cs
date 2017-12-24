using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Serialization.Ini
{
    /// <summary>
    /// 
    /// </summary>
    public class IniKey
    {
        // 非公開フィールド
        private string name;
        private string valueString;
        private string comment;


        // 公開プロパティ

        /// <summary>
        /// キー名を取得または設定します。
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        /// <summary>
        /// 値を文字列で取得または設定します。
        /// </summary>
        public string ValueString
        {
            get => this.valueString;
            set => this.valueString = value;
        }

        /// <summary>
        /// このパラメータのコメントを取得または設定します。
        /// コメントを記述しない場合、null を示します。
        /// </summary>
        public string Comment
        {
            get => this.comment;
            set => this.comment = value;
        }


        // コンストラクタ

        /// <summary>
        /// キー名、値、コメントを指定して、新しい <see cref="IniKey"/> クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="name">キー名</param>
        /// <param name="valueString">文字列で表現された値</param>
        /// <param name="comment">このパラメータに対するコメント</param>
        public IniKey(string name, string valueString, string comment)
        {
            this.name = name;
            this.valueString = valueString;
            this.comment = comment;
        }

        /// <summary>
        /// キー名と値を指定して、新しい <see cref="IniKey"/> クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="name">キー名</param>
        /// <param name="valueString">文字列で表現された値</param>
        public IniKey(string name, string valueString)
            : this(name, valueString, null)
        {
            // 実装なし
        }
    }
}
