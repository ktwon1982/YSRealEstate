using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YSRealEstate.Model
{
    public static class ImageMgr
    {
        public static ImageSource ToWpfImage(this System.Drawing.Image img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Seek(0, SeekOrigin.Begin);
                return BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            catch (Exception)
            {
                //LogMgr.Log(null, ex.Message);
            }
            return null;
        }
    }
}
