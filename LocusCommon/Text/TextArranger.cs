using System;
using System.Collections.Generic;
using System.Text;

using self = LocusCommon.Text.TextArranger;


namespace LocusCommon.Text
{
    /// <summary>
    /// 
    /// </summary>
    [Locus(Purpose="")]
    public class TextArranger
    {
        // 非公開フィールド
        private int byteWidth;
        private string lineHeader;
        private string lineFooter;
        private string newLine;


        private static Encoding sjis;


        // 公開プロパティ

        /// <summary>
        /// ShiftJISの場合のバイト単位での1行の幅を取得または設定します。
        /// </summary>
        public int ByteWidth
        {
            get { return this.byteWidth; }
            set { this.byteWidth = value; }
        }

        /// <summary>
        /// 各行の先頭に添加する文字列を取得または設定します。
        /// </summary>
        public string LineHeader
        {
            get { return this.lineHeader; }
            set { this.lineHeader = value; }
        }

        /// <summary>
        /// 各行の末尾に添加する文字列を取得または設定します。
        /// </summary>
        public string LineFooter
        {
            get { return this.lineFooter; }
            set { this.lineFooter = value; }
        }

        /// <summary>
        /// 各行の終端文字を取得または設定します。
        /// </summary>
        public string NewLine
        {
            get { return this.newLine; }
            set { this.newLine = value; }
        }


        // 公開イベント

        /// <summary>
        /// Arrangeメソッドが実行された際に発生します。ヘッダやフッタの書き換えなどを行うことが可能です。
        /// </summary>
        public event TextArrangerArrangingEventHandler Arranging;


        // コンストラクタ

        /// <summary>
        /// 新しいTextArrangerクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="ByteWidth">ShiftJISの場合のバイト単位での1行の幅</param>
        /// <param name="LineHeader">各行の先頭に添加する文字列</param>
        /// <param name="LineFooter">各行の末尾に添加する文字列</param>
        /// <param name="NewLine">各行の終端文字</param>
        public TextArranger(int ByteWidth, string LineHeader, string LineFooter, string NewLine)
        {
            this.byteWidth = ByteWidth;
            this.lineHeader = LineHeader;
            this.lineFooter = LineFooter;
            this.newLine = NewLine;

            this.Arranging += (sender, e) => { };
        }

        /// <summary>
        /// 新しいTextArrangerクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="ByteWidth">ShiftJISの場合のバイト単位での1行の幅</param>
        /// <param name="NewLine">各行の終端文字</param>
        public TextArranger(int ByteWidth, string NewLine)
            : this(ByteWidth, "", "", NewLine)
        {
        }

        /// <summary>
        /// 新しいTextArrangerクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="ByteWidth">ShiftJISの場合のバイト単位での1行の幅</param>
        public TextArranger(int ByteWidth)
            : this(ByteWidth, "\n")
        {
        }


        // 静的コンストラクタ

        static TextArranger()
        {
            self.sjis = Encoding.GetEncoding("shift_jis");
        }

        
        // 非公開メソッド

        private static string _arrange(string source, int byteWidth, string header, string footer, string newLine)
        {
            string[] lines = source.Replace("\r\n", "\n").Split('\n');
            string[] resultLines = new string[lines.Length];

            int mainByteWidth = byteWidth - (header.Length + footer.Length);
            
            for (int i = 0; i < lines.Length; i++)
                resultLines[i] = self.__lineArrange(lines[i], mainByteWidth, "\n");

            for (int i = 0; i < resultLines.Length; i++)
            {
                string[] mains = resultLines[i].Split('\n');
                for (int j = 0, padLen = 0; j < mains.Length; j++)
                {
                    padLen = mainByteWidth - self.___getByteLength(mains[j]);
                    mains[j] = mains[j] + new string(' ', padLen);
                }

                resultLines[i] = String.Join(footer + newLine + header, mains);
            }

            return header + String.Join(footer + newLine + header, resultLines) + footer;
        }

        private static string __lineArrange(string source, int byteWidth, string newLine)
        {
            StringBuilder lineTemp = new StringBuilder();
            List<string> result = new List<string>();

            int lineByteCount = 0;
            while (source.Length > 0)
            {
                char ch = source[0];
                int chPoint = self.___getByteLength(ch.ToString());
                source = source.Substring(1);

                if (byteWidth < lineByteCount + chPoint)
                {
                    result.Add(lineTemp.ToString());
                    lineTemp.Length = 0; // lineTemp.Clear();
                    lineByteCount = 0;
                }

                lineTemp.Append(ch);
                lineByteCount += chPoint;
            }
            result.Add(lineTemp.ToString());

            return String.Join(newLine, result.ToArray());
        }

        private static int ___getByteLength(string source)
        {
            return self.sjis.GetByteCount(source);
        }

        
        // 公開メソッド

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public string Arrange(string Source)
        {
            var e = new TextArrangerArrangingEventArgs(this.byteWidth, this.lineHeader, Source, this.lineFooter);
            this.Arranging(this, e);

            int byteWidth = e.ByteWidth;
            string header = e.Header;
            string main = e.Main;
            string footer = e.Footer;

            return self._arrange(main, byteWidth, header, footer, this.newLine);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TextArrangerArrangingEventHandler(Object sender, TextArrangerArrangingEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class TextArrangerArrangingEventArgs : EventArgs
    {
        // 非公開フィールド
        private int byteWidth;
        private string header;
        private string main;
        private string footer;


        // 公開フィールド

        /// <summary>
        /// ShiftJISの場合のバイト単位での1行の幅を取得または設定します。実行されたArrangeメソッド1回に対してのみ適用されます。
        /// </summary>
        public int ByteWidth
        {
            get { return this.byteWidth; }
            set { this.byteWidth = value; }
        }

        /// <summary>
        /// 各行の先頭に添加する文字列を取得または設定します。実行されたArrangeメソッド1回に対してのみ適用されます。
        /// </summary>
        public string Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        /// <summary>
        /// ArrangeメソッドのSource引数を取得または設定します。
        /// </summary>
        public string Main
        {
            get { return this.main; }
            set { this.main = value; }
        }

        /// <summary>
        /// 各行の末尾に添加する文字列を取得または設定します。実行されたArrangeメソッド1回に対してのみ適用されます。
        /// </summary>
        public string Footer
        {
            get { return this.footer; }
            set { this.footer = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいTextArrangerArrangingEventArgsクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="ByteWidth"></param>
        /// <param name="Header"></param>
        /// <param name="Main"></param>
        /// <param name="Footer"></param>
        public TextArrangerArrangingEventArgs(int ByteWidth, string Header, string Main, string Footer)
        {
            this.byteWidth = ByteWidth;
            this.header = Header;
            this.main = Main;
            this.footer = Footer;
        }
    }
}
