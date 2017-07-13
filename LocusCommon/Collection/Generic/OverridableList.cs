using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LocusCommon.Collection.Generic
{
    /// <summary>
    /// すべてのメンバがvirtualで定義されたジェネリックな標準List機能を提供します。List&lt;<typeparamref name="T"/>&gt;型へのキャスト機能も提供しております。
    /// </summary>
    [Locus(Purpose="Listの各処理に対して、オーバーライドで拡張などを行いたい場合に利用します。")]
    public class OverridableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
    {
        // 非公開フィールド
        private List<T> internalList;


        // 公開プロパティ
        #region 公開プロパティ
        
        /// <summary>
        /// 指定したインデックスにある要素を取得または設定します。
        /// </summary>
        /// <param name="index">取得または設定する要素の、0 から始まるインデックス番号。</param>
        /// <returns>指定したインデックスにある要素。</returns>
        public virtual T this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        /// <summary>
        /// 内部データ構造体がサイズ変更せずに格納できる要素の合計数を取得または設定します。
        /// </summary>
        public virtual int Capacity {
            get { return this.internalList.Capacity; }
            set { this.internalList.Capacity = value; }
        }

        /// <summary>
        /// OverridableList に実際に格納されている要素の数を取得します。
        /// </summary>
        public virtual int Count
        {
            get { return this.internalList.Count; }
        }

        /// <summary>
        /// OverridableList が読み取り専用かどうかを示す値を取得します。このプロパティは、IList向けの実装です。
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return ((IList)this.internalList).IsReadOnly; }
        }

        /// <summary>
        /// OverridableList が読み取り専用かどうかを示す値を取得します。このプロパティは、IList向けの実装です。
        /// </summary>
        public virtual bool IsFixedSize
        {
            get { return ((IList)this.internalList).IsFixedSize; }
        }

        /// <summary>
        /// OverridableList へのアクセスが同期されている (スレッド セーフである) かどうかを示す値を取得します。このプロパティは、ICollection向けの実装です。
        /// </summary>
        public virtual bool IsSynchronized
        {
            get { return ((ICollection)this.internalList).IsSynchronized; }
        }

        /// <summary>
        /// OverridableList へのアクセスを同期するために使用できるオブジェクトを取得します。このプロパティは、ICollection向けの実装です。
        /// </summary>
        public virtual Object SyncRoot
        {
            get { return ((ICollection)this.internalList).SyncRoot; }
        }

        #endregion


        // 公開プロパティ :: 明示的なインターフェイスの実装
        #region 公開プロパティ :: 明示的なインターフェイスの実装

        Object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }


        #endregion


        // コンストラクタ
        #region コンストラクタ

        /// <summary>
        /// 空で、既定の初期量を備えた、OverridableList&lt;T&gt; クラスの新しいインスタンスを初期化します。
        /// </summary>
        public OverridableList()
        {
            this.internalList = new List<T>();
        }

        /// <summary>
        /// 指定したコレクションからコピーした要素を格納し、コピーされる要素の数を格納できるだけの容量を備えた、OverridableList&lt;T&gt; クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="collection"></param>
        public OverridableList(IEnumerable<T> collection)
        {
            this.internalList = new List<T>(collection);
        }

        /// <summary>
        /// 空で、指定した初期量を備えた、OverridableList&lt;T&gt; クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="capacity"></param>
        public OverridableList(int capacity)
        {
            this.internalList = new List<T>(capacity);
        }

        #endregion


        // 公開メソッド
        #region 公開メソッド

        /// <summary>
        /// 現在のOverridableListを List&lt;<typeparamref name="T"/>&gt; へ変換したものを取得します。
        /// </summary>
        /// <returns>OverridableListの内容を複製した List&lt;<typeparamref name="T"/>&gt;</returns>
        public List<T> AsList()
        {
            return new List<T>(this.internalList.ToArray());
        }
            
        #endregion


        // 公開メソッド :: List機能の実装
        #region 公開メソッド :: List機能の実装

        /// <summary>
        /// OverridableList&lt;T&gt; の末尾にオブジェクトを追加します。
        /// </summary>
        /// <param name="item">OverridableList&lt;T&gt; の末尾に追加するオブジェクト。 参照型の場合は null の値を使用できます。</param>
        public virtual void Add(T item)
        {
            this.internalList.Add(item);
        }

        /// <summary>
        /// 指定したコレクションの要素を OverridableList&lt;T&gt; の末尾に追加します。
        /// </summary>
        /// <param name="collection"> OverridableList&lt;T&gt; の末尾に要素が追加されるコレクション。 コレクション自体を null にすることはできませんが、型 T が参照型の場合、コレクションに格納する要素は null であってもかまいません。</param>
        public virtual void AddRange(IEnumerable<T> collection)
        {
            this.internalList.AddRange(collection);
        }
        
        /// <summary>
        /// 現在のコレクションの読み取り専用の ReadOnlyCollection&lt;T&gt; ラッパーを返します。
        /// </summary>
        /// <returns>現在の OverridableList&lt;T&gt; をラップする読み取り専用のラッパーとして動作するオブジェクト。</returns>
        public virtual ReadOnlyCollection<T> AsReadOnly()
        {
            return this.internalList.AsReadOnly();
        }

        /// <summary>
        /// 既定の比較演算子を使用して、並べ替えられた要素の OverridableList 全体を検索し、その要素の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="item">検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <returns></returns>
        public virtual int BinarySearch(T item)
        {
            return this.internalList.BinarySearch(item);
        }

        /// <summary>
        /// 指定した比較演算子を使用して、並べ替えられた要素の List 全体を検索し、その要素の 0 から始まるインデックスを返します。 
        /// </summary>
        /// <param name="item">検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <param name="comparer">要素を比較する場合に使用する IComparer 実装。または、既定の比較演算子 Comparer.Default を使用する場合は null 参照 (Visual Basic では Nothing)。</param>
        /// <returns></returns>
        public virtual int BinarySearch(T item, IComparer<T> comparer)
        {
            return this.internalList.BinarySearch(item, comparer);
        }

        /// <summary>
        /// 指定した比較演算子を使用して、並べ替えられた要素の OverridableList の 1 つの要素の範囲を検索し、その要素の 0 から始まるインデックスを返します。 
        /// </summary>
        /// <param name="index">検索範囲の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">検索する範囲の長さ。</param>
        /// <param name="item">検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <param name="comparer">要素を比較する場合に使用する IComparer 実装。または、既定の比較演算子 Comparer.Default を使用する場合は null 参照 (Visual Basic では Nothing)。</param>
        /// <returns></returns>
        public virtual int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return this.internalList.BinarySearch(index, count, item, comparer);
        }

        /// <summary>
        /// OverridableList からすべての要素を削除します。 
        /// </summary>
        public virtual void Clear()
        {
            this.internalList.Clear();
        }

        /// <summary>
        /// ある要素が OverridableList 内に存在するかどうかを判断します。
        /// </summary>
        /// <param name="item">List 内で検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <returns></returns>
        public virtual bool Contains(T item)
        {
            return this.internalList.Contains(item);
        }

        /// <summary>
        /// 現在の OverridableList の要素を別の型に変換し、変換された要素が格納されたリストを返します。
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="converter">各要素の型を変換するための Converter デリゲート。</param>
        /// <returns>現在の List の要素の型を変換した後の List。 </returns>
        public virtual List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return this.internalList.ConvertAll(converter);
        }

        /// <summary>
        /// OverridableList 全体を互換性のある 1 次元の配列にコピーします。コピー操作は、コピー先の配列の先頭から始まります。 
        /// </summary>
        /// <param name="array">OverridableList から要素がコピーされる 1 次元の Array。Array には、0 から始まるインデックス番号が必要です。</param>
        public virtual void CopyTo(T[] array)
        {
            this.internalList.CopyTo(array);
        }

        /// <summary>
        /// すべての OverridableList を互換性のある 1 次元配列にコピーします。コピー操作は、コピー先の配列の指定したインデックスから始まります。 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            this.internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 要素の範囲を OverridableList から互換性のある 1 次元の配列にコピーします。コピー操作は、コピー先の配列の指定したインデックスから始まります。
        /// </summary>
        /// <param name="index">コピーを開始するコピー元の OverridableList 内の、0 から始まるインデックス番号。</param>
        /// <param name="array">OverridableList から要素がコピーされる 1 次元の Array。Array には、0 から始まるインデックス番号が必要です。</param>
        /// <param name="arrayIndex">コピーの開始位置となる、array の 0 から始まるインデックス番号。</param>
        /// <param name="count">コピーする要素の数。</param>
        public virtual void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            this.internalList.CopyTo(index, array, arrayIndex, count);
        }

        /// <summary>
        /// OverridableList に、指定された述語によって定義された条件と一致する要素が含まれているかどうかを判断します。 
        /// </summary>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>指定された述語によって定義された条件と一致する要素が少なくとも 1 つ、OverridableList に存在する場合は、true。それ以外の場合は false。</returns>
        public virtual bool Exists(Predicate<T> match)
        {
            return this.internalList.Exists(match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を検索し、OverridableList 全体の中で最もインデックス番号の小さい要素を返します。
        /// </summary>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>見つかった場合は、指定された述語によって定義された条件と一致する最初の要素。それ以外の場合は、型 T の既定値。</returns>
        public virtual T Find(Predicate<T> match)
        {
            return this.internalList.Find(match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致するすべての要素を取得します。
        /// </summary>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>見つかった場合は、指定された述語によって定義された条件と一致するすべての要素を格納した List。それ以外の場合は、空の List。</returns>
        public virtual List<T> FindAll(Predicate<T> match)
        {
            return this.internalList.FindAll(match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を、OverridableList 全体を対象に検索し、最もインデックス番号の小さい要素の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>match で定義された条件と一致する要素が存在した場合、最もインデックス番号の小さい要素の 0 から始まるインデックス。それ以外の場合は –1。</returns>
        public virtual int FindIndex(Predicate<T> match)
        {
            return this.internalList.FindIndex(match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を、OverridableList の指定されたインデックスから、最後の要素までを範囲として検索し、最もインデックス番号の小さい要素の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="startIndex">検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns></returns>
        public virtual int FindIndex(int startIndex, Predicate<T> match)
        {
            return this.internalList.FindIndex(startIndex, match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を、OverridableList の指定されたインデックスから、指定された要素数を範囲として検索し、最もインデックス番号の小さい要素の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="startIndex">検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">検索対象の範囲内にある要素の数。</param>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns></returns>
        public virtual int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return this.internalList.FindIndex(startIndex, count, match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を、OverridableList 全体を対象に検索し、最もインデックス番号の大きい要素を返します。
        /// </summary>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>見つかった場合は、指定された述語によって定義された条件と一致する最後の要素。それ以外の場合は、型 T の既定値。</returns>
        public virtual T FindLast(Predicate<T> match)
        {
            return this.internalList.FindLast(match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を、OverridableList 全体を対象に検索し、最もインデックス番号の大きい要素の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>match で定義された条件と一致する要素が存在する場合、最もインデックス番号の大きい要素の 0 から始まるインデックス。それ以外の場合は –1。</returns>
        public virtual int FindLastIndex(Predicate<T> match)
        {
            return this.internalList.FindLastIndex(match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を、OverridableList の先頭の要素から、指定されたインデックス位置までを範囲として検索し、最もインデックス番号の大きい要素の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="startIndex">後方検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns></returns>
        public virtual int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return this.internalList.FindLastIndex(startIndex, match);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致する要素を、OverridableList の指定されたインデックス位置から、指定された要素数までを範囲として検索し、最もインデックス番号の大きい要素の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="startIndex">後方検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">検索対象の範囲内にある要素の数。</param>
        /// <param name="match">検索する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>match で定義された条件と一致する要素が存在する場合、最もインデックス番号の大きい要素の 0 から始まるインデックス。それ以外の場合は –1。</returns>
        public virtual int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return this.internalList.FindLastIndex(startIndex, count, match);
        }

        /// <summary>
        /// OverridableList の各要素に対して実行する Action デリゲート。
        /// </summary>
        /// <param name="action">OverridableList の各要素に対して実行する Action デリゲート。</param>
        public virtual void ForEach(Action<T> action)
        {
            this.internalList.ForEach(action);
        }

        /// <summary>
        /// OverridableList を反復処理する列挙子を返します。
        /// </summary>
        /// <returns>OverridableList の List.Enumerator。</returns>
        public virtual List<T>.Enumerator GetEnumerator()
        {
            return this.internalList.GetEnumerator();
        }

        /// <summary>
        /// コピー元の OverridableList 内の、ある範囲の要素の簡易コピーを作成します。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual List<T> GetRange(int index, int count)
        {
            return this.internalList.GetRange(index, count);
        }

        /// <summary>
        /// 指定したオブジェクトを検索し、List 全体内で最初に見つかった位置の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="item">OverridableList 内で検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <returns>index から始まって count 個の要素を格納する List 内の要素の範囲内で item が見つかった場合は、最初に見つかった位置の 0 から始まるインデックス番号。それ以外の場合は –1。</returns>
        public virtual int IndexOf(T item)
        {
            return this.internalList.IndexOf(item);
        }

        /// <summary>
        /// 指定したオブジェクトを検索し、指定したインデックスから最後の要素までの List 内の要素の範囲内で最初に出現する位置の 0 から始まるインデックス番号を返します。
        /// </summary>
        /// <param name="item">OverridableList 内で検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <param name="index">検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <returns>index から始まって count 個の要素を格納する List 内の要素の範囲内で item が見つかった場合は、最初に見つかった位置の 0 から始まるインデックス番号。それ以外の場合は –1。</returns>
        public virtual int IndexOf(T item, int index)
        {
            return this.internalList.IndexOf(item, index);
        }

        /// <summary>
        /// 指定したオブジェクトを検索し、指定したインデックスから始まって指定した数の要素を格納する List 内の要素の範囲内で最初に出現する位置の 0 から始まるインデックス番号を返します。
        /// </summary>
        /// <param name="item">OverridableList 内で検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <param name="index">検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">検索対象の範囲内にある要素の数。</param>
        /// <returns>index から始まって count 個の要素を格納する List 内の要素の範囲内で item が見つかった場合は、最初に見つかった位置の 0 から始まるインデックス番号。それ以外の場合は –1。</returns>
        public virtual int IndexOf(T item, int index, int count)
        {
            return this.internalList.IndexOf(item, index, count);
        }

        /// <summary>
        /// OverridableList 内の指定したインデックスの位置に要素を挿入します。 
        /// </summary>
        /// <param name="index">item を挿入する位置の、0 から始まるインデックス番号。</param>
        /// <param name="item">挿入するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        public virtual void Insert(int index, T item)
        {
            this.internalList.Insert(index, item);
        }

        /// <summary>
        /// コレクションの要素を OverridableList 内の指定したインデックスの位置に挿入します。
        /// </summary>
        /// <param name="index">新しい要素が挿入される位置の 0 から始まるインデックス。</param>
        /// <param name="collection">List に要素を挿入するコレクション。コレクション自体を null 参照 (Visual Basic では Nothing) にすることはできませんが、型 T が参照型の場合、コレクションに格納する要素は null 参照 (Visual Basic では Nothing) であってもかまいません。</param>
        public virtual void InsertRange(int index, IEnumerable<T> collection)
        {
            this.internalList.InsertRange(index, collection);
        }

        /// <summary>
        /// 指定したオブジェクトを検索し、OverridableList 全体内で最後に見つかった位置の 0 から始まるインデックスを返します。
        /// </summary>
        /// <param name="item">List 内で検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <returns>OverridableList 全体内で item が見つかった場合は、最後に見つかった位置の 0 から始まるインデックス番号。それ以外の場合は –1。</returns>
        public virtual int LastIndexOf(T item)
        {
            return this.internalList.LastIndexOf(item);
        }

        /// <summary>
        /// 指定したオブジェクトを検索し、最初の要素から、指定したインデックスまでの List 内の要素の範囲内で最後に出現する位置の 0 から始まるインデックス番号を返します。
        /// </summary>
        /// <param name="item">OverridableList 内で検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <param name="index">後方検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <returns></returns>
        public virtual int LastIndexOf(T item, int index)
        {
            return this.internalList.LastIndexOf(item, index);
        }

        /// <summary>
        /// 指定したオブジェクトを検索して、指定した数の要素を格納し、指定したインデックスの位置で終了する OverridableList 内の要素の範囲内で最後に出現する位置の 0 から始まるインデックス番号を返します。
        /// </summary>
        /// <param name="item">OverridableList 内で検索するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <param name="index">後方検索の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">検索対象の範囲内にある要素の数。</param>
        /// <returns>count 個の要素を格納し、index の位置で終了する OverridableList 内の要素の範囲内で item が見つかった場合は、最後に見つかった位置の 0 から始まるインデックス番号。それ以外の場合は –1。</returns>
        public virtual int LastIndexOf(T item, int index, int count)
        {
            return this.internalList.LastIndexOf(item, index, count);
        }

        /// <summary>
        /// OverridableList 内で最初に見つかった特定のオブジェクトを削除します。
        /// </summary>
        /// <param name="item">OverridableList から削除するオブジェクト。参照型の場合、null 参照 (Visual Basic では Nothing) の値を使用できます。</param>
        /// <returns>item が正常に削除された場合は true。それ以外の場合は false。このメソッドは、item が OverridableList に見つからなかった場合にも false を返します。</returns>
        public virtual bool Remove(T item)
        {
            return this.internalList.Remove(item);
        }

        /// <summary>
        /// 指定された述語によって定義された条件と一致するすべての要素を削除します。
        /// </summary>
        /// <param name="match">削除する要素の条件を定義する Predicate デリゲート。</param>
        /// <returns>OverridableList から削除される要素の数。</returns>
        public virtual int RemoveAll(Predicate<T> match)
        {
            return this.internalList.RemoveAll(match);
        }

        /// <summary>
        /// OverridableList の指定したインデックスにある要素を削除します。
        /// </summary>
        /// <param name="index">削除する要素の、0 から始まるインデックス番号。</param>
        public virtual void RemoveAt(int index)
        {
            this.internalList.RemoveAt(index);
        }

        /// <summary>
        /// OverridableList から要素の範囲を削除します。
        /// </summary>
        /// <param name="index">削除する要素の範囲の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">削除する要素の数。</param>
        public virtual void RemoveRange(int index, int count)
        {
            this.internalList.RemoveRange(index, count);
        }

        /// <summary>
        /// OverridableList 全体の要素の順序を反転させます。
        /// </summary>
        public virtual void Reverse()
        {
            this.internalList.Reverse();
        }

        /// <summary>
        /// 指定した範囲の要素の順序を反転させます。
        /// </summary>
        /// <param name="index">反転させる範囲の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">反転させる範囲内にある要素の数。</param>
        public virtual void Reverse(int index, int count)
        {
            this.internalList.Reverse(index, count);
        }

        /// <summary>
        /// 既定の比較演算子を使用して、OverridableList 全体内の要素を並べ替えます。
        /// </summary>
        public virtual void Sort()
        {
            this.internalList.Sort();
        }

        /// <summary>
        /// 指定した System.Comparison を使用して、OverridableList 全体内の要素を並べ替えます。
        /// </summary>
        /// <param name="comparison">要素を比較する場合に使用する System.Comparison。</param>
        public virtual void Sort(Comparison<T> comparison)
        {
            this.internalList.Sort(comparison);
        }

        /// <summary>
        /// 指定した比較演算子を使用して、OverridableList 全体内の要素を並べ替えます。
        /// </summary>
        /// <param name="comparer">要素を比較する場合に使用する IComparer 実装。または、既定の比較演算子 Comparer.Default を使用する場合は null 参照 (Visual Basic では Nothing)。</param>
        public virtual void Sort(IComparer<T> comparer)
        {
            this.internalList.Sort(comparer);
        }

        /// <summary>
        /// 指定した比較演算子を使用して、OverridableList 内の要素の範囲内の要素を並べ替えます。
        /// </summary>
        /// <param name="index">並べ替える範囲の開始位置を示す 0 から始まるインデックス。</param>
        /// <param name="count">並べ替える範囲の長さ。</param>
        /// <param name="comparer">要素を比較する場合に使用する IComparer 実装。または、既定の比較演算子 Comparer.Default を使用する場合は null 参照 (Visual Basic では Nothing)。</param>
        public virtual void Sort(int index, int count, IComparer<T> comparer)
        {
            this.internalList.Sort(index, count, comparer);
        }

        #endregion


        // 公開メソッド :: 明示的なインターフェイスの実装
        #region 公開メソッド :: 明示的なインターフェイスの実装

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)this.internalList).GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            ((ICollection)this.internalList).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.internalList).GetEnumerator();
        }
        
        int IList.Add(Object item)
        {
            //return ((IList)this.internalList).Add(item);
            //return this.Add((T)item);

            this.Add((T)item);
            return this.Count - 1;
        }

        bool IList.Contains(Object item)
        {
            //return ((IList)this.internalList).Contains(item);
            return this.Contains((T)item);
        }

        int IList.IndexOf(Object item)
        {
            //return ((IList)this.internalList).IndexOf(item);
            return this.IndexOf((T)item);
        }

        void IList.Insert(int index, Object item)
        {
            //((IList)this.internalList).Insert(index, item);
            this.Insert(index, (T)item);
        }

        void IList.Remove(Object item)
        {
            //((IList)this.internalList).Remove(item);
            this.Remove((T)item);
        }

        #endregion


        // 公開静的メソッド
        #region 公開静的メソッド

        /// <summary>
        /// 内部のListを取得します。
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator List<T> (OverridableList<T> source)
        {
            return source.internalList;
        }


        #endregion


            // 公開内部クラス

        }
}
