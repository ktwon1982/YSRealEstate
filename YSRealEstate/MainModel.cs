using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSRealEstate
{
    public class MainModel : Notifier
    {
        #region Input and output properties

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


        private Product selectedRealEstate;

        public Product SelectedRealEstate
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
        }

        private void OnSearchInputChanged()
        {
            // Optional: just make sure any selected
            // product is unselected
            SelectedRealEstate = null;

            FoundRealEstate = factory.FindProducts(SearchInput);
        }
    }
}
