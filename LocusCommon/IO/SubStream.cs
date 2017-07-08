using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LocusCommon.IO
{
    /// <summary>
    /// 
    /// </summary>
    [Locus(Purpose="")]
    public class SubStream : Stream
    {
        // 非公開フィールド
        private Stream baseStream;
        private long start;
        private long length;
        private long position;

        // 公開プロパティ

        /// <summary>
        /// 
        /// </summary>
        public override long Length
        {
            get { return length; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead
        {
            get { return position < length; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanSeek
        {
            get { return true; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override long Position
        {
            get { return position; }
            set { baseStream.Position = start + (position = value); }
        }


        // コンストラクタ

        /// <summary>
        /// 既存のストリームの現在の位置から長さを指定して、新しいSubStreamクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        public SubStream(Stream s, long length)
            : this(s, s.Position, length)
        {
        }

        /// <summary>
        /// 新しいSubStreamクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public SubStream(Stream s, long start, long length)
        {
            this.baseStream = s;
            this.start = start;
            this.length = length;
        }

        
        // 公開メソッド
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!CanRead) return 0;
            count = (int)Math.Min(length - position, count);
            int ret = baseStream.Read(buffer, offset, count);
            position += ret;
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin: Position = offset; break;
                case SeekOrigin.Current: Position += offset; break;
                case SeekOrigin.End: Position = length + offset; break;
            }
            return position;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void Flush()
        {
        }
    }
}
