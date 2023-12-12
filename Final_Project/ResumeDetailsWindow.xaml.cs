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

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for ResumeDetailsWindow.xaml
    /// </summary>
    public partial class ResumeDetailsWindow : Window
    {
        Resume resume;
        public ResumeDetailsWindow(Resume resume)
        {
            InitializeComponent();
            this.resume = resume;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateResumeWindow updateResumeWindow = new UpdateResumeWindow(resume);
            updateResumeWindow.ShowDialog();
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ResumeDBHandler db = ResumeDBHandler.Instance;
            db.DeleteResume(resume);
            Close();
        }
    }
}
