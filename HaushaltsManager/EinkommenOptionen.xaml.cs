using HaushaltsManager.Repository;
using System.Windows;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für EinkommenOptionen.xaml
    /// </summary>
    public partial class EinkommenOptionen : Window
    {
        private readonly BasicRepository rep;

        public EinkommenOptionen()
        {
            InitializeComponent();
        }

        public EinkommenOptionen(BasicRepository rep)
        {
            this.rep = rep;
        }
    }
}
