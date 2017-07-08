using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LocusCommon.IO
{
    /// <summary>
    /// LocusCommon.IO名前空間向けのStreamWriter機能を提供します。
    /// デフォルトの設定では、WriteLineメソッドでWriteメソッドが呼ばれるようになります。引数なしのWriteLineメソッドはキャンセルされません。
    /// </summary>
    [Locus(Purpose="")]
    public class QuickStreamWriter : StreamWriter
    {
        // 非公開フィールド
        private bool cancelWriteLine;


        // 公開プロパティ

        /// <summary>
        /// WriteLineメソッドをキャンセルし、Writeメソッドへ転送するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool CancelWriteLine
        {
            get { return this.cancelWriteLine; }
            set { this.cancelWriteLine = true; }
        }


        // コンストラクタ

        /// <summary>
        /// InterceptableStreamを使用し、指定されたエンコーディングで新しいQuickStreamWriterクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="Encoding"></param>
        public QuickStreamWriter(Encoding Encoding)
            : base(new InterceptableStream(), Encoding)
        {
            this.AutoFlush = true;
            this.cancelWriteLine = true;
        }

        /// <summary>
        /// 既存のストリームを使用し、指定されたエンコーディングで新しいQuickStreamWriterクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="Stream"></param>
        /// <param name="Encoding"></param>
        /// <param name="AutoFlush"></param>
        public QuickStreamWriter(Stream Stream, Encoding Encoding, bool AutoFlush)
            : base(Stream, Encoding)
        {
            this.AutoFlush = AutoFlush;
            this.cancelWriteLine = true;
        }


        // 公開メソッド

        /// <summary>
        /// Boolean 値のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む Boolean。</param>
        public override void WriteLine(bool value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 文字をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(char value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 文字の配列をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">データの読み取り元の文字配列。</param>
        public override void WriteLine(char[] value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 10 進値のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む 10 進値。</param>
        public override void WriteLine(decimal value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 8 バイト浮動小数点値のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む 8 バイト浮動小数点値。</param>
        public override void WriteLine(double value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 4 バイト符号付き整数のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む 4 バイト符号付き整数。</param>
        public override void WriteLine(int value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 8 バイト符号付き整数のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む 8 バイト符号付き整数。</param>
        public override void WriteLine(long value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// オブジェクトで ToString を呼び出して、そのオブジェクトのテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込むオブジェクト。value が null 参照 (Visual Basic では Nothing) の場合は、行終端文字だけを書き込みます。</param>
        public override void WriteLine(Object value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 4 バイト浮動小数点値のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む 4 バイト浮動小数点値。</param>
        public override void WriteLine(float value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 文字列をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む文字列。value が null 参照 (Visual Basic では Nothing) の場合は、行終端文字だけを書き込みます。</param>
        public override void WriteLine(string value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 4 バイト符号なし整数のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む 4 バイト符号なし整数。</param>
        public override void WriteLine(uint value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// 8 バイト符号なし整数のテキスト形式をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む 8 バイト符号なし整数。</param>
        public override void WriteLine(ulong value)
        {
            if (this.cancelWriteLine)
                base.Write(value);
            else
                base.WriteLine(value);
        }

        /// <summary>
        /// Format と同じセマンティクスを使用して、書式設定された文字列と改行を書き込みます。
        /// </summary>
        /// <param name="format">書式指定文字列。</param>
        /// <param name="arg0">書式設定された文字列に書き込むオブジェクト。</param>
        public override void WriteLine(string format, Object arg0)
        {
            if (this.cancelWriteLine)
                base.Write(format, arg0);
            else
                base.WriteLine(format, arg0);
        }

        /// <summary>
        /// Format と同じセマンティクスを使用して、書式設定された文字列と改行を書き込みます。
        /// </summary>
        /// <param name="format">書式指定文字列。</param>
        /// <param name="args">書式設定された文字列に書き込むオブジェクト。</param>
        public override void WriteLine(string format, params Object[] args)
        {
            if (this.cancelWriteLine)
                base.Write(format, args);
            else
                base.WriteLine(format, args);
        }

        /// <summary>
        /// 文字の部分配列をテキスト ストリームに書き込み、続けて行終端記号を書き込みます。
        /// </summary>
        /// <param name="buffer">データの読み取り元の文字配列。</param>
        /// <param name="index">読み取りの開始位置を示す buffer 内のインデックス。</param>
        /// <param name="count">書き込む文字数の上限。</param>
        public override void WriteLine(char[] buffer, int index, int count)
        {
            if (this.cancelWriteLine)
                base.Write(buffer, index, count);
            else
                base.WriteLine(buffer, index, count);
        }

        /// <summary>
        /// Format と同じセマンティクスを使用して、書式設定された文字列と改行を書き込みます。
        /// </summary>
        /// <param name="format">書式指定文字列。</param>
        /// <param name="arg0">書式設定された文字列に書き込むオブジェクト。</param>
        /// <param name="arg1">書式設定された文字列に書き込むオブジェクト。</param>
        public override void WriteLine(string format, Object arg0, Object arg1)
        {
            if (this.cancelWriteLine)
                base.Write(format, arg0, arg1);
            else
                base.WriteLine(format, arg0, arg1);
        }

        /// <summary>
        /// Format と同じセマンティクスを使用して、書式設定された文字列と改行を書き込みます。
        /// </summary>
        /// <param name="format">書式指定文字列。</param>
        /// <param name="arg0">書式設定された文字列に書き込むオブジェクト。</param>
        /// <param name="arg1">書式設定された文字列に書き込むオブジェクト。</param>
        /// <param name="arg2">書式設定された文字列に書き込むオブジェクト。</param>
        public override void WriteLine(string format, Object arg0, Object arg1, Object arg2)
        {
            if (this.cancelWriteLine)
                base.Write(format, arg0, arg1, arg2);
            else
                base.WriteLine(format, arg0, arg1, arg2);
        }
    }
}
