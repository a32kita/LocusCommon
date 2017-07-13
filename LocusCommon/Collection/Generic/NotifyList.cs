using System;
using System.Collections.Generic;
using System.Text;

namespace LocusCommon.Collection.Generic
{
    public class NotifyList<T> : OverridableList<T>
    {
        // 非公開フィールド

        // 公開プロパティ

        // 非公開イベント
        private event NotifyListEventHandler<T> collectionChanged;


        // 公開イベント

        public event NotifyListEventHandler<T> CollectionChanged
        {
            add { this.collectionChanged += value; }
            remove { this.collectionChanged += value; }
        }


        // コンストラクタ

        /// <summary>
        /// 空で、既定の初期量を備えた、OverridableList&lt;T&gt; クラスの新しいインスタンスを初期化します。
        /// </summary>
        public NotifyList()
            : base()
        {
        }

        /// <summary>
        /// 指定したコレクションからコピーした要素を格納し、コピーされる要素の数を格納できるだけの容量を備えた、OverridableList&lt;T&gt; クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="collection"></param>
        public NotifyList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// 空で、指定した初期量を備えた、OverridableList&lt;T&gt; クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="capacity"></param>
        public NotifyList(int capacity)
            : base(capacity)
        {
        }
    }

    public delegate void NotifyListEventHandler<T>(Object sender, NotifyListEventArgs<T> e);

    public class NotifyListEventArgs<T>
    {
        // 非公開フィールド
        private T target;
        
    }

    /// <summary>
    /// 
    /// </summary>
    public enum NotifyListAction
    {
        /// <summary>
        /// アイテムを追加しました。
        /// </summary>
        Add,

        /// <summary>
        /// アイテムを除去しました。
        /// </summary>
        Remove,
    }
}
