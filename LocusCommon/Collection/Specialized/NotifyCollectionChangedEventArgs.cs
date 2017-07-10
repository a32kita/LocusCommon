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
        /// <param name="action">イベントの原因となったアクション。 これは Reset、Add、または Remove に設定できます。</param>
        /// <param name="changedItems"></param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
            : this(action, null, -1, null, -1)
        {
            if (action == NotifyCollectionChangedAction.Add)
                this.newItems = changedItems;
            else if (action == NotifyCollectionChangedAction.Remove || action == NotifyCollectionChangedAction.Reset)
                this.oldItems = changedItems;
        }

        /// <summary>
        /// 複数項目の Replace の変更について記述する NotifyCollectionChangedEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action">イベントの原因となったアクション。 Replace にのみ設定できます。</param>
        /// <param name="newItems">元の項目を置き換える新しい項目。</param>
        /// <param name="oldItems">置き換えられた元の項目。</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
            : this(action, newItems, -1, oldItems, -1)
        {
        }

        /// <summary>
        /// 複数項目の Replace の変更について記述する NotifyCollectionChangedEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action">イベントの原因となったアクション。 Replace にのみ設定できます。</param>
        /// <param name="newItems">元の項目を置き換える新しい項目。</param>
        /// <param name="oldItems">置き換えられる元の項目。</param>
        /// <param name="startingIndex">置き換えられる項目の最初の項目のインデックス。</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
            : this(action, newItems, startingIndex, oldItems, startingIndex)
        {
        }

        /// <summary>
        /// 複数項目の変更または Reset による変更を記述する NotifyCollectionChangedEventArgs クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="action">イベントの原因となったアクション。 これは Reset、Add、または Remove に設定できます。</param>
        /// <param name="changedItems">変更の影響を受ける項目。</param>
        /// <param name="startingIndex">変更が発生したインデックス。</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
            : this(action, changedItems, startingIndex, changedItems, startingIndex)
        {
            if (action == NotifyCollectionChangedAction.Add)
            {
                this.oldItems = null;
                this.oldStartingIndex = 0;
            }
            else if (action == NotifyCollectionChangedAction.Remove || action == NotifyCollectionChangedAction.Reset)
            {
                this.newItems = null;
                this.newStartingIndex = 0;
            }
        }

        /// <summary>
        /// NotifyCollectionChangedEventArgs クラスの新しいインスタンスを初期化し、複数項目 Move の変化を記述します。
        /// </summary>
        /// <param name="action">イベントの原因となったアクション。 これは Move にのみ設定できます。</param>
        /// <param name="changedItems">変化の影響を受ける項目。</param>
        /// <param name="index">変化した項目の新しいインデックス。</param>
        /// <param name="oldIndex">変化した項目の古いインデックス。</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
            : this(action, changedItems, index, changedItems, oldIndex)
        {
        }
    }
}
