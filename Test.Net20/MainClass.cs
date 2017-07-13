using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using LocusCommon;
using LocusCommon.IO;
using LocusCommon.Collection.Generic;
using LocusCommon.Text;


namespace LocusCommon.Test.Net20
{
    public static class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Encoding encoding = Encoding.GetEncoding("shift_jis");
            

            #region InterceptableStream :: Writing 
            Console.WriteLine("InterceptableStream :: Writing ");

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
            Console.WriteLine("InterceptableStream :: Reading ");

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
            Console.WriteLine("DistributableStream :: Writing ");

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

            Console.WriteLine("DistributableStream test is completed.");
            Console.WriteLine();
            #endregion

            #region TextArranger (normal)
            Console.WriteLine("TextArranger (normal)");

            TextArranger arranger = new TextArranger(Console.WindowWidth - 1, "Header | ", " | Footer", "\n");
            Console.WriteLine(arranger.Arrange("この内Clearメソッドは、Lengthプロパティを0にして自分自身を返しているだけですので、実質的にLengthプロパティを0にする方法と同じです。（ただ、メソッドになっているため分かりやすい、自分自身のStringBuilderオブジェクトを返すため続けて別のメソッドを呼び出せるなどの利点があります。）\n新しいインスタンスを作成する方法とLengthを0にする方法を比較すると、前者は新しいインスタンスを作成し、古いインスタンスを破棄するため、後者のインスタンスを再利用する方法と比べて無駄が多いと考えられます。実際「Clear C# StringBuilder : C# 411」によると、Lengthを0にした方法のほうが速いそうです。"));
            
            Console.WriteLine("TextArranger (normal) test is completed.");
            Console.WriteLine();
            #endregion

            #region TextArranger (use event, InterceptableStream)
            Console.WriteLine("TextArranger (use event, InterceptableStream, QuickStreamWriter, DistributableStream)");

            TextArranger arranger2 = new TextArranger(Console.WindowWidth - 1, "Header | ", " | Footer", "\n");
            arranger2.Arranging += (sender, e) =>
            {
                e.Header += DateTime.Now.ToString("HH:mm:ss.fff") + " | ";
            };

            FileStream fileStream = File.Open("debug.txt", FileMode.Create, FileAccess.Write, FileShare.Read);
            InterceptableStream arrangerStream = new InterceptableStream();
            arrangerStream.Writing += (sender, e) =>
            {
                string input = encoding.GetString(e.Data);
                Console.WriteLine(arranger2.Arrange(input));
            };

            DistributableStream dStream = new DistributableStream(new Stream[]
            {
                fileStream,
                arrangerStream,
            });

            StreamWriter asWriter = new QuickStreamWriter(dStream, encoding, true);
            asWriter.WriteLine("さらに、書式を指定してDateTimeオブジェクトを文字列に変換することもできます。書式を指定するには、ToStringメソッドのパラメータに書式を文字列で渡します。");
            asWriter.WriteLine("日時を文字列に変換する時に使用できる書式の文字列は、「日時書式指定文字列」と呼びます。また、日時書式指定文字列は、「標準の日時書式指定文字列」と「カスタム日時書式指定文字列」に分けることができます。");

            Console.WriteLine("TextArranger (normal) test is completed.");
            Console.WriteLine();
            #endregion

            #region OverridableList
            Console.WriteLine("OverridableList");

            OverridableList<string> list = new OverridableList<string>();
            list.Add("keisei");
            list.Add("keikyu");
            list.Add("tokyu");
            foreach (var item in list)
                Console.WriteLine("item:" + item);

            Console.WriteLine("OverridableList test is completed.");
            Console.WriteLine();
            #endregion

            Console.ReadKey();
        }
    }
}
