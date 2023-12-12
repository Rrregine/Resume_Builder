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
        Resume resume;

        public UpdateResumeWindow(Resume resume)
        {
            InitializeComponent();
            this.resume = resume;

            resume.FirstName = firstNameTextBox.Text;
            resume.LastName = lastNameTextBox.Text;
            resume.Gender = genderTextBox.Text;
            resume.Age = Convert.ToInt32(ageTextBox.Text);
            resume.ContactInfo = contactTextBox.Text;
            resume.Experience = experienceTextBox.Text;
            resume.Education = educationTextBox.Text;
            resume.Hobbies = hobbiesTextBox.Text;
            resume.References = referencesTextBox.Text;
        }
        
             private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            resume.FirstName = firstNameTextBox.Text;
            resume.LastName = lastNameTextBox.Text;
            resume.Gender = genderTextBox.Text;
            resume.Age = Convert.ToInt32(ageTextBox.Text);
            resume.ContactInfo = contactTextBox.Text;
            resume.Experience = experienceTextBox.Text;
            resume.Education = educationTextBox.Text;
            resume.Hobbies = hobbiesTextBox.Text;
            resume.References = referencesTextBox.Text;

            ResumeDBHandler db = ResumeDBHandler.Instance;
            db.UpdateResume(resume);
            Close();

        }
    }
}
