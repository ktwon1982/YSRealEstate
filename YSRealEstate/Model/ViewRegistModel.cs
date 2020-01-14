using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using YSRealEstate.Command;
using YSRealEstate.DTO;

namespace YSRealEstate.Model
{
    class ViewRegistModel : Notifier
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

        public event EventHandler RequestOK;

        public CommonCommand AddImageCommand { get; private set; }
        public CommonCommand NextImageCommand { get; private set; }
        public CommonCommand DelImageCommand { get; private set; }

        public CommonCommand RealEstateRegistCommand { get; private set; }


        RealEstateInfoDTO realEstateInfo = new RealEstateInfoDTO();    
        DateTime today;

        //List<string> imageNameLists = new List<string>();
        int imgNum;
        int nextImageNum;
        //int nowImageNum;

        public ViewRegistModel(RealEstateInfoDTO realEstateInfo, RealEstateFactory factory)
        {
            imgNum = 0;
            nextImageNum = 0;
           // nowImageNum = 0;
            realEstateFactory = factory;
            //realEstateInfo = new RealEstateInfoDTO();
            AddImageCommand = new CommonCommand(AddImageCMD);
            NextImageCommand = new CommonCommand(NextImageCMD);
            DelImageCommand = new CommonCommand(DelImageCMD);

            //RealEstateRegistCommand = new CommonCommand(RealEstateRegist);
            RealEstateRegistCommand = new CommonCommand(RealEstateRegistTest);


            //날짜 선택 기본은 오늘날짜
            today = DateTime.Now;
            selectedDate = today;
        }

