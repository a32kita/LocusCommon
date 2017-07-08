using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LocusCommon.IO
{
    /// <summary>
    /// 書き込み処理を複数のストリームに転送します。読み込みやシークは行なえません。
    /// </summary>
    [Locus(Purpose="1回の書き込み処理で複数のストリームへ書き込みを行いたい場合などに利用します。")]
    public class DistributableStream : Stream
    {
        // 非公開フィールド
        private List<Stream> streamList;
        private bool autoClose;


        // 公開プロパティ

        /// <summary>
        /// 現在のストリームが読み取りをサポートするかどうかを示す値を取得します。DistributableStreamクラスでは常にfalseを示します。
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }

        /// <summary>
        /// 現在のストリームが書き込みをサポートするかどうかを示す値を取得します。 
        /// </summary>
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        /// 現在のストリームがシークをサポートするかどうかを示す値を取得します。DistributableStreamクラスでは常にfalseを示します。
        /// </summary>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// ストリームの長さをバイト単位で取得します。DistributableStreamクラスではサポートしておりません。
        /// </summary>
        public override long Length
        {
            get { throw new NotFiniteNumberException(); }
        }

        /// <summary>
        /// 現在のストリーム内の位置を取得、設定します。DistributableStreamクラスではサポートしておりません。
        /// </summary>
        public override long Position
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        
        /// <summary>
        /// Close()メソッド実行時に転送先のすべてのストリームをCloseするかどうかを示す値を取得、設定します。
        /// </summary>
        public bool AutoClose
        {
            get { return this.autoClose; }
            set { this.autoClose = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 転送先のストリームを指定して、新しいDistributableStreamクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="Streams"></param>
        public DistributableStream(ICollection<Stream> Streams)
        {
            this.streamList = new List<Stream>();
            this.streamList.AddRange(Streams);
        }

        /// <summary>
        /// 新しいDistributableStreamクラスのインスタンスを初期化します。
        /// </summary>
        public DistributableStream()
            : this(new Stream[0])
        {
        }


        // 非公開メソッド

        private void _write(byte[] buffer)
        {
            foreach (var s in this.streamList)
                s.Write(buffer, 0, buffer.Length);
        }

        private void _flush()
        {
            foreach (var s in this.streamList)
                s.Flush();
        }

        private void _close()
        {
            if (!this.autoClose)
                return;

            foreach (var s in this.streamList)
                s.Close();
        }


        // 公開メソッド

        /// <summary>
        /// 現在のストリームからバイト シーケンスを読み取り、読み取ったバイト数の分だけストリームの位置を進めます。DistributableStreamクラスではサポートしておりません。
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Offset"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public override int Read(byte[] Buffer, int Offset, int Count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ストリームから 1 バイトを読み取り、ストリーム内の位置を 1 バイト進めます。ストリームの末尾の場合は -1 を返します。DistributableStreamクラスではサポートしておりません。
        /// </summary>
        /// <returns></returns>
        public override int ReadByte()
        {
            throw new NotFiniteNumberException();
        }

        /// <summary>
        /// 現在のストリームにバイト シーケンスを書き込み、書き込んだバイト数の分だけストリームの現在位置を進めます。
        /// </summary>
        /// <param name="Buffer">バイト配列。このメソッドは、buffer から現在のストリームに、count で指定されたバイト数だけコピーします。</param>
        /// <param name="Offset">現在のストリームへのバイトのコピーを開始する位置を示す buffer 内のバイト オフセット。インデックス番号は 0 から始まります。</param>
        /// <param name="Count">現在のストリームに書き込むバイト数。</param>
        public override void Write(byte[] Buffer, int Offset, int Count)
        {
            byte[] target = new byte[Count];
            Array.Copy(Buffer, Offset, target, 0, Count);
            this._write(target);
        }

        /// <summary>
        /// ストリームの現在位置にバイトを書き込み、ストリームの位置を 1 バイトだけ進めます。
        /// </summary>
        /// <param name="Value">ストリームに書き込むバイト。</param>
        public override void WriteByte(byte Value)
        {
            this._write(new byte[] { Value });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        public override void SetLength(long Value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 現在のストリームの長さを設定します。DistributableStreamクラスではサポートしておりません。
        /// </summary>
        /// <param name="Offset"></param>
        /// <param name="Origin"></param>
        /// <returns></returns>
        public override long Seek(long Offset, SeekOrigin Origin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 転送先ストリームに対応するすべてのバッファをクリアし、バッファ内のデータを基になるデバイスに書き込みます。
        /// </summary>
        public override void Flush()
        {
            this._flush();
        }

        /// <summary>
        /// AutoCloseがtrueの場合、現在のストリームを閉じ、現在のストリームに関連付けられているすべてのリソース (ソケット、ファイル ハンドルなど) を解放します。
        /// </summary>
        public override void Close()
        {
            this._close();
            base.Close();
        }
    }
}
