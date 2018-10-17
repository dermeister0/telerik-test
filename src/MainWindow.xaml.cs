using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;
using Telerik.Windows.Data;

namespace Test100900
{
    internal class TestContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Data Source=Test.db");
        }
    }

    internal class City
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QueryableCollectionView view;

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var context = new TestContext();

            if (File.Exists("Test.db"))
            {
                File.Delete("Test.db");
            }

            File.Copy(@"..\..\Test.db", "Test.db");
            view = new QueryableCollectionView(context.Cities);
            view.FilterDescriptors.Add(new FilterDescriptor("Id", FilterOperator.IsEqualTo, 123));
            GV.ItemsSource = view;
            File.Delete("Test.db");
        }
    }
}