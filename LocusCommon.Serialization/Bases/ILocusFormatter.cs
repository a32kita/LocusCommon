using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LocusCommon.Serialization.Bases
{
    /// <summary>
    /// LocusCommonの提供するFormatterクラスであることを表します。
    /// </summary>
    public interface ILocusFormatter
    {
        // プロパティ


        // メソッド

        /// <summary>
        /// 指定したオブジェクトをシリアライズします。
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="obj"></param>
        void Serialize(Stream stream, Object obj);

        /// <summary>
        /// 指定したストリームから、指定した型でデシリアライズします。
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        Object Deserialize(Stream stream, Type targetType);

        /// <summary>
        /// 指定したストリームから、<typeparamref name="TTarget"/> でデシリアライズします。
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        TTarget Deserialize<TTarget>(Stream stream);
    }
}
