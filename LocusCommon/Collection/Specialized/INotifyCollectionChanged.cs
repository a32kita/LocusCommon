using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Collection.Specialized
{
    /// <summary>
    /// 項目が追加、削除された場合やリスト全体がクリアされた場合など、動的な変更をリスナーに通知します。
    /// </summary>
    public interface INotifyCollectionChanged
    {
        /// <summary>
        /// コレクションが変更されたときに発生します。
        /// </summary>
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
