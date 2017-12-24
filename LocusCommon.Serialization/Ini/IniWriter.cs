using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using LocusCommon.Serialization.Ini.Elements;

namespace LocusCommon.Serialization.Ini
{
    using self = IniWriter;

    /// <summary>
    /// Ini形式のデータを書き出す機能を提供します。
    /// </summary>
    public class IniWriter
    {
        // 非公開フィールド
        private StreamWriter streamWriter;
        private string commentHeader;


        // 非公開静的フィールド
        private static Encoding defaultEncoding;
        private static string defaultCommentHeader;


        // 公開プロパティ

        /// <summary>
        /// コメント行の行頭に付与するヘッダ文字列を取得または設定します。
        /// </summary>
        public string CommentHeader
        {
            get => this.commentHeader;
            set => this.commentHeader = value;
        }


        // 公開静的プロパティ

        /// <summary>
        /// <see cref="self"/> で使用される既定の <see cref="Encoding"/> を取得または設定します。
        /// </summary>
        public static Encoding DefaultEncoding
        {
            get => self.defaultEncoding;
            set => self.defaultEncoding = value;
        }

        /// <summary>
        /// <see cref="self"/> で使用される既定のコメント行のヘッダを取得または設定します。
        /// </summary>
        public static string DefaultCommentHeader
        {
            get => self.defaultCommentHeader;
            set => self.defaultCommentHeader = value;
        }


        // コンストラクタ

        /// <summary>
        /// 出力先として利用する <see cref="StreamWriter"/> を指定して、新しい <see cref="IniWriter"/> クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="streamWriter"></param>
        public IniWriter(StreamWriter streamWriter)
        {
            this.streamWriter = streamWriter;
            this.commentHeader = self.defaultCommentHeader;
        }

        /// <summary>
        /// 出力先として利用する <see cref="Stream"/> と <see cref="Encoding"/> を指定して、新しい <see cref="IniWriter"/> クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public IniWriter(Stream stream, Encoding encoding)
            : this(new StreamWriter(stream, encoding))
        {
            // 実装なし
        }

        /// <summary>
        /// 出力先として利用する <see cref="Stream"/> を指定して、新しい <see cref="IniWriter"/> クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="stream"></param>
        public IniWriter(Stream stream)
            : this(stream, self.defaultEncoding)
        {
            // 実装なし
        }


        // 静的コンストラクタ

        /// <summary>
        /// <see cref="IniWriter"/> を初期化します。
        /// </summary>
        static IniWriter()
        {
            self.defaultEncoding = Encoding.UTF8;
            self.defaultCommentHeader = "; ";
        }
        
        
        // 非公開メソッド

        /// <summary>
        /// セクション名を記述します。
        /// </summary>
        /// <param name="unescapedName">エスケープ処理がまだ行われていないセクション名</param>
        private void _writeSectionName(string unescapedName)
        {
            string sectionName = String.Format("[{0}]", EscapeProcessor.GetEscapeString(unescapedName));

            this.streamWriter.WriteLine(sectionName);
        }

        /// <summary>
        /// キーとその値、及び添付コメントを記述します。
        /// </summary>
        /// <param name="key"></param>
        private void _writeKeyValue(IniKey key)
        {
            if (key.Comment != null)
                this._writeComment(key.Comment);
            
            this.streamWriter.WriteLine("{0}={1}",
                EscapeProcessor.GetEscapeString(key.Name),
                EscapeProcessor.GetEscapeString(key.ValueString));
        }
        
        /// <summary>
        /// コメント行を記述します。
        /// </summary>
        /// <param name="comment"></param>
        private void _writeComment(string comment)
        {
            this.streamWriter.WriteLine(this.commentHeader + comment);
        }
        

        // 公開メソッド

        /// <summary>
        /// セクションを記述します。
        /// </summary>
        /// <param name="section"></param>
        public void WriteSection(IniSection section)
        {
            this._writeSectionName(section.Name);
            foreach (var key in section.Keys)
                this._writeKeyValue(key);
        }

        /// <summary>
        /// コメントアウトを記述します。
        /// </summary>
        /// <param name="comment"></param>
        public void WriteComment(string comment)
        {
            this._writeComment(comment);
        }
    }
}
