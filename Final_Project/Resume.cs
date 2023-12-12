using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Resume
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String Gender { get; set; }
        public int Age { get; set; }
        public string ContactInfo { get; set; } 
        public string Experience { get; set; } 
        public string Education { get; set; } 
        public string Hobbies { get; set; } 
        public string References { get; set; }

        public override string ToString()
        {
            string formatted = String.Format("{0}\t {1}\t {2} \t, {3}\t, {4} year-old\nContact Information: {5}\nWork Experience: {6}\nEducation: {7}\nHobbies: {8}\nReferences: {9}",
                                              Id, FirstName, LastName, Gender, Age, string.Join(", ", ContactInfo), string.Join(", ", Experience), string.Join(", ", Education), string.Join(", ", Hobbies), string.Join(", ", References));
            return formatted;
        }
    }
}
