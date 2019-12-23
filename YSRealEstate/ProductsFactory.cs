using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSRealEstate
{
    public class RealEstateFactory
    {
        public IEnumerable<RealEstate> GetAllProducts()
        {
            //return products;
            return realEstateList;
        }

        public IEnumerable<RealEstate> FindDate(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.접수일.Contains(searchString));
        }

        public IEnumerable<RealEstate> FindSpacious(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.평수.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstate> FindFloorNumber(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.층수.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstate> FindEstateType(string searchString)
        {      
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.매매구분.Contains(searchString));
        }

        public IEnumerable<RealEstate> FindDeposit(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.보증금.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstate> FindElevator(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.승강기.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstate> FindHoist(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.호이스트.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstate> FindFloorHeight(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.층고.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstate> FindPower(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.전력.ToString().Contains(searchString));
        }

        public IEnumerable<RealEstate> FindAddress(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.주소.Contains(searchString));
        }

        public IEnumerable<RealEstate> FindMaintenance(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.담담자연락처.Contains(searchString));
        }

        public IEnumerable<RealEstate> FindComment(string searchString)
        {
            //return products.Where(p => p.Title.Contains(searchString));
            return realEstateList.Where(p => p.비고.Contains(searchString));
        }

        #region In-memory data
        // This code builds an in-memory product collection
        // but we could as well fectch it from a database
        // or web service and it would yield the same result.
        //static IList<Product> products;
        static IList<RealEstate> realEstateList;
        
        static RealEstateFactory()
        {
            //products = new List<Product>();
            //for (int i = 0; i < 100; i++)
            //{
            //    products.Add(generateRandomProduct());
            //}

            realEstateList = new List<RealEstate>();

            ReadCSV();
        }

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
        
        
        static void ReadCSV()
        {
            string strFile = "RealEstateInfo.CSV";

            using (FileStream fs = new FileStream(strFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
                {
                    string strLineValue = null;
                    string[] keys = null;
                    string[] values = null;
                    string[] date = null;

                    while ((strLineValue = sr.ReadLine()) != null)
                    {
                        // Must not be empty.
                        if (string.IsNullOrEmpty(strLineValue)) return;

                        RealEstate realEstate = new RealEstate();

                        if (strLineValue.Substring(0, 1).Equals("#"))
                        {                          

                            continue;
                        }

                        values = strLineValue.Split(',');

                        
                        //date = values[0].Split('-');

                        //DateTime dt = new DateTime(Convert.ToInt16(date[0]), Convert.ToInt16(date[1]), Convert.ToInt16(date[2]));

                        //string receiptDate = values[0].ToString("yyy-MM-dd");

                        realEstate.접수일 = values[0]; //접수일
                        realEstate.평수 = Convert.ToInt16(values[1]);//평수
                        realEstate.층수 = Convert.ToInt16(values[2]);//층수
                        realEstate.매매구분 = values[3];//매매구분    
                        realEstate.보증금 = Convert.ToInt16(values[4]);//보증금
                        realEstate.승강기 = Convert.ToInt16(values[5]);//승강기
                        realEstate.호이스트 = Convert.ToInt16(values[6]); //호이스트
                        realEstate.층고 = Convert.ToInt16(values[7]);//층고
                        realEstate.전력 = Convert.ToInt16(values[8]);//전력
                        realEstate.주소 = values[9];//주소
                        realEstate.담담자연락처 = values[10];//담당자
                        realEstate.비고 = values[11];//비고

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

        //보증금
        private int deposit;

        public int 보증금
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
