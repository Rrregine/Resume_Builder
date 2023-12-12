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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResumeDBHandler db = ResumeDBHandler.Instance;
        List<Resume> resumes;

        public MainWindow()
        {
            InitializeComponent();

            RefreshAllResumesList();
        }

        private void RefreshAllResumesList()
        {
            AllResumesDataGrid.ItemsSource = null;
            resumes = db.ReadAllResumes();
            AllResumesDataGrid.ItemsSource = resumes;
        }

        private void AddResumeButton_Click(object sender, RoutedEventArgs e)
        {
            AddResumeWindow addResumeWindow = new AddResumeWindow();
            addResumeWindow.ShowDialog();
            RefreshAllResumesList();

        }

        private void ExportToPDFButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AllResumesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Resume resume = (Resume)AllResumesDataGrid.SelectedItem;

            if (resume != null)
            {
                ResumeDetailsWindow resumeDetailsWindow = new ResumeDetailsWindow(resume);
                resumeDetailsWindow.ShowDialog();
                RefreshAllResumesList();
            }
        }
    }
}
