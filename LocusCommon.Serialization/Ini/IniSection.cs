using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Serialization.Ini
{
    /// <summary>
    /// 
    /// </summary>
    public class IniSection
    {
        // 非公開フィールド
        private string name;
        private List<IniKey> keys;

        
        // 公開プロパティ

        /// <summary>
        /// このセクションの名前を取得または設定します。
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        /// <summary>
        /// キーのコレクションを取得します。
        /// </summary>
        public ICollection<IniKey> Keys
        {
            get => this.keys;
        }


        // コンストラクタ

        /// <summary>
        /// セクション名と既存の <see cref="IniKey"/> のコレクションを指定して、新しい <see cref="IniSection"/> を初期化します。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="keys"></param>
        public IniSection(string name, IEnumerable<IniKey> keys)
        {
            this.name = name;

            this.keys = new List<IniKey>();
            this.keys.AddRange(keys);
        }

        /// <summary>
        /// セクション名を指定して、新しい <see cref="IniSection"/> を初期化します。
        /// </summary>
        /// <param name="name"></param>
        public IniSection(string name)
            : this(name, new IniKey[0])
        {
            // 実装なし
        }
    }
}
