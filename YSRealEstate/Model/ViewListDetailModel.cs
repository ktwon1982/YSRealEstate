using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using YSRealEstate.Command;
using YSRealEstate.DTO;
using System.Windows.Media.Imaging;

namespace YSRealEstate.Model
{
    public class ViewListDetailModel : Notifier
    {
        private string nowImage;

        public string NowImage
        {
            get { return nowImage; }
            set
            {
                nowImage = value;
                OnPropertyChanged("NowImage");
            }
        }

        private bool okEabled = false;
        public bool OKEabled
        {
            get { return okEabled; }
            set
            {
                okEabled = value;
                OnPropertyChanged("OKEabled");
            }
        }

        private bool updatebtnEabled = true;
        public bool UpdatebtnEabled
        {
            get { return updatebtnEabled; }
            set
            {
                updatebtnEabled = value;
                OnPropertyChanged("UpdatebtnEabled");
            }
        }
        private bool udateEabled = false;
        public bool UdateEabled
        {
            get { return udateEabled; }
            set
            {
                udateEabled = value;
                OnPropertyChanged("UdateEabled");
            }
        }

        private bool delImageEnable = false;
        public bool DelImageEnable
        {
            get { return delImageEnable; }
            set
            {
                delImageEnable = value;
                OnPropertyChanged("DelImageEnable");
            }
        }

        private EventCommand doubleClickCommand;
        public ICommand DoubleClickCommand
        {
            get
            {
                if (doubleClickCommand == null)
                {
                    doubleClickCommand = new EventCommand(KeyDown, obj => true);
                }
                return doubleClickCommand;
            }
        }

        private void KeyDown(object obj)
        {

        }

        private string estateNumber;

        public string EstateNumber
        {
            get { return estateNumber; }
            set
            {
                estateNumber = value;
                OnPropertyChanged("EstateNumber");
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private DateTime contractSelectedDate;
        public DateTime ContractSelectedDate
        {
            get
            {
                return contractSelectedDate;
            }
            set
            {
                contractSelectedDate = value;
                OnPropertyChanged("ContractSelectedDate");
            }
        }

        private DateTime contractEndSelectedDate;
        public DateTime ContractEndSelectedDate
        {
            get
            {
                return contractEndSelectedDate;
            }
            set
            {
                contractEndSelectedDate = value;
                OnPropertyChanged("ContractEndSelectedDate");
            }
        }

        private string spacious;

        public string Spacious
        {
            get { return spacious; }
            set
            {
                spacious = value;
                OnPropertyChanged("Spacious");
            }
        }

        private string floorNumber;

        public string FloorNumber
        {
            get { return floorNumber; }
            set
            {
                floorNumber = value;
                OnPropertyChanged("FloorNumber");
            }
        }

        private string estateType;

        public string EstateType
        {
            get { return estateType; }
            set
            {
                estateType = value;
                OnPropertyChanged("EstateType");
            }
        }

        private string deposit;

        public string Deposit
        {
            get { return deposit; }
            set
            {
                deposit = value;
                OnPropertyChanged("Deposit");
            }
        }

        private string elevator;

        public string Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                OnPropertyChanged("Elevator");
            }
        }

        private string hoist;

        public string Hoist
        {
            get { return hoist; }
            set
            {
                hoist = value;
                OnPropertyChanged("Hoist");
            }
        }

        private string floorHeight;

        public string FloorHeight
        {
            get { return floorHeight; }
            set
            {
                floorHeight = value;
                OnPropertyChanged("FloorHeight");
            }
        }

        private string power;

