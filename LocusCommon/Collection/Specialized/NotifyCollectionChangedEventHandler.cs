using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Collection.Specialized
{
    /// <summary>
    /// CollectionChanged イベントを処理するメソッドを表します。
    /// </summary>
    /// <param name="sender">イベントを発生させたオブジェクト。</param>
    /// <param name="e">イベントに関する情報。</param>
    public delegate void NotifyCollectionChangedEventHandler(Object sender, NotifyCollectionChangedEventArgs e);
}
