using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public sealed class ResumeDBHandler
    {
        static readonly string ConString =
            ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        static readonly ResumeDBHandler instance = new ResumeDBHandler();


        private ResumeDBHandler()
        {
            CreateTable();

            //seed the table
            Resume newR1 = new Resume
            {
                FirstName = "Han",
                LastName = "Solo",
                Gender = "Male",
                Age = 40,
                ContactInfo = { "hansolo@gmail.com" }
            };

            Resume newR2 = new Resume
            {
                FirstName = "Eltaw",
                LastName = "Forst",
                Gender = "Male",
                Age = 18,
                ContactInfo = {"111-1111-1111"}
            };

            Resume newR3 = new Resume
            {
                FirstName = "Ellan",
                LastName = "Forst",
                Gender = "Male",
                Age = 24,
                ContactInfo = {"ellanforst@gmail.com","222-2222-2222"}
            };

            Person newP4 = new Person
            {
                FirstName = "Nordt",
                LastName = "Silversmith",
                City = "Flora",
                Age = 25
            };

            AddPerson(newP1);
            AddPerson(newP2);
            AddPerson(newP3);
            AddPerson(newP4);

        }

        public static PersonDBHandler Instance { get { return instance; } }

        public int AddPerson(Person person)
        {
            int newId = 0;
            int rows = 0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                //Create parameterized query
                string query = "INSERT INTO PERSONS (FirstName, LastName, City, Age) VALUES (@FirstName, @LastName, @City, @Age);";

                SQLiteCommand insertcom = new SQLiteCommand(query, con);

                //Pass values to query parameter
                insertcom.Parameters.AddWithValue("@FirstName", person.FirstName);
                insertcom.Parameters.AddWithValue("@LastName", person.LastName);
                insertcom.Parameters.AddWithValue("@City", person.City);
                insertcom.Parameters.AddWithValue("@Age", person.Age);

                try
                {
                    rows = insertcom.ExecuteNonQuery();

                    //lets get the rowid inserted
                    insertcom.CommandText = "select last_insert_rowid()";

                    Int64 LastRowID64 = Convert.ToInt64(insertcom.ExecuteScalar());

                    newId = Convert.ToInt32(LastRowID64);
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Detail: " + ex.ToString());
                }
                return newId;
            }
        }

        public void CreateTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                string drop = "drop table if exists PERSONS";
                SQLiteCommand command1 = new SQLiteCommand(drop, con);
                command1.ExecuteNonQuery();

                string table = "create table PERSONS (ID integer primary key, FirstName text, LastName text, City text, Age integer);";

                SQLiteCommand command2 = new SQLiteCommand(table, con);
                command2.ExecuteNonQuery();
            }
        }

        public Person GetPerson(int id)
        {
            Person person = new Person();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                SQLiteCommand getcom = new SQLiteCommand("Select * from Persons WHERE Id = @Id",
                    con);

                getcom.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getcom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            person.Id = id2;
                        }
                        person.FirstName = reader["FirstName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.City = reader["City"].ToString();

                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            person.Age = age;
                        }
                    }
                }
                return person;
            }

        }

        public int UpdatePerson(Person person)
        {
            int row = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                string query = "UPDATE Persons SET FirstName = @FirstName, LastName = @LastName, City = @City, Age = @Age WHERE Id = @Id";

                SQLiteCommand updatecom = new SQLiteCommand(query, con);
                updatecom.Parameters.AddWithValue("@Id", person.Id);
                updatecom.Parameters.AddWithValue("@FirstName", person.FirstName);
                updatecom.Parameters.AddWithValue("@LastName", person.LastName);
                updatecom.Parameters.AddWithValue("@City", person.City);
                updatecom.Parameters.AddWithValue("@Age", person.Age);

                try
                {
                    row = updatecom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Detail: " + ex.ToString());
                }
            }
            return row;
        }

        public int DeletePerson(Person person)
        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                string query = "DELETE FROM Persons WHERE id=@Id";
                SQLiteCommand deletecom = new SQLiteCommand(query, con);
                deletecom.Parameters.AddWithValue("@Id", person.Id);

                try
                {
                    row = deletecom.ExecuteNonQuery();

                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }

                return row;
            }

        }

        public List<Person> ReadAllPersons()
        {
            List<Person> listPersons = new List<Person>();
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("Select * from Persons", con);

                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Create Person object
                        Person person = new Person();

                        if (Int32.TryParse(reader["Id"].ToString(), out int id))
                        {
                            person.Id = id;
                        }
                        person.FirstName = reader["FirstName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.City = reader["City"].ToString();
                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            person.Age = age;
                        }
                        listPersons.Add(person);

                    }

                }
            }
            return listPersons;
        }
    }
}
