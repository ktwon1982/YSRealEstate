using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using YSRealEstate.Command;

namespace YSRealEstate.Model
{
    class ViewRegistModel : Notifier
    {
        private System.Windows.Media.ImageSource imageSource;
        public System.Windows.Media.ImageSource ImageSource
        {
            get { return this.imageSource; }
            set
            {
                this.imageSource = value;
                this.OnPropertyChanged("ImageSource");
            }
        }

        public CommonCommand AddImageCommand { get; private set; }
        public CommonCommand RealEstateRegistCommand { get; private set; }

        public ViewRegistModel()
        {
            AddImageCommand = new CommonCommand(AddImageCMD);
            RealEstateRegistCommand = new CommonCommand(RealEstateRegist);
        }

        private void AddImageCMD(object obj)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Images";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG |*.jpg| GIF |*.gif"; Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName); bitmap.EndInit();
                ImageSource = bitmap;

            }
        }

        private void RealEstateRegist(object obj)
        {
        }
    }
}
