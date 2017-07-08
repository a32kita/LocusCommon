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

        /// <summary>
        /// 現在のストリームがシークをサポートするかどうかを示す値を取得します。
        /// </summary>
        public override bool CanSeek
        {
            get { return this.canSeek; }
        }

        /// <summary>
        /// 現在のストリームが読み取りをサポートするかどうかを示す値を取得します。
        /// </summary>
        public override bool CanRead
        {
            get { return this.canRead; }
        }

        /// <summary>
        /// 現在のストリームが書き込みをサポートするかどうかを示す値を取得します。
        /// </summary>
        public override bool CanWrite
        {
            get { return this.canWrite; }
        }

        /// <summary>
        /// 現在のストリームがタイムアウトできるかどうかを決定する値を取得します。
        /// </summary>
        public override bool CanTimeout
        {
            get { return this.canTimeout; }
        }

        /// <summary>
        /// ストリームの長さをバイト単位で取得します。
        /// </summary>
        public override long Length
        {
            get { return this._getLength(); }
        }

        /// <summary>
        /// 現在のストリーム内の位置を取得または設定します。
        /// </summary>
        public override long Position
        {
            get { return this._getPosition(); }
            set { this._setPosition(value); }
        }


        // 公開イベント

        /// <summary>
        /// 読み込みが行われた際に発生します。
        /// </summary>
        public event InterceptableStreamReadingEventHandler Reading;

        /// <summary>
        /// 書き込み処理が行われた際に発生します。
        /// </summary>
        public event InterceptableStreamWritingEventHandler Writing;

        /// <summary>
        /// Flush操作が行われた直後に発生します。
        /// </summary>
        public event EventHandler Flushed;

        /// <summary>
        /// LengthプロパティでLengthの取得が行われようとしているときに発生します。
        /// </summary>
        public event InterceptableStreamLengthEventHandler GettingLength;

        /// <summary>
        /// SetLength操作が行われる際に発生します。
        /// </summary>
        public event InterceptableStreamLengthEventHandler ChangeLength;

        /// <summary>
        /// Seek操作が行われようとしているときに発生します。
        /// </summary>
        public event InterceptableStreamSeekEventHandler Seeking;


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

            this.Reading += (sender, e) => { };
            this.Writing += (sender, e) => { };
            this.Flushed += (sender, e) => { };

            this.GettingLength += (sender, e) => { };
            this.ChangeLength += (sender, e) => { };
        }


        // 非公開メソッド

        private long _getLength()
        {
            var e = new InterceptableStreamLengthEventArgs(0, null);
            this.GettingLength(this, e);

            if (e.Exception != null)
                throw e.Exception;

            return e.Length;
        }

        private void _setLength(long len)
        {
            var e = new InterceptableStreamLengthEventArgs(len, null);
            this.ChangeLength(this, e);

            if (e.Exception != null)
                throw e.Exception;
        }

        private long _getPosition()
        {
            return this.position;
        }

        private void _setPosition(long position)
        {
            this.position = position;
        }

        private _ReadResult _read(int byteLen)
        {
            byte[] buffer = new byte[byteLen];
            var e = new InterceptableStreamReadingEventArgs(this.canRead, buffer, null);
            this.Reading(this, e);

            if (!this.canRead)
                throw new NotSupportedException();

            if (e.Exception != null)
                throw e.Exception;

            return new _ReadResult() { Buffer = buffer, Count = e.Count };
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

        private long _seek(long offset, SeekOrigin seekOrigin)
        {
            var e = new InterceptableStreamSeekEventArgs(offset, seekOrigin, null);
            this.Seeking(this, e);

            if (!this.canSeek)
                throw new NotSupportedException();

            if (e.Exception != null)
                throw e.Exception;

            return e.Offset;
        }


        // 公開メソッド

        /// <summary>
        /// 現在のストリームからバイト シーケンスを読み取り、読み取ったバイト数の分だけストリームの位置を進めます。
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Offset"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public override int Read(byte[] Buffer, int Offset, int Count)
        {
            var r = this._read(Count);
            if (Buffer.Length < Offset + r.Count)
                throw new IndexOutOfRangeException("Bufferの長さがOffsetや読み込まれたバイト数に対して短すぎます。");

            Array.Copy(r.Buffer, 0, Buffer, Offset, r.Count);
            return r.Count;
        }

        /// <summary>
        /// ストリームから 1 バイトを読み取り、ストリーム内の位置を 1 バイト進めます。ストリームの末尾の場合は -1 を返します。
        /// </summary>
        /// <returns>Int32 にキャストされた符号なしバイト。ストリームの末尾の場合は -1。</returns>
        public override int ReadByte()
        {
            var r = this._read(1);
            if (r.Count == 0)
                return -1;

            return r.Buffer[0];
        }

        /// <summary>
        /// 現在のストリームからバイト シーケンスを読み取り、読み取ったバイト数の分だけストリームの位置を進めます。
        /// </summary>
        /// <param name="Buffer">バイト配列。 このメソッドが戻るとき、指定したバイト配列の Offset から (Offset + Count -1) までの値が、現在のソースから読み取られたバイトに置き換えられます。</param>
        /// <param name="Offset">現在のストリームから読み取ったデータの格納を開始する位置を示す buffer 内のバイト オフセット。インデックス番号は 0 から始まります。</param>
        /// <param name="Count">現在のストリームから読み取る最大バイト数。</param>
        public override void Write(byte[] Buffer, int Offset, int Count)
        {
            byte[] target = new byte[Count];
            Array.Copy(Buffer, Offset, target, 0, Count);

            this._write(target);
        }

        /// <summary>
        /// ストリームの現在位置にバイトを書き込み、ストリームの位置を 1 バイトだけ進めます。
        /// </summary>
        /// <param name="Value"></param>
        public override void WriteByte(byte Value)
        {
            this._write(new byte[] { Value });
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Flush()
        {
            this.Flushed(this, new EventArgs());
        }

        /// <summary>
        /// 現在のストリームの長さを設定します。
        /// </summary>
        /// <param name="Value">現在のストリームの希望の長さ (バイト数)。</param>
        public override void SetLength(long Value)
        {
            this._setLength(Value);
        }

        /// <summary>
        /// 現在のストリーム内の位置を設定します。
        /// </summary>
        /// <param name="Offset">Origin パラメーターからの相対バイト オフセット。</param>
        /// <param name="Origin">新しい位置を取得するために使用する参照ポイントを示す SeekOrigin 型の値。</param>
        /// <returns></returns>
        public override long Seek(long Offset, SeekOrigin Origin)
        {
            return this._seek(Offset, Origin);
        }


        // 非公開クラス

        private class _ReadResult
        {
            public byte[] Buffer;
            public int Count;
        }
    }

    /// <summary>
    /// InterceptableStream クラスのイベントを処理するメソッドを表します。 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void InterceptableStreamWritingEventHandler(Object sender, InterceptableStreamWritingEventArgs e);

    /// <summary>
    /// InterceptableStream クラスのイベントのデータを提供します。 
    /// </summary>
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

    /// <summary>
    /// InterceptableStream クラスのイベントを処理するメソッドを表します。 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void InterceptableStreamReadingEventHandler(Object sender, InterceptableStreamReadingEventArgs e);

    /// <summary>
    /// InterceptableStream クラスのイベントのデータを提供します。 
    /// </summary>
    public class InterceptableStreamReadingEventArgs
    {
        // 非公開フィールド
        private bool canRead;
        private byte[] data;
        private int count;
        private Exception exception;


        // 公開プロパティ

        /// <summary>
        /// 読み込みが行われた時点でストリームがCanReadであったかどうかを示す値を取得します。
        /// </summary>
        public bool CanRead
        {
            get { return this.canRead; }
        }

        /// <summary>
        /// 読み込まれるバッファを取得します。バッファの指定はSetDataメソッドで行います。
        /// </summary>
        public byte[] Data
        {
            get { return (byte[])this.data.Clone(); }
        }

        /// <summary>
        /// SetDataで指定されたデータの長さを取得します。
        /// </summary>
        public int Count
        {
            get { return this.count; }
        }

        /// <summary>
        /// このイベントの処理が終了した後に発生する例外を指定します。発生させない場合は、nullを指定します。
        /// nullの場合でも、ストリームのCanReadがfalseであった場合は、NotSupportedExceptionがスローされます。
        /// </summary>
        public Exception Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいInterceptableStreamReadingEventArgsクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="CanRead"></param>
        /// <param name="Data">Readで取得されるバッファです。ストリームのReadメソッドのCount（読み込みバイト数）を超えない長さのbyte[]を指定します。</param>
        /// <param name="Exception"></param>
        public InterceptableStreamReadingEventArgs(bool CanRead, byte[] Data, Exception Exception)
        {
            this.canRead = CanRead;
            this.data = Data;
            this.exception = Exception;
        }


        // 公開メソッド

        /// <summary>
        /// Dataにバッファを設定します。
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Offset"></param>
        /// <param name="Count"></param>
        public void SetData(byte[] Buffer, int Offset, int Count)
        {
            if (Buffer.Length < Offset + Count)
                throw new IndexOutOfRangeException("指定されたバッファの長さに対して、大きすぎるOffsetとCountが指定されています。");

            if (this.data.Length < Count)
                throw new IndexOutOfRangeException("ストリームのReadメソッドで読み込みが行われるバイト数よりも長いデータが書き込まれようとしています。");

            this.count = Count;
            Array.Copy(Buffer, Offset, this.data, 0, Count);
        }

        /// <summary>
        /// Dataにバッファを設定します。
        /// </summary>
        /// <param name="Buffer"></param>
        public void SetData(byte[] Buffer)
        {
            this.SetData(Buffer, 0, Buffer.Length);
        }
    }

    /// <summary>
    /// InterceptableStream クラスのイベントを処理するメソッドを表します。 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void InterceptableStreamLengthEventHandler(Object sender, InterceptableStreamLengthEventArgs e);

    /// <summary>
    /// InterceptableStream クラスのイベントのデータを提供します。 
    /// </summary>
    public class InterceptableStreamLengthEventArgs
    {
        // 非公開フィールド
        private long length;
        private Exception exception;

        
        // 公開プロパティ
        
        /// <summary>
        /// SetLengthで指定されたストリームの長さ、または、Length.getで取得されるストリームの長さを取得、設定します。
        /// </summary>
        public long Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        /// <summary>
        /// このイベントの処理が終了した後に発生する例外を指定します。発生させない場合は、nullを指定します。
        /// </summary>
        public Exception Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいInterceptableStreamLengthEventArgsクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="Length"></param>
        /// <param name="Exception"></param>
        public InterceptableStreamLengthEventArgs(long Length, Exception Exception)
        {
            this.length = Length;
            this.exception = Exception;
        }


        // 公開メソッド

        /// <summary>
        /// ExceptionプロパティにNotSupportedExceptionを設定し、イベント終了直後にスローできるようにします。
        /// </summary>
        public void EnableNotSupportedException()
        {
            this.exception = new NotSupportedException();
        }
    }

    /// <summary>
    /// InterceptableStream クラスのイベントを処理するメソッドを表します。 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void InterceptableStreamSeekEventHandler(Object sender, InterceptableStreamSeekEventArgs e);

    /// <summary>
    /// InterceptableStream クラスのイベントのデータを提供します。 
    /// </summary>
    public class InterceptableStreamSeekEventArgs
    {
        // 非公開フィールド
        private long offset;
        private SeekOrigin seekOrigin;
        private Exception exception;


        // 公開プロパティ

        /// <summary>
        /// 
        /// </summary>
        public long Offset
        {
            get { return this.offset; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SeekOrigin SeekOrigin
        {
            get { return this.seekOrigin; }
            set { this.seekOrigin = value; }
        }

        /// <summary>
        /// このイベントの処理が終了した後に発生する例外を指定します。発生させない場合は、nullを指定します。
        /// nullの場合でも、ストリームのCanSeekがfalseであった場合は、NotSupportedExceptionがスローされます。
        /// </summary>
        public Exception Exception
        {
            get { return this.exception; }
            set { this.exception = value; }
        }


        // コンストラクタ

        /// <summary>
        /// 新しいInterceptableStreamSeekEventArgsクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="Offset"></param>
        /// <param name="SeekOrigin"></param>
        /// <param name="exception"></param>
        public InterceptableStreamSeekEventArgs(long Offset, SeekOrigin SeekOrigin, Exception exception)
        {
            this.offset = Offset;
            this.seekOrigin = SeekOrigin;
            this.exception = Exception;
        }
    }
}
