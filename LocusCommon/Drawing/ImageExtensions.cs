#if NET40||NET45||NET461

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace LocusCommon.Drawing
{
    using ImageSource = System.Windows.Media.ImageSource;
    using BitmapCacheOption = System.Windows.Media.Imaging.BitmapCacheOption;
    using BitmapCreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions;
    using BitmapDecoder = System.Windows.Media.Imaging.BitmapDecoder;
    using WritableBitmap = System.Windows.Media.Imaging.WriteableBitmap;
    
    /// <summary>
    /// System.Drawing.Image クラスに対する拡張メソッドを提供します。
    /// </summary>
    public static class ImageExtensions
    {
        // 非公開静的フィールド

        // 公開静的プロパティ

        // 静的コンストラクタ

        /// <summary>
        /// ImageExtensions クラスを初期化します。
        /// </summary>
        static ImageExtensions()
        {

        }


        // 非公開静的メソッド

        // 公開静的メソッド
        
        /// <summary>
        /// System.Windows.Media.ImageSource を生成します。
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static ImageSource CreateImageSource(this Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var bitmapDecoder = BitmapDecoder.Create(
                    memoryStream,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.OnLoad);

                var writable = new WritableBitmap(bitmapDecoder.Frames.Single());
                writable.Freeze();

                return writable;
            }
        }
    }
}

#endif