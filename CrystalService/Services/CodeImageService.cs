using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;

namespace CrystalService.Services
{
    public static class CodeImageService
    {
        public static string GenerateQrPng(string content, string tempDir)
        {
            Directory.CreateDirectory(tempDir);

            var file = Path.Combine(tempDir, "qr_" + Guid.NewGuid().ToString("N") + ".png");

            using (var generator = new QRCodeGenerator())
            using (var data = generator.CreateQrCode(content ?? "", QRCodeGenerator.ECCLevel.Q))
            using (var qr = new QRCode(data))
            using (var bmp = qr.GetGraphic(8))
            {
                bmp.Save(file, ImageFormat.Png);
            }

            return file;
        }

        public static string GenerateCode128Png(string content, string tempDir)
        {
            Directory.CreateDirectory(tempDir);

            var file = Path.Combine(tempDir, "bar_" + Guid.NewGuid().ToString("N") + ".png");

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 90,
                    Width = 520,
                    Margin = 2,
                    PureBarcode = true
                }
            };

            var pixelData = writer.Write(content ?? "");
            using (var bmp = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.WriteOnly, bmp.PixelFormat);

                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bmpData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bmp.UnlockBits(bmpData);
                }

                bmp.Save(file, ImageFormat.Png);
            }

            return file;
        }
    }
}