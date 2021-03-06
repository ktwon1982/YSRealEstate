﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YSRealEstate.Command;
using YSRealEstate.DTO;
using System.Data;
using System.Data.OleDb;

namespace YSRealEstate.Model
{
    public class MainModel : Notifier
    {
        #region Input and output properties

        private EventCommand doubleClickCommand;
        public ICommand DoubleClickCommand
        {
            get
            {
                if (doubleClickCommand == null)
                {
                    doubleClickCommand = new EventCommand(GrideviewDoubleClick, obj => true);
                }
                return doubleClickCommand;
            }
        }

        public CommonCommand RegistCommand { get; private set; }
        public CommonCommand DelCommand { get; private set; }

        public ObservableCollection<string> ComboItems { get; private set; }

        // 콤보박스 아이템 변경시
        private String selectedItem;
        public String SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }


        private string searchInput;

        public string SearchInput
        {
            get { return searchInput; }
            set
            {
                searchInput = value;
                base.OnPropertyChanged("SearchInput");
                OnSearchInputChanged();
            }
        }

        private IEnumerable<RealEstateInfoDTO> foundRealEstate;

        public IEnumerable<RealEstateInfoDTO> FoundRealEstate
        {
            get { return foundRealEstate; }
            set
            {
                foundRealEstate = value;
                OnPropertyChanged("FoundRealEstate");
            }
        }


        private RealEstateInfoDTO selectedRealEstate;

        public RealEstateInfoDTO SelectedRealEstate
        {
            get { return selectedRealEstate; }
            set
            {
                selectedRealEstate = value;
                OnPropertyChanged("SelectedRealEstate");
            }
        }

        #endregion

        RealEstateFactory factory = new RealEstateFactory();
        //RealEstate realEstate = new RealEstate();
        RealEstateInfoDTO realEstateInfoDTO;
        
        public MainModel()
        {
            realEstateInfoDTO = new RealEstateInfoDTO();
            // Optional: we're just making sure the
            // list is empty.
            FoundRealEstate = Enumerable.Empty<RealEstateInfoDTO>();

            RegistCommand = new CommonCommand(RegistCMD);
            DelCommand = new CommonCommand(DelCMD);
            
            //컴포넌트 초기화
            InitComponents();
        }

        private void InitComponents()
        {
            ComboItems = new ObservableCollection<string>()
            {
                "접수일",
                "계약체결일",
                "계약종료일",
                "평수",
                "층수",
                "매물구분",
                "임대료",
                "승강기",
                "호이스트",
                "층고",
                "전력",
                "주소",
                "담당자",
                "비고"
             };

            factory.ReadExcel();

            selectedItem = "비고";
            OnPropertyChanged("SelectedItem");
            
            searchInput = "";
            base.OnPropertyChanged("SearchInput");
            OnSearchInputChanged();
        } 


        private void RegistCMD(object obj)
        {
            ViewRegist VR = new ViewRegist(realEstateInfoDTO, factory);
            VR.ShowDialog();
            if (VR.DialogResult == true)
            {
                //CSV 
                //factory.ReadExcel();
                
                OnLoadData();

                searchInput = "";
                base.OnPropertyChanged("SearchInput");
                OnSearchInputChanged();
            }
        }

        private void DelCMD(object obj)
        {
            if(selectedRealEstate == null)
            {
                MessageBox.Show("행을 선택해 주세요");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("선택하신 정보가 삭제됩니다", "YesOrNo", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.No) { return; }

            int i = 0;
            
            string delNum = selectedRealEstate.번호;

            //foundRealEstate.ElementAt(4);

            foreach (var list in foundRealEstate)
            {
                if (list.임대료.Contains(","))
                {
                    list.임대료 = list.임대료.Replace(",", "&");
                }

                string selectList = list.번호;

                if(delNum.Equals(selectList))
                {
                    factory.DelRealEstate(i);
                    break;
                }
                i++;
            }

            //CSV 
            //factory.WriteCSV();
            factory.WriteExcelTest();

            //factory.InitLoadReadCSV();

            OnLoadData();

            searchInput = "";
            base.OnPropertyChanged("SearchInput");
            OnSearchInputChanged();

            //폴더 삭제
            DirectoryInfo di = new DirectoryInfo("./image/"+ delNum);
            if(di.Exists == true)
            {
                di.Delete(true);
            }            

        }

        private void OnLoadData()
        {
            SelectedRealEstate = null;
            foundRealEstate = factory.GetAllProducts();
            
            OnPropertyChanged("FoundRealEstate");
            
        }

        private void OnSearchInputChanged()
        {
            // Optional: just make sure any selected
            // product is unselected
            SelectedRealEstate = null;

            if (selectedItem.Equals("접수일"))
            {
                foundRealEstate = factory.FindDate(SearchInput);
            }
            if (selectedItem.Equals("계약체결일"))
            {
                foundRealEstate = factory.FindContractDate(SearchInput);
            }
            if (selectedItem.Equals("계약종료일"))
            {
                foundRealEstate = factory.FindContractEndDate(SearchInput);
            }
            else if (selectedItem.Equals("평수"))
            {
                foundRealEstate = factory.FindSpacious(SearchInput);
            }
            else if (selectedItem.Equals("층수"))
            {
                foundRealEstate = factory.FindFloorNumber(SearchInput);
            }
            else if (selectedItem.Equals("매물구분"))
            {
                foundRealEstate = factory.FindEstateType(SearchInput);
            }
            else if (selectedItem.Equals("임대료"))
            {
                foundRealEstate = factory.FindDeposit(SearchInput);
            }
            else if (selectedItem.Equals("승강기"))
            {
                foundRealEstate = factory.FindElevator(SearchInput);

            }
            else if (selectedItem.Equals("호이스트"))
            {
                foundRealEstate = factory.FindHoist(SearchInput);

            }
            else if (selectedItem.Equals("층고"))
            {
                foundRealEstate = factory.FindFloorHeight(SearchInput);

            }
            else if (selectedItem.Equals("전력"))
            {
                foundRealEstate = factory.FindPower(SearchInput);

            }
            else if (selectedItem.Equals("주소"))
            {
                foundRealEstate = factory.FindAddress(SearchInput);

            }
            else if (selectedItem.Equals("담당자"))
            {
                foundRealEstate = factory.FindMaintenance(SearchInput);

            }
            else
            {
                foundRealEstate = factory.FindComment(SearchInput);
            }

            OnPropertyChanged("FoundRealEstate");
        }

        private void GrideviewDoubleClick(object obj)
        {
            if(SelectedRealEstate == null)
            {
                MessageBox.Show("행을 선택 해 주세요.");
                return;
            }

            ViewListDetail VLD = new ViewListDetail(selectedRealEstate, factory);
            VLD.ShowDialog();

            if (VLD.DialogResult == true)
            {
                //CSV 
                //factory.ReadCSV();
                //factory.ReadExcel();

                OnLoadData();

                searchInput = "";
                base.OnPropertyChanged("SearchInput");
                OnSearchInputChanged();
            }
        }

        private void RegistClick(object obj)
        {
            int a = 0;
        }
    }
}
