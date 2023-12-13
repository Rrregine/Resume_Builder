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
    /// Interaction logic for AddResumeWindow.xaml
    /// </summary>
    public partial class AddResumeWindow : Window
    {
        private DateTime createdOn;
        private DateTime lastModified;

        public AddResumeWindow()
        {
            InitializeComponent();
            InitializeDates();
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

        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
            Resume newResume = new Resume();
            newResume.FirstName = firstNameTextBox.Text;
            newResume.LastName = lastNameTextBox.Text;
            newResume.Gender = genderTextBox.Text;

            newResume.ContactInfo = contactTextBox.Text;
            newResume.Experience = experienceTextBox.Text;
            newResume.Education = educationTextBox.Text;
            newResume.Hobbies = hobbiesTextBox.Text;
            newResume.Ref = referencesTextBox.Text;


            if (firstNameTextBox.Text == "")
            {
                string messageBoxText = "First name cannot be empty!";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
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
                newResume.Age = Convert.ToInt32(ageTextBox.Text);
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



            ResumeDBHandler db = ResumeDBHandler.Instance;
            db.AddResume(newResume);
            Close();
        }
    }
}