        protected void OnRequestOK()
        {
            EventHandler handler = this.RequestOK;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        Dictionary<int, System.Drawing.Bitmap> imageDictionary = new Dictionary<int, System.Drawing.Bitmap>();
        Dictionary<int, string> imageNameDictionary = new Dictionary<int, string>();
        //List<string> imageNameList = new List<string>();
        //List<System.Drawing.Bitmap> imageList = new List<System.Drawing.Bitmap>();

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
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageSource = bitmap;

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
            }
        }

        

        private void NextImageCMD(object obj)
        {
            nextImageNum = Convert.ToInt16(nowImage) + 1;
            int key = 0;

            if (imageNameDictionary.ContainsKey(nextImageNum) == false)
            {
                foreach (var pair in imageNameDictionary)
                {
                    if(pair.Key != Convert.ToInt16(nowImage))
                    {
                        key = pair.Key;
                        break;
                    }                    
                }

                
                //if (nextImageNum != 1)
                //{
                //    nextImageNum = 1;
                //}                
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
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFileName);
            bitmap.EndInit();
            ImageSource = bitmap;

            nowImage = key.ToString(); 
            OnPropertyChanged("NowImage");
        }

        private void DelImageCMD(object obj)
        {
            int delNum = Convert.ToInt16(nowImage);
            int imagQ = imageNameDictionary.Count;
            int key = 0;

            if (imageNameDictionary.ContainsKey(delNum) == true)
            {
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

                //Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                string selectedFileName = imageNameDictionary[key];
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageSource = bitmap;

                nowImage = key.ToString();
                OnPropertyChanged("NowImage");
            }   
        }

        RealEstateFactory realEstateFactory;

        private void RealEstateRegistTest(object obj)
        {
            int number = realEstateFactory.GetNum() + 1;
            this.realEstateInfo.번호 = number.ToString();
            //this.realEstateInfo.접수일 = selectedDate.ToString("yyy-MM-dd");
            this.realEstateInfo.접수일 = selectedDate.ToString();

            if (spacious != null)
            {
                this.realEstateInfo.평수 = spacious;
            }
            else
            {
                this.realEstateInfo.평수 = "-";
            }

            if (floorNumber != null)
            {        
                this.realEstateInfo.층수 = floorNumber;
            }
            else
            {
                this.realEstateInfo.층수 = "-";
            }

            if (estateType != null)
            {                
                this.realEstateInfo.매물구분 = estateType;
            }
            else
            {
                this.realEstateInfo.매물구분 = "-";
            }

            if (deposit != null)
            {
                this.realEstateInfo.임대료 = deposit;
            }
            else
            {
                this.realEstateInfo.임대료 = "-";
            }

            if (elevator != null)
            {
                this.realEstateInfo.승강기 = elevator;
            }
            else
            {
                this.realEstateInfo.승강기 = "-";
            }

            if (hoist != null)
            {                
                this.realEstateInfo.호이스트 = hoist;
            }
            else
            {
                this.realEstateInfo.호이스트 = "-";
            }

            if (floorHeight != null)
            {               
                this.realEstateInfo.층고 = floorHeight;
            }
            else
            {
                this.realEstateInfo.층고 = "-";
            }

            if (power != null)
            {                
                this.realEstateInfo.전력 = power;
            }
            else
            {
                this.realEstateInfo.전력 = "-";
            }

            if (address != null)
            {               
                this.realEstateInfo.주소 = address;
            }
            else
            {
                this.realEstateInfo.주소 = "-";
            }

            if (maintenance != null)
            {
                this.realEstateInfo.담당자연락처 = maintenance;
            }
            else
            {
                this.realEstateInfo.담당자연락처 = "-";
            }

            if (comment != null)
            {               
                this.realEstateInfo.비고 = comment;
            }
            else
            {
                this.realEstateInfo.비고 = "-";
            }


            //realEstateFactory.ClearRealEstate();
            //realEstateFactory.ReadCSV();
            realEstateFactory.AddRealEstate(realEstateInfo);
            //데이터 셋에 저장


            realEstateFactory.WriteExcelTest();
            //realEstateFactory.WriteCSV();


            string imagePath = @"./image/" + number;

            if (Directory.Exists(imagePath) == false)
            {
                Directory.CreateDirectory(imagePath);
            }
            int i = 0;

            foreach (var pair in imageDictionary)
            {

                //imageDictionary[pair.Key].Save(imagePath + @"/image_" + i + ".jpg");

                pair.Value.Save(imagePath + @"/image_" + i + ".jpg");

                i++;
            }

            OnRequestOK();
        }
        private void RealEstateRegist(object obj)
        {
            int number = realEstateFactory.GetNum() + 1;
            this.realEstateInfo.번호 = number.ToString();
            this.realEstateInfo.접수일 = selectedDate.ToString("yyy-MM-dd");
            //this.realEstateInfo.계약체결일 = "";
            //this.realEstateInfo.계약종료일 = "";

            if(spacious != null)
            {
                if (spacious.Contains(","))
                {
                    this.realEstateInfo.평수 = spacious.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.평수 = spacious;
                }
            }

            if (floorNumber != null)
            {
                if (floorNumber.Contains(","))
                {
                    this.realEstateInfo.층수 = floorNumber.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.층수 = floorNumber;
                }
            }

            if (estateType != null)
            {
                if (estateType.Contains(","))
                {
                    this.realEstateInfo.매물구분 = estateType.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.매물구분 = estateType;
                }
            }

            if (deposit != null)
            {
                if (deposit.Contains(","))
                {
                    this.realEstateInfo.임대료 = deposit.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.임대료 = deposit;
                }
            }

            if (elevator != null)
            {
                if (elevator.Contains(","))
                {
                    this.realEstateInfo.승강기 = elevator.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.승강기 = elevator;
                }
            }

            if (hoist != null)
            {
                if (hoist.Contains(","))
                {
                    this.realEstateInfo.호이스트 = hoist.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.호이스트 = hoist;
                }
            }

            if (floorHeight != null)
            {
                if (floorHeight.Contains(","))
                {
                    this.realEstateInfo.층고 = floorHeight.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.층고 = floorHeight;
                }
            }

            if (power != null)
            {
                if (power.Contains(","))
                {
                    this.realEstateInfo.전력 = power.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.전력 = power;
                }
            }


            if (address != null)
            {
                if (address.Contains(","))
                {
                    this.realEstateInfo.주소 = address.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.주소 = address;
                }
            }

            if (maintenance != null)
            {
                if (maintenance.Contains(","))
                {
                    this.realEstateInfo.담당자연락처 = maintenance.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.담당자연락처 = maintenance;
                }
            }

            if (comment != null)
            {
                if (comment.Contains(","))
                {
                    this.realEstateInfo.비고 = comment.Replace(",", "&");
                }
                else
                {
                    this.realEstateInfo.비고 = comment;
                }
            }


            realEstateFactory.ClearRealEstate();
            realEstateFactory.ReadCSV();
            realEstateFactory.AddRealEstate(realEstateInfo);
            
            realEstateFactory.WriteCSV();
            

            string imagePath = @"./image/" + number;

            if (Directory.Exists(imagePath) == false)
            {
                Directory.CreateDirectory(imagePath);
            }
            int i = 0;

            foreach (var pair in imageDictionary)
            {
                
                //imageDictionary[pair.Key].Save(imagePath + @"/image_" + i + ".jpg");

                pair.Value.Save(imagePath + @"/image_" + i + ".jpg");

                i++;                
            }          

            OnRequestOK();
        }
    }
}
