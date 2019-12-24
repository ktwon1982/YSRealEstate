using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using YSRealEstate.Command;

namespace YSRealEstate.Model
{
    public class ViewListDetailModel : Notifier
    {
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
            int a = 0;
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

        private string receiptDate;

        public string ReceiptDate
        {
            get { return receiptDate; }
            set
            {
                receiptDate = value;
                OnPropertyChanged("ReceiptDate");
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
        private List<String> imageNameLists;

        //static int count = 1;
        void DirFileSearch(string path, string file)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path); string[] files = Directory.GetFiles(path, $"*.{file}"); foreach (string f in files)
                {
                    string[] values = null;
                    values = f.Split('\\');

                    imageNameLists.Add(values[1]);
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

        


        public ViewListDetailModel(RealEstate details)
        {
            imageNameLists = new List<string>();

            estateNumber = "매물 " + details.번호 + " 상세";
            receiptDate = details.접수일;
            spacious = details.평수.ToString();
            floorNumber = details.층수.ToString();
            estateType = details.매매구분;
            deposit = details.보증금.ToString();
            elevator = details.승강기.ToString();
            hoist = details.호이스트.ToString();
            floorHeight = details.층고.ToString();
            power = details.전력.ToString();
            address = details.주소;
            maintenance = details.담담자연락처;
            comment = details.비고;

            int number = Convert.ToInt16(details.번호);
            string imagePath = @"./image/" + number;

            if (Directory.Exists(imagePath) == false)
            {
                Directory.CreateDirectory(imagePath);
            }
            else
            {
                DirFileSearch(imagePath,"jpg");
            }

            string filePath = imagePath + @"//" + imageNameLists[0];

            FileInfo file = new FileInfo(filePath);
            if (file.Exists == true)
            {
                Bitmap Image = new Bitmap(filePath);
                imageSource = ImageMgr.ToWpfImage(Image);
                this.OnPropertyChanged("ImageSource");
            }
        }
    }
}
