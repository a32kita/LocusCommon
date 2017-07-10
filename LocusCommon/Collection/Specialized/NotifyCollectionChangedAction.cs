using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Collection.Specialized
{
    /// <summary>
    /// CollectionChanged イベントの原因となったアクションについて記述します。
    /// </summary>
    public enum NotifyCollectionChangedAction
    {
        /// <summary>
        /// コレクションに項目が追加されました。
        /// </summary>
        Add,

        /// <summary>
        /// コレクション内で項目が移動されました。
        /// </summary>
        Move,

        /// <summary>
        /// コレクションから項目が削除されました。
        /// </summary>
        Remove,

        /// <summary>
        /// コレクションの項目が置き換えられました。
        /// </summary>
        Replace,

        /// <summary>
        /// コレクションの内容が消去されました。
        /// </summary>
        Reset,
    }
}