        public string Power
        {
            get { return power; }
            set
            {
                power = value;
                OnPropertyChanged("Power");
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private string maintenance;

        public string Maintenance
        {
            get { return maintenance; }
            set
            {
                maintenance = value;
                OnPropertyChanged("Maintenance");
            }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

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
        public CommonCommand NextImageCommand { get; private set; }
        public CommonCommand DelImageCommand { get; private set; }

        public CommonCommand RealEstateUpdateCommand { get; private set; }
        public CommonCommand RealEstateUpdateOKCommand { get; private set; }

        public event EventHandler RequestOK;

        Dictionary<int, System.Drawing.Bitmap> imageDictionary = new Dictionary<int, System.Drawing.Bitmap>();
        Dictionary<int, string> imageNameDictionary = new Dictionary<int, string>();
        //private List<String> imageNameLists;

        RealEstateInfoDTO details;
        RealEstateFactory realEstateFactory;
        //List<string> imageNameLists = new List<string>();
        int imgNum;
        int nextImageNum;
        string imagePath;

        FileInfo firstFile;

        public ViewListDetailModel(RealEstateInfoDTO details, RealEstateFactory factory)
        {
            if(details == null)
            {
                OnRequestOK();
                return;
            }

            DateTime today = DateTime.Today;


            this.details = details;
            imgNum = 0;
            nextImageNum = 0;

            AddImageCommand = new CommonCommand(AddImageCMD);
            NextImageCommand = new CommonCommand(NextImageCMD);
            DelImageCommand = new CommonCommand(DelImageCMD);

            RealEstateUpdateCommand = new CommonCommand(RealEstateUpdate);
            RealEstateUpdateOKCommand = new CommonCommand(RealEstateUpdateOK);

            realEstateFactory = factory;

            //imageNameLists = new List<string>();

            estateNumber = "매물 " + details.번호 + " 상세";

            selectedDate = Convert.ToDateTime(details.접수일);

            if(string.IsNullOrEmpty(details.계약체결일))
            {
                contractSelectedDate = today;
            }
            else
            {
                contractSelectedDate = Convert.ToDateTime(details.계약체결일);
            }

            if (string.IsNullOrEmpty(details.계약종료일))
            {
                contractEndSelectedDate = today;
            }
            else
            {
                contractEndSelectedDate = Convert.ToDateTime(details.계약종료일);
            }            

            spacious = details.평수.ToString();
            floorNumber = details.층수.ToString();
            estateType = details.매물구분;
            deposit = details.보증금.ToString();
            elevator = details.승강기.ToString();
            hoist = details.호이스트.ToString();
            floorHeight = details.층고.ToString();
            power = details.전력.ToString();
            address = details.주소;
            maintenance = details.담담자연락처;
            comment = details.비고;

            int number = Convert.ToInt16(details.번호);
            imagePath = @"./image/" + number;

            if (Directory.Exists(imagePath) == false)
            {
                Directory.CreateDirectory(imagePath);
            }
            else
            {
                DirFileSearch(imagePath,"jpg");
            }

            if(imageNameDictionary.Count < 1)
            {
                return;
            }

            int key = 0;
            //string filePath = imagePath + @"//" + imageNameDictionary[0];
            foreach (var pair in imageNameDictionary)
            {
                if (pair.Key != Convert.ToInt16(nowImage))
                {
                    key = pair.Key;
                    break;
                }
            }

            string filePath = imageNameDictionary[key];

            firstFile = new FileInfo(filePath);
            if (firstFile.Exists == true)
            {
                Bitmap Image = new Bitmap(filePath);

                RectangleF cloneRect = new RectangleF(0, 0, Image.Width, Image.Height);
                System.Drawing.Imaging.PixelFormat format =
                    Image.PixelFormat;
                Bitmap cloneBitmap = Image.Clone(cloneRect, format);


                imageSource = ImageMgr.ToWpfImage(cloneBitmap);
                this.OnPropertyChanged("ImageSource");

                nowImage = key.ToString();
                //nextImage = imgNum + 1;
                OnPropertyChanged("NowImage");

                imageDictionary.Add(key, cloneBitmap);
                Image.Dispose();
            }


        }

        protected void OnRequestOK()
        {
            EventHandler handler = this.RequestOK;
            if (handler != null)
                handler(this, EventArgs.Empty);
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
                Bitmap Image = new Bitmap(selectedFileName);

                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.UriSource = new Uri(selectedFileName);
                //bitmap.EndInit();
                //ImageSource = bitmap;

                imageSource = ImageMgr.ToWpfImage(Image);
                this.OnPropertyChanged("ImageSource");

                imgNum++;
                //화면에 보여줄 이미지 경로 
                //imageNameList.Add(selectedFileName);
                imageNameDictionary.Add(imgNum, selectedFileName);

                //저장
                System.Drawing.Bitmap saveimage = new System.Drawing.Bitmap(selectedFileName);
                //imageList.Add(saveimage);
                imageDictionary.Add(imgNum, saveimage);

                nowImage = imgNum.ToString();
                //nextImage = imgNum + 1;
                OnPropertyChanged("NowImage");

                Image.Dispose();


            }
        }



        private void NextImageCMD(object obj)
        {
           

#if true
            nextImageNum = Convert.ToInt16(nowImage) + 1;
            int key = 0;

            if (imageNameDictionary.ContainsKey(nextImageNum) == false)
            {
                foreach (var pair in imageNameDictionary)
                {
                    if (pair.Key != Convert.ToInt16(nowImage))
                    {
                        key = pair.Key;
                        break;
                    }
                }                
            }
            else
            {
                key = nextImageNum;
            }

            if (key == 0)
            {
                return;
            }

            string selectedFileName = imageNameDictionary[key];

            Bitmap Image = new Bitmap(selectedFileName);
            imageSource = ImageMgr.ToWpfImage(Image);
            this.OnPropertyChanged("ImageSource");


            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(selectedFileName);
            //bitmap.EndInit();
            //ImageSource = bitmap;

            nowImage = key.ToString();
            OnPropertyChanged("NowImage");
            Image.Dispose();
#endif
        }

        List<string> delList = new List<string>();
         
        private void DelImageCMD(object obj)
        {
            int delNum = Convert.ToInt16(nowImage);
            int imagQ = imageNameDictionary.Count;
            int key = 0;

            if (imageNameDictionary.ContainsKey(delNum) == true)
            {
                //string[] values = null;
                //values = imageNameDictionary[delNum].Split('\\');
                //int lastValue = values.Length;

                delList.Add(imageNameDictionary[delNum]);

                imageNameDictionary.Remove(delNum);
                imageDictionary.Remove(delNum);

                



                if (imageNameDictionary.Count == 0)
                {
                    imgNum = 0;

                    nowImage = null;
                    OnPropertyChanged("NowImage");

                    ImageSource = null;
                    return;
                }

                foreach (var pair in imageNameDictionary)
                {
                    key = pair.Key;
                    break;
                }

                //string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "image\\" + number + "\\image_" + pair.Key + ".jpg";

                //Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                string selectedFileName = imageNameDictionary[key];

                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.UriSource = new Uri(selectedFileName);
                //bitmap.EndInit();
                //ImageSource = bitmap;

                Bitmap Image = new Bitmap(selectedFileName);

                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.UriSource = new Uri(selectedFileName);
                //bitmap.EndInit();
                //ImageSource = bitmap;

                imageSource = ImageMgr.ToWpfImage(Image);
                this.OnPropertyChanged("ImageSource");


                nowImage = key.ToString();
                OnPropertyChanged("NowImage");

                Image.Dispose();
            }            
        }
        
        void DirFileSearch(string path, string file)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path, $"*.{file}");

                foreach (string f in files)
                {
                    imgNum++;
                    //imageNameDictionary.Add(imgNum, f);
                    string[] values = null;
                    values = f.Split('/');

                    string temp = values[2];
                    values = temp.Split('\\');

                    //string filePath = imagePath + @"/" + values[1];

                    string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "image\\" + values[0] + "\\" + values[1];

                    

                    imageNameDictionary.Add(imgNum, filePath);
                    //imageNameLists.Add(values[1]);
                    //Console.WriteLine($"[{count++}] path - {f}");
                }

                if (dirs.Length > 0)
                {
                    foreach (string dir in dirs)
                    {
                        DirFileSearch(dir, file);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void RealEstateUpdate(object obj)
        {
            udateEabled = true;
            OnPropertyChanged("UdateEabled");

            updatebtnEabled = false;
            OnPropertyChanged("UpdatebtnEabled");
            
            okEabled = true;
            OnPropertyChanged("OKEabled");

        }

        //RealEstateInfoDTO realEstateInfo = new RealEstateInfoDTO();

        private void RealEstateUpdateOK(object obj)
        {            
            int number = Convert.ToInt16(details.번호);

            IEnumerable<RealEstateInfoDTO> getAll = realEstateFactory.GetAllProducts();
            
            foreach (RealEstateInfoDTO realEstateInfo in getAll)
            {
                if(Convert.ToInt16(realEstateInfo.번호) == number)
                {
                    //realEstateFactory.DelRealEstate(i);

                    realEstateInfo.번호 = number.ToString();
                    realEstateInfo.접수일 = selectedDate.ToString();
                    realEstateInfo.계약체결일 = contractSelectedDate.ToString();
                    realEstateInfo.계약종료일 = contractEndSelectedDate.ToString();
                    realEstateInfo.평수 = spacious;
                    realEstateInfo.층수 = floorNumber;
                    realEstateInfo.매물구분 = estateType;
                    realEstateInfo.보증금 = deposit;
                    realEstateInfo.승강기 = elevator;
                    realEstateInfo.호이스트 = hoist;
                    realEstateInfo.층고 = floorHeight;
                    realEstateInfo.전력 = power;
                    realEstateInfo.주소 = address;
                    realEstateInfo.담담자연락처 = maintenance;
                    realEstateInfo.비고 = comment;
                }
            }


            //realEstateFactory.AddRealEstate(realEstateInfo);

            realEstateFactory.WriteCSV();
            
            string imagePath = @"./image/" + number;

            if (Directory.Exists(imagePath) == false)
            {
                Directory.CreateDirectory(imagePath);
            }

            int i = 0;

            foreach (var pair in imageNameDictionary)
            {

            }
                     
            foreach (var pair in imageDictionary)
            {
                try
                {
                    //string delfilePath = System.AppDomain.CurrentDomain.BaseDirectory + "image\\" + number;
                    //string filePath = imagePath + @"/image_" + i + ".jpg";
                    string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "image\\" + number + "\\image_" + pair.Key + ".jpg";

                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists == true)
                    {
                        file.Delete();
                    }

                    pair.Value.Save(filePath);

                    i++;
                }
                catch (Exception)
                {

                }
            }

            foreach (string filePath in delList)
            {
                FileInfo file = new FileInfo(filePath);
                
                if (file.Exists == true)
                {
                    file.Delete();
                }
            }

            OnRequestOK();            
        }
    }
}
