using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSRealEstate.DTO;

namespace YSRealEstate
{
    public class RealEstateFactory
    {
        public IEnumerable<RealEstateInfoDTO> GetAllProducts()
        {
            //return products;
            return realEstateList;
        }

        public IEnumerable<RealEstateInfoDTO> FindDate(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.접수일.Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindContractDate(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.계약체결일.Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindContractEndDate(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.계약종료일.Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindSpacious(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.평수.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindFloorNumber(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.층수.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindEstateType(string searchString)
        {      
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.매물구분.Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindDeposit(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.임대료.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindElevator(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.승강기.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindHoist(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.호이스트.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindFloorHeight(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.층고.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindPower(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.전력.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindAddress(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.주소.Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindMaintenance(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.담담자연락처.Contains(searchString));
        }

        public IEnumerable<RealEstateInfoDTO> FindComment(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.비고.Contains(searchString));
        }

        #region In-memory data
        // This code builds an in-memory product collection
        // but we could as well fectch it from a database
        // or web service and it would yield the same result.
        //static IList<Product> products;
        IList<RealEstateInfoDTO> realEstateList;
        
        public RealEstateFactory()
        {
            //products = new List<Product>();
            //for (int i = 0; i < 100; i++)
            //{
            //    products.Add(generateRandomProduct());
            //}

            realEstateList = new List<RealEstateInfoDTO>();

            ReadCSV();
        }
#if false
        static Random r = new Random(DateTime.Now.Millisecond);
        static Product generateRandomProduct()
        {
            var titles = new string[] { "Classic city bike", "Vintage city bike", "Basic mountain bike", "Easy mountain bike", "Devil mountain bike" };
            var colors = new string[] { "Red", "Blue", "Green", "Brown", "Gray", "Black" };
            return new Product()
            {
                Title = pickRandom(titles),
                Color = pickRandom(colors),
                Price = Math.Round(300M + (decimal)r.NextDouble() * 1700M, 2),
                Reference = "BK"+r.Next(100000).ToString("d8")
            };
        }
        static T pickRandom<T>(T[] array)
        {
            return array[r.Next(array.Length)];
        }
#endif

        public void DelRealEstate(int index)
        {
            realEstateList.RemoveAt(index);
        }

        public void AddRealEstate(RealEstateInfoDTO realEstate)
        {
            realEstateList.Add(realEstate); 
        }

        public int GetNum()
        {
            return realEstateList.Count;
        }

        public void WriteCSV()
        {
            using (var w = new StreamWriter(@"RealEstateInfo.CSV"))
            {
                for (int i = 0 ; i < realEstateList.Count ; i++)
                {
                    if(i == 0)
                    {
                        var firstline = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", 
                            "#물건번호", "접수일", "계약체결일", "계약종료일", "평수", "층수", "매물구분", "임대료", "승강기", "호이스트", "층고", "전력", "주소", "담당자", "비고");
                        w.WriteLine(firstline);
                        w.Flush();
                    }

                    string replace = string.Empty;

                    var num = i + 1;
                    var receiptDate = realEstateList[i].접수일;
                    var contractDate = realEstateList[i].계약체결일;
                    var contractEndDate = realEstateList[i].계약종료일;
                    var spacious = realEstateList[i].평수;
                    var floorNumber = realEstateList[i].층수;
                    var estateType = realEstateList[i].매물구분;
                    var deposit = realEstateList[i].임대료;
                    var elevator = realEstateList[i].승강기;
                    var hoist = realEstateList[i].호이스트;
                    var floorHeight = realEstateList[i].층고;
                    var power = realEstateList[i].전력;

                    var address = string.Empty;

                    if (realEstateList[i].주소 != null)
                    {
                        replace = realEstateList[i].주소.Replace("\r\n", ":");
                        address = replace;
                    }

                    var maintenance = string.Empty;
                    if (realEstateList[i].담담자연락처 != null)
                    {
                        replace = realEstateList[i].담담자연락처.Replace("\r\n", ":");
                        maintenance = replace;
                    }

                    var comment = string.Empty;

                    if (realEstateList[i].비고 != null)
                    {
                        replace = realEstateList[i].비고.Replace("\r\n", ":");
                        comment = replace;
                    }               

                    var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", num, receiptDate, contractDate, contractEndDate, spacious, floorNumber, estateType, deposit, elevator, hoist, floorHeight, power, address, maintenance, comment);
                    w.WriteLine(line);
                    w.Flush();
                }
            }
        }

        public void ReadCSV()
        {
            string strFile = "RealEstateInfo.CSV";

            FileInfo fileInfo = new FileInfo(@"./"+strFile);
            if(fileInfo.Exists == false)
            {
                return;
            }

            realEstateList.Clear();

            using (FileStream fs = new FileStream(strFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
                {
                    string strLineValue = null;
                    string[] values = null;

                    while ((strLineValue = sr.ReadLine()) != null)
                    {
                        // Must not be empty.
                        if (string.IsNullOrEmpty(strLineValue)) return;

                        RealEstateInfoDTO realEstate = new RealEstateInfoDTO();

                        if (strLineValue.Substring(0, 1).Equals("#"))
                        {                          

                            continue;
                        }
                        
                        values = strLineValue.Split(',');

                        //DateTime dt = new DateTime(Convert.ToInt16(date[0]), Convert.ToInt16(date[1]), Convert.ToInt16(date[2]));

                        //string receiptDate = values[0].ToString("yyy-MM-dd");

                        int i = 0;
                        foreach(string value in values)
                        {
                            if (value.Contains("&"))
                            {
                                values[i] = value.Replace("&", ",");
                            }
                            i++;
                        }

                        realEstate.번호 = values[0];
                        realEstate.접수일 = values[1]; //접수일
                        realEstate.계약체결일 = values[2]; //접수일
                        realEstate.계약종료일 = values[3]; //접수일
                        realEstate.평수 = values[4];//평수
                        realEstate.층수 = values[5];//층수                        
                        realEstate.매물구분 = values[6];//매매구분  

                        realEstate.임대료 = values[7];//임대료
                        realEstate.승강기 = values[8];//승강기
                        realEstate.호이스트 = values[9]; //호이스트
                        realEstate.층고 = values[10];//층고
                        realEstate.전력 = values[11];//전력
                        realEstate.주소 = values[12].Replace(":", "\r\n"); //주소
                        realEstate.담담자연락처 = values[13].Replace(":", "\r\n"); //담당자
                        realEstate.비고 = values[14].Replace(":", "\r\n");//비고

                        realEstateList.Add(realEstate);
                    }
                }
            }
        }


#endregion
    }
    
    
    //CSV 파싱

    public class RealEstate : Notifier
    {
        //접수일
        private string number;

        public string 번호
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        //접수일
        private string receiptDate;

        public string 접수일
        {
            get { return receiptDate; }
            set
            {
                receiptDate = value;
                OnPropertyChanged("ReceiptDate");
            }
        }

        //사이즈
        private int spacious;

        public int 평수
        {
            get { return spacious; }
            set
            {
                spacious = value;
                OnPropertyChanged("Spacious");
            }
        }

        //층
        private int floorNumber;

        public int 층수
        {
            get { return floorNumber; }
            set
            {
                floorNumber = value;
                OnPropertyChanged("FloorNumber");
            }
        }

        //구분
        private string estateType;

        public string 매매구분
        {
            get { return estateType; }
            set
            {
                estateType = value;
                OnPropertyChanged("EstateType");
            }
        }

        //임대료
        private int deposit;

        public int 임대료
        {
            get { return deposit; }
            set
            {
                deposit = value;
                OnPropertyChanged("Deposit");
            }
        }

        //승강기
        private int elevator;

        public int 승강기
        {
            get { return elevator; }
            set
            {
                elevator = value;
                OnPropertyChanged("Elevator");
            }
        }

        //호이스트
        private int hoist; 

        public int 호이스트
        {
            get { return hoist; }
            set
            {
                hoist = value;
                OnPropertyChanged("Hoist");
            }
        }

        //층고
        private int floorHeight;

        public int 층고
        {
            get { return floorHeight; }
            set
            {
                floorHeight = value;
                OnPropertyChanged("FloorHeight");
            }
        }

        //전력
        private int power;

        public int 전력
        {
            get { return power; }
            set
            {
                power = value;
                OnPropertyChanged("Power");
            }
        }

        //주소
        private string address;

        public string 주소
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        //담당자 연락처
        private string guestTel;

        public string 담담자연락처
        {
            get { return guestTel; }
            set
            {
                guestTel = value;
                OnPropertyChanged("GuestTel");
            }
        }

        //코멘트
        private string comment;

        public string 비고
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }
    }

    public class Product : Notifier
    {
        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

        private string reference;

        public string Reference
        {
            get { return reference; }
            set
            {
                reference = value;
                OnPropertyChanged("Reference");
            }
        }

    }
}
