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
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ViewRegist : Window
    {
        public ViewRegist(RealEstateInfoDTO realEstateInfoDTO, RealEstateFactory factory)
        {
            InitializeComponent();

            ViewRegistModel vm = new ViewRegistModel(realEstateInfoDTO, factory);
            this.DataContext = vm;

            //window close
            //vm.RequestClose += (s, e) => this.Close();

            //dialogResult return OK
            vm.RequestOK += (s, e) => this.DialogResult = true;
        }
    }
}
