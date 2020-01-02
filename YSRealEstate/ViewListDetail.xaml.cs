using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YSRealEstate.Model;
using YSRealEstate.DTO;

namespace YSRealEstate
{
    /// <summary>
    /// ViewListDetail.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ViewListDetail : Window
    {
        public ViewListDetail(RealEstateInfoDTO details, RealEstateFactory factory)
        {
            InitializeComponent();

            ViewListDetailModel vm = new ViewListDetailModel(details, factory);
            this.DataContext = vm;

            vm.RequestOK += (s, e) => this.DialogResult = true;
        }
    }
}
