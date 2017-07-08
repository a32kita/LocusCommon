using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using LocusCommon;
using LocusCommon.IO;

namespace LocusCommon.Test.Net20
{
    public static class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Encoding encoding = Encoding.GetEncoding("shift_jis");


            #region InterceptableStream :: Writing 
            InterceptableStream stream1 = new InterceptableStream();
            stream1.Writing += (sender, e) =>
            {
                string str = encoding.GetString(e.Data);
                Console.Write("Input: ");
                Console.WriteLine(str);
            };

            StreamWriter sw = new StreamWriter(stream1, encoding);
            sw.AutoFlush = true;
            sw.Write("hello, world!!");
            sw.Write("hello, world!!");

            Console.WriteLine("Writing test is completed.");
            Console.WriteLine();
            #endregion

            #region InterceptableStream :: Reading
            InterceptableStream stream2 = new InterceptableStream();
            stream2.Reading += (sender, e) =>
            {
                Console.WriteLine("ReadReq: {0}bytes", e.MaxCount);
                e.SetData(encoding.GetBytes(new String('#', e.MaxCount)));
                Console.WriteLine("ReadReq: {0}bytes are loaded.", e.Count);
            };

            char[][] bufs = new char[][]
            {
                new char[5],
                new char[7],
                new char[3],
            };

            StreamReader sr = new StreamReader(stream2, encoding);
            foreach (char[] buf in bufs)
            {
                sr.Read(buf, 0, buf.Length);
                Console.WriteLine("ReadRes:" + new string(buf));
            }

            Console.WriteLine("Reading test is completed.");
            Console.WriteLine();
            #endregion

            #region DistributableStream :: Writing
            InterceptableStream intStream1 = new InterceptableStream();
            InterceptableStream intStream2 = new InterceptableStream();
            InterceptableStream intStream3 = new InterceptableStream();

            intStream1.Writing += (sender, e) => Console.WriteLine("st1: {0}", encoding.GetString(e.Data));
            intStream2.Writing += (sender, e) => Console.WriteLine("st2: {0}", encoding.GetString(e.Data));
            intStream3.Writing += (sender, e) => Console.WriteLine("st3: {0}", encoding.GetString(e.Data));

            DistributableStream distStream = new DistributableStream(new Stream[]
            {
                intStream1,
                intStream2,
                intStream3,
            });

            StreamWriter distSW = new StreamWriter(distStream, encoding);
            distSW.AutoFlush = true;
            distSW.Write("This is a test data.");

            Console.WriteLine("InterceptableStream test is completed.");
            Console.WriteLine();
            #endregion


            Console.ReadKey();
        }
    }
}
