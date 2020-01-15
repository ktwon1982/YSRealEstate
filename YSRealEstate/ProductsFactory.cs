using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSRealEstate.DTO;
using Microsoft.Office.Interop;

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
            return realEstateList.Where(p => p.담당자연락처.Contains(searchString));
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

            //ReadCSV();
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

        public void ClearRealEstate()
        {
            realEstateList.Clear();
        }

        public int CountRealEstate()
        {
            return realEstateList.Count;
        }


        public int GetNum()
        {
            return realEstateList.Count;
        }

        #region 메모리해제
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception e)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion
        
        public void WriteExcelTest()
        {
#if false
            DataTable dt = new DataTable();
            
            dt.Columns.Add(new DataColumn("번호", typeof(string)));
            dt.Columns.Add(new DataColumn("접수일", typeof(string)));
            dt.Columns.Add(new DataColumn("평수", typeof(string)));
            dt.Columns.Add(new DataColumn("층수", typeof(string)));
            dt.Columns.Add(new DataColumn("매물구분", typeof(string)));
            dt.Columns.Add(new DataColumn("임대료", typeof(string)));
            dt.Columns.Add(new DataColumn("승강기", typeof(string)));
            dt.Columns.Add(new DataColumn("호이스트", typeof(string)));
            dt.Columns.Add(new DataColumn("층고", typeof(string)));
            dt.Columns.Add(new DataColumn("전력", typeof(string)));
            dt.Columns.Add(new DataColumn("주소", typeof(string)));
            dt.Columns.Add(new DataColumn("담당자", typeof(string)));
            dt.Columns.Add(new DataColumn("비고", typeof(string)));
            dt.Columns.Add(new DataColumn("계약체결일", typeof(string)));
            dt.Columns.Add(new DataColumn("계약종료일", typeof(string)));
#endif
            //ExportToExcel(dt);     
            ExportToExcel();
        }

        private void ExportToExcel()
            //private void ExportToExcel(DataTable dt)
        {

            DataTable dt = new DataTable();
            DataRow row = null;

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel._Worksheet workSheet = wb.Worksheets.get_Item(1) as Microsoft.Office.Interop.Excel._Worksheet;
            workSheet.Name = "물건표";

            if (realEstateList.Count == 0)
            {
                return;
            }

            int i = 2;

            // 헤더 출력
            workSheet.Cells[1, 1] = "번호";
            workSheet.Cells[1, 2] = "접수일";
            workSheet.Cells[1, 3] = "평수";
            workSheet.Cells[1, 4] = "층수";
            workSheet.Cells[1, 5] = "매물구분";
            workSheet.Cells[1, 6] = "임대료";
            workSheet.Cells[1, 7] = "승강기";
            workSheet.Cells[1, 8] = "호이스트";
            workSheet.Cells[1, 9] = "층고";
            workSheet.Cells[1, 10] = "전력";
            workSheet.Cells[1, 11] = "주소";
            workSheet.Cells[1, 12] = "담당자연락처";
            workSheet.Cells[1, 13] = "비고";
            workSheet.Cells[1, 14] = "계약체결일";
            workSheet.Cells[1, 15] = "계약종료일";


            foreach (RealEstateInfoDTO values in realEstateList)
            {
                row = dt.NewRow();
                // row[""] = values[0];
                workSheet.Cells[i, 1] = values.번호;
                workSheet.Cells[i, 2] = values.접수일;
                workSheet.Cells[i, 3] = values.평수;
                workSheet.Cells[i, 4] = values.층수;
                workSheet.Cells[i, 5] = values.매물구분;
                workSheet.Cells[i, 6] = values.임대료;
                workSheet.Cells[i, 7] = values.승강기;
                workSheet.Cells[i, 8] = values.호이스트;
                workSheet.Cells[i, 9] = values.층고;
                workSheet.Cells[i, 10] = values.전력;
                workSheet.Cells[i, 11] = values.주소;
                workSheet.Cells[i, 12] = values.비고;
                workSheet.Cells[i, 13] = values.담당자연락처;
                workSheet.Cells[i, 14] = values.계약체결일;
                workSheet.Cells[i, 15] = values.계약종료일;

                i++;
            }


            //내용 출력
            //for (int r = 0; r < dgv.Rows.Count; r++)
            //{
            //    for (int i = 0; i < dgv.Columns.Count - 1; i++)
            //    {
            //        workSheet.Cells[r + 2, i + 1] = dgv.Rows[r].Cells[i].Value;
            //    }
            //}

            workSheet.Columns.AutoFit(); // 글자 크기에 맞게 셀 크기를 자동으로 조절
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "TestSheet.xls";
            wb.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            wb.Close(Type.Missing, Type.Missing, Type.Missing);
            excelApp.Quit();
            releaseObject(excelApp);
            releaseObject(workSheet);
            releaseObject(wb);
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

                    if (spacious != null)
                    {
                        if (spacious.Contains(","))
                        {
                            realEstateList[i].평수 = spacious.Replace(",", "&");
                        }                      
                    }
                                        
                    var floorNumber = realEstateList[i].층수;
                    if (floorNumber != null)
                    {
                        if (floorNumber.Contains(","))
                        {
                            realEstateList[i].층수 = floorNumber.Replace(",", "&");
                        }
                    }

                    var estateType = realEstateList[i].매물구분;
                    if (estateType != null)
                    {
                        if (estateType.Contains(","))
                        {
                            realEstateList[i].매물구분 = estateType.Replace(",", "&");
                        }                        
                    }

                    var deposit = realEstateList[i].임대료;
                    if (deposit != null)
                    {
                        if (deposit.Contains(","))
                        {
                            realEstateList[i].임대료 = deposit.Replace(",", "&");
                        }
                    }

                    var elevator = realEstateList[i].승강기;
                    if (elevator != null)
                    {
                        if (elevator.Contains(","))
                        {
                            realEstateList[i].승강기 = elevator.Replace(",", "&");
                        }
                    }

                    var hoist = realEstateList[i].호이스트;
                    if (hoist != null)
                    {
                        if (hoist.Contains(","))
                        {
                            realEstateList[i].호이스트 = hoist.Replace(",", "&");
                        }
                    }

                    var floorHeight = realEstateList[i].층고;
                    if (floorHeight != null)
                    {
                        if (floorHeight.Contains(","))
                        {
                            realEstateList[i].층고 = floorHeight.Replace(",", "&");
                        }
                    }

                    var power = realEstateList[i].전력;
                    if (power != null)
                    {
                        if (power.Contains(","))
                        {
                            realEstateList[i].전력 = power.Replace(",", "&");
                        }
                    }

                    var address = realEstateList[i].주소;
                    if (address != null)
                    {
                        if (address.Contains(","))
                        {
                            realEstateList[i].주소 = address.Replace(",", "&");
                        }

                        replace = realEstateList[i].주소.Replace("\r\n", ":");
                        address = replace;
                    }                    
                    else
                    {
                        address = "-";
                    }

                    var maintenance = realEstateList[i].담당자연락처;

                    if (maintenance != null)
                    {
                        if (maintenance.Contains(","))
                        {
                            realEstateList[i].담당자연락처 = maintenance.Replace(",", "&");
                        }
                    }                   
                    else
                    {
                        maintenance = "-";
                    }

                    var comment = realEstateList[i].비고;

                    if (comment != null)
                    {
                        if (comment.Contains(","))
                        {
                            realEstateList[i].비고 = comment.Replace(",", "&");
                        }
                        replace = realEstateList[i].비고.Replace("\r\n", ":");
                        comment = replace;
                    }
                    else
                    {
                        comment = "-";
                    }


                    var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", num, receiptDate, contractDate, contractEndDate, spacious, floorNumber, estateType, deposit, elevator, hoist, floorHeight, power, address, maintenance, comment);
                    w.WriteLine(line);
                    w.Flush();
                }
            }
        }

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

        public void ReadExcel()
        {
            realEstateList.Clear();

            string strFile = "TestSheet.xls";
            string fileExtension = ".xls";
            string connectionString = string.Empty;
            string filePath = @".\" + strFile;
            string header = "Yes";
            string sheetName = string.Empty;

            // 확장자로 구분하여 커넥션 스트링을 가져옮
            switch (fileExtension)
            {
                case ".xls":    //Excel 97-03
                    connectionString = string.Format(Excel03ConString, filePath, header);
                    break;
                case ".xlsx":  //Excel 07
                    connectionString = string.Format(Excel07ConString, filePath, header);
                    break;
            }

            // 첫 번째 시트의 이름을 가져옮
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }
            Console.WriteLine("sheetName = " + sheetName);

            // 첫 번째 쉬트의 데이타를 읽어서 datagridview 에 보이게 함.
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();
                        //Populate DataGridView.
                        //dataGridView1.DataSource = dt;

                        

                        foreach (DataRow values in dt.Rows)
                        {
                            if(String.IsNullOrEmpty(values[0].ToString()))
                            {
                                continue;
                            }

                            RealEstateInfoDTO realEstate = new RealEstateInfoDTO();

                            realEstate.번호 = values[0].ToString(); //번호
                            realEstate.접수일 = values[1].ToString(); //접수일                           
                            realEstate.평수 = values[2].ToString();//평수
                            realEstate.층수 = values[3].ToString();//층수    
                            realEstate.매물구분 = values[4].ToString();//매매구분  
                            realEstate.임대료 = values[5].ToString();//임대료
                            realEstate.승강기 = values[6].ToString();//승강기
                            realEstate.호이스트 = values[7].ToString(); //호이스트
                            realEstate.층고 = values[8].ToString();//층고
                            realEstate.전력 = values[9].ToString();//전력
                            realEstate.주소 = values[10].ToString(); //주소
                            realEstate.담당자연락처 = values[11].ToString();//담당자                   
                            realEstate.비고 = values[12].ToString(); //비고                            
                            realEstate.계약체결일 = values[13].ToString(); //계약체결일
                            realEstate.계약종료일 = values[14].ToString(); //계약종료일 
                            
                            realEstateList.Add(realEstate);
                        }
                    }
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
                    string[] values = new string[15];
                        

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

                        foreach (string value in values)
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                if((i != 0)&& (i != 1) && (i != 2) && (i != 3))
                                {
                                    values[i] = "-";
                                }                                
                            }

                            //if (value.Contains("&"))
                            //{
                            //    values[i] = value.Replace("&", ",");
                            //}

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
                        realEstate.담당자연락처 = values[13].Replace(":", "\r\n"); //담당자
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

        public string 담당자연락처
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
