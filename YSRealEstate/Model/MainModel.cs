using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YSRealEstate.Command;

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

        private IEnumerable<RealEstate> foundRealEstate;

        public IEnumerable<RealEstate> FoundRealEstate
        {
            get { return foundRealEstate; }
            set
            {
                foundRealEstate = value;
                OnPropertyChanged("FoundRealEstate");
            }
        }


        private RealEstate selectedRealEstate;

        public RealEstate SelectedRealEstate
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
        RealEstate realEstate = new RealEstate();

        public MainModel()
        {
            // Optional: we're just making sure the
            // list is empty.
            FoundRealEstate = Enumerable.Empty<RealEstate>();

            OnLoadData();
        }

        private void OnLoadData()
        {
            SelectedRealEstate = null;
            FoundRealEstate = factory.GetAllProducts();

            ComboItems = new ObservableCollection<string>()
            {
                "접수일",
                "평수",
                "층수",
                "매물구분",
                "보증금",
                "승강기",
                "호이스트",
                "층고",
                "전력",
                "주소",
                "담당자",
                "비고"
            };

            selectedItem = "비고";
            OnPropertyChanged("SelectedItem");
        }

        private void OnSearchInputChanged()
        {
            // Optional: just make sure any selected
            // product is unselected
            SelectedRealEstate = null;

            if (selectedItem.Equals("접수일"))
            {
                FoundRealEstate = factory.FindDate(SearchInput);
            }
            else if (selectedItem.Equals("평수"))
            {
                FoundRealEstate = factory.FindSpacious(SearchInput);
            }
            else if (selectedItem.Equals("층수"))
            {
                FoundRealEstate = factory.FindFloorNumber(SearchInput);
            }
            else if (selectedItem.Equals("매물구분"))
            {
                FoundRealEstate = factory.FindEstateType(SearchInput);
            }
            else if (selectedItem.Equals("보증금"))
            {
                FoundRealEstate = factory.FindDeposit(SearchInput);
            }
            else if (selectedItem.Equals("승강기"))
            {
                FoundRealEstate = factory.FindElevator(SearchInput);

            }
            else if (selectedItem.Equals("호이스트"))
            {
                FoundRealEstate = factory.FindHoist(SearchInput);

            }
            else if (selectedItem.Equals("층고"))
            {
                FoundRealEstate = factory.FindFloorHeight(SearchInput);

            }
            else if (selectedItem.Equals("전력"))
            {
                FoundRealEstate = factory.FindPower(SearchInput);

            }
            else if (selectedItem.Equals("주소"))
            {
                FoundRealEstate = factory.FindAddress(SearchInput);

            }
            else if (selectedItem.Equals("담당자"))
            {
                FoundRealEstate = factory.FindMaintenance(SearchInput);

            }
            else
            {
                FoundRealEstate = factory.FindComment(SearchInput);
            }
        }

        private void GrideviewDoubleClick(object obj)
        {
            ViewListDetail VLD = new ViewListDetail(selectedRealEstate);
            VLD.ShowDialog();
        }
    }
}
