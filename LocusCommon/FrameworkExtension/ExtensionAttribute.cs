#if NET20

using System;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// メソッドが拡張メソッドであること、またはクラスやアセンブリに拡張メソッドが含まれていることを示します。
    /// この属性は LocusCommon の中で定義されます。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute
    {

    }
}

#endif