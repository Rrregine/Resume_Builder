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
    /// Interaction logic for UpdateResumeWindow.xaml
    /// </summary>
    public partial class UpdateResumeWindow : Window
    {
        private DateTime createdOn;
        private DateTime lastModified;

        Resume resume;

        public UpdateResumeWindow(Resume resume)
        {
            InitializeComponent();
            this.resume = resume;
            InitializeDates();
            firstNameTextBox.Text = resume.FirstName;
            lastNameTextBox.Text = resume.LastName;
            genderTextBox.Text = resume.Gender;
            ageTextBox.Text = resume.Age.ToString();
            contactTextBox.Text = resume.ContactInfo;
            experienceTextBox.Text = resume.Experience;
            educationTextBox.Text = resume.Education;
            hobbiesTextBox.Text = resume.Hobbies;
            referencesTextBox.Text = resume.Ref;
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

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            
            
            resume.Gender = genderTextBox.Text;
            
            resume.Experience = experienceTextBox.Text;
            resume.Education = educationTextBox.Text;
            resume.Hobbies = hobbiesTextBox.Text;
            resume.Ref = referencesTextBox.Text;

            if (firstNameTextBox.Text == "")
            {
                string messageBoxText = "First name cannot be empty!";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            else
            {
                resume.FirstName = firstNameTextBox.Text;
            }

            if (lastNameTextBox.Text == "")
            {
                string messageBoxText = "Last name cannot be empty!";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            else
            {
                resume.LastName = lastNameTextBox.Text;
            }

            if (ageTextBox.Text == "")
            {
                string messageBoxText = "Age cannot be empty!";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            else
            {
                resume.Age = Convert.ToInt32(ageTextBox.Text);
            }

            if (contactTextBox.Text == "")
            {
                string messageBoxText = "Contact Information cannot be empty!";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            else
            {
                resume.ContactInfo = contactTextBox.Text;
            }

            ResumeDBHandler db = ResumeDBHandler.Instance;
            db.UpdateResume(resume);
            Close();

        }
    }
}
