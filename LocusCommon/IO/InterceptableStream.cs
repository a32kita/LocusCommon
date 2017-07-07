using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LocusCommon.IO
{
    /// <summary>
    /// 操作が行われるとイベントが発生する疑似ストリームです。
    /// </summary>
    [Locus(Purpose="ストリームに書き込み処理が発生した場合などに、イベントで書き込み内容の取得や、読み込み内容の指定などを行えるようにします。")]
    public class InterceptableStream : Stream
    {
        // 非公開フィールド
        private bool canSeek;
        private bool canRead;
        private bool canWrite;
        private bool canTimeout;

        private long position;


        // 公開プロパティ
        
        public override bool CanSeek
        {
            get { return this.canSeek; }
        }

        public override bool CanRead
        {
            get { return this.canRead; }
        }

        public override bool CanWrite
        {
            get { return this.canWrite; }
        }

        public override bool CanTimeout
        {
            get { return this.canTimeout; }
        }

        public override long Position
        {
            get { return this._getPosition(); }
            set { this._setPosition(value); }
        }


        // 公開イベント

        /// <summary>
        /// 書き込み処理が行われた際に発生します。
        /// </summary>
        public event InterceptableStreamWritingEventHandler Writing;


        // コンストラクタ

        /// <summary>
        /// 新しいInterceptableStreamクラスのインスタンスを初期化します。
        /// </summary>
        public InterceptableStream()
        {
            this.canSeek = true;
            this.canRead = true;
            this.canWrite = true;
            this.canTimeout = true;
            this.position = 0;

            this.Writing += (sender, e) => { };
        }


        // 非公開メソッド

        private long _getPosition()
        {
            return this.position;
        }

        private void _setPosition(long position)
        {
            this.position = position;
        }

        private void _write(byte[] data)
        {
            var e = new InterceptableStreamWritingEventArgs(this.canWrite, data, null);
            this.Writing(this, e);

            if (!this.canWrite)
                throw new NotSupportedException();

            if (e.Exception != null)
                throw e.Exception;
        }


        // 公開メソッド

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Offset"></param>
        /// <param name="Count"></param>
        public override void Write(byte[] Buffer, int Offset, int Count)
        {
            byte[] target = new byte[Count];
            Array.Copy(Buffer, Offset, target, 0, Count);

            this._write(target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteByte(byte value)
        {
            this._write(new byte[] { value });
        }
    }

    public delegate void InterceptableStreamWritingEventHandler(Object sender, InterceptableStreamWritingEventArgs e);

    public class InterceptableStreamWritingEventArgs
    {
        // 非公開フィールド
        private byte[] data;
        private bool canWrite;
        private Exception exception;


        // 公開プロパティ

        /// <summary>
        /// 書き込みが行われた時点でストリームがCanWriteであったかどうかを示す値を取得します。
        /// </summary>
        public bool CanWrite
        {
            get { return this.canWrite; }
        }

        /// <summary>
        /// 書き込みが行われたデータを示すバッファを取得します。
        /// </summary>
        public byte[] Data
        {
            get { return this.data; }
        }

        /// <summary>
        /// このイベントの処理が終了した後に発生する例外を指定します。発生させない場合は、nullを指定します。
        /// nullの場合でも、ストリームのCanWriteがfalseであった場合は、NotSupportedExceptionがスローされます。
        /// </summary>
        public Exception Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいInterceptableStreamWritingEventArgsクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="CanWrite"></param>
        /// <param name="Data"></param>
        /// <param name="Exception"></param>
        public InterceptableStreamWritingEventArgs(bool CanWrite, byte[] Data, Exception Exception)
        {
            this.canWrite = CanWrite;
            this.data = (byte[])Data.Clone();
            this.exception = Exception;
        }
    }
}
