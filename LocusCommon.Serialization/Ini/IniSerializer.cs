using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LocusCommon.Serialization.Ini
{
    /// <summary>
    /// 
    /// </summary>
    public class IniSerializer : Bases.LocusFormatterBase
    {
        // 非公開フィールド
        private List<IniSectionConverter> sectionConverters;


        // 公開プロパティ
        
        /// <summary>
        /// このシリアライザで使用する <see cref="IniSectionConverter"/> の一覧を取得します。
        /// </summary>
        public ICollection<IniSectionConverter> SectionConverters
        {
            get => this.sectionConverters;
        }


        // コンストラクタ

        /// <summary>
        /// 新しい <see cref="IniSerializer"/> クラスのインスタンスを初期化します。
        /// </summary>
        public IniSerializer()
        {
            this.sectionConverters = new List<IniSectionConverter>();
        }


        // 非公開メソッド
        


        // 公開メソッド

        /// <summary>
        /// 指定したオブジェクトをシリアライズします。
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="obj"></param>
        public override void Serialize(Stream stream, object obj)
        {

        }
    }
}
