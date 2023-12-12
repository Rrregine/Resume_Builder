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
        private DateTime createdOn;
        private DateTime lastModified;

        Resume resume;
        public ResumeDetailsWindow(Resume resume)
        {
            InitializeComponent();
            this.resume = resume;
            InitializeDates();
            firstNameTextBlock.Text = resume.FirstName ;
            lastNameTextBlock.Text = resume.LastName;
            genderTextBlock.Text = resume.Gender;
            ageTextBlock.Text = resume.Age.ToString();
            contactTextBlock.Text = resume.ContactInfo;
            experienceTextBlock.Text = resume.Experience;
            educationTextBlock.Text = resume.Education;
            hobbiesTextBlock.Text = resume.Hobbies;
            referencesTextBlock.Text = resume.Ref;
        }

        private void InitializeDates()
        {
            createdOn = DateTime.Now;
            lastModified = createdOn;
            UpdateDateTextBlocks();
        }

        private void UpdateDateTextBlocks()
        {
            createdOnTextBlock.Text = createdOn.ToString("G");
            lastModifiedTextBlock.Text = lastModified.ToString("G");
        }

        private void ModifyData_Click(object sender, RoutedEventArgs e)
        {
            // Simulate data modification
            lastModified = DateTime.Now;
            UpdateDateTextBlocks();
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
