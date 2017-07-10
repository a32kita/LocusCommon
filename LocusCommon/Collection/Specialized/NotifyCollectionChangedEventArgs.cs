using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Collection.Specialized
{
    /// <summary>
    /// CollectionChanged イベントのデータを提供します。
    /// </summary>
    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        // 非公開フィールド
        private NotifyCollectionChangedAction action;
        private IList newItems;
        private int newStartingIndex;
        private IList oldItems;
        private int oldStartingIndex;


        // 公開プロパティ

        /// <summary>
        /// イベントの原因となったアクションを取得します。
        /// </summary>
        public NotifyCollectionChangedAction Action
        {
            get { return this.action; }
        }

        /// <summary>
        /// この変更に関係のある新しい項目の一覧を取得します。
        /// </summary>
        public IList NewItems
        {
            get { return this.newItems; }
        }

        /// <summary>
        /// 変更が発生した位置のインデックスを取得します。
        /// </summary>
        public int NewStartingIndex
        {
            get { return this.newStartingIndex; }
        }

        /// <summary>
        /// Replace、削除、または移動アクションの影響を受ける項目の一覧を取得します。
        /// </summary>
        public IList OldItems
        {
            get { return this.oldItems; }
        }

        /// <summary>
        /// Move、Remove、または Replaceアクションが発生した位置のインデックスを取得します。
        /// </summary>
        public int OldStartingIndex
        {
            get { return this.oldStartingIndex; }
        }


        // コンストラクタ

        private NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex, IList oldItems, int oldStartingIndex)
        {
            this.action = action;
            this.newItems = newItems;
            this.newStartingIndex = newStartingIndex;
            this.oldItems = oldItems;
            this.oldStartingIndex = oldStartingIndex;
        }

        /// <summary>
        /// Reset の変更を記述する NotifyCollectionChangedEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action"></param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
            : this(action, null, -1, null, -1)
        {
        }

        /// <summary>
        /// 複数項目の変更を記述 する NotifyCollectionChangedEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action"></param>
        /// <param name="changedItems"></param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
        {
        }
    }
}
