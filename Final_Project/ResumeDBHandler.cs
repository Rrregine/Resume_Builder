﻿using System;
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
                FirstName = "Regine",
                LastName = "Wang",
                Gender = "Female",
                Age = 18,
                ContactInfo = "2232176@champlaincollege.qc.ca",
                Experience = "none",
                Education = "College Level",
                Hobbies = "Writing",
                Ref = "none"
            };

            Resume newR2 = new Resume
            {
                FirstName = "Mariya",
                LastName = "Zolovska",
                Gender = "Female",
                Age = 18,
                ContactInfo = "2230965@champlaincollege.qc.ca",
                Experience = "some",
                Education = "College Level",
                Hobbies = "some",
                Ref = "none"
            };

            Resume newR3 = new Resume
            {
                FirstName = "Anna",
                LastName = "Pogonetz",
                Gender = "Female",
                Age = 18,
                ContactInfo = "2231334@champlaincollege.qc.ca",
                Experience = "none",
                Education = "College Level",
                Hobbies = "Drawing",
                Ref = "none"
            };

            AddResume(newR1);
            AddResume(newR2);
            AddResume(newR3);

        }

        public static ResumeDBHandler Instance { get { return instance; } }

        public int AddResume(Resume resume)
        {
            int newId = 0;
            int rows = 0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                //Create parameterized query
                string query = "INSERT INTO RESUMES (FirstName, LastName, Gender, Age, ContactInfo, Experience, Education, Hobbies, Ref) VALUES (@FirstName, @LastName, @Gender, @Age, @ContactInfo, @Experience, @Education, @Hobbies, @Ref);";

                SQLiteCommand insertcom = new SQLiteCommand(query, con);

                //Pass values to query parameter
                insertcom.Parameters.AddWithValue("@FirstName", resume.FirstName);
                insertcom.Parameters.AddWithValue("@LastName", resume.LastName);
                insertcom.Parameters.AddWithValue("@Gender", resume.Gender);
                insertcom.Parameters.AddWithValue("@Age", resume.Age);
                insertcom.Parameters.AddWithValue("@ContactInfo", resume.ContactInfo);
                insertcom.Parameters.AddWithValue("@Experience", resume.Experience);
                insertcom.Parameters.AddWithValue("@Education", resume.Education);
                insertcom.Parameters.AddWithValue("@Hobbies", resume.Hobbies);
                insertcom.Parameters.AddWithValue("@Ref", resume.Ref);

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
                string drop = "drop table if exists RESUMES";
                SQLiteCommand command1 = new SQLiteCommand(drop, con);
                command1.ExecuteNonQuery();

                string table = "create table RESUMES (ID integer primary key, FirstName text, LastName text, Gender text, Age integer, ContactInfo text, Experience text, Education text, Hobbies text, Ref text);";

                SQLiteCommand command2 = new SQLiteCommand(table, con);
                command2.ExecuteNonQuery();
            }
        }

        public Resume GetResume(int id)
        {
            Resume resume = new Resume();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                SQLiteCommand getcom = new SQLiteCommand("Select * from Resumes WHERE Id = @Id",
                    con);

                getcom.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getcom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            resume.Id = id2;
                        }
                        resume.FirstName = reader["FirstName"].ToString();
                        resume.LastName = reader["LastName"].ToString();
                        resume.Gender = reader["Gender"].ToString();

                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            resume.Age = age;
                        }

                        
                        resume.ContactInfo = reader["ContactInfo"].ToString();

                       
                        resume.Experience = reader["Experience"].ToString();

                         
                        resume.Education = reader["Education"].ToString();

                        
                        resume.Hobbies = reader["Hobbies"].ToString();

                        
                        resume.Ref = reader["Ref"].ToString();
                    }
                }
                return resume;
            }

        }

        public int UpdateResume(Resume resume)
        {
            int row = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                string query = "UPDATE Resumes SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender, Age = @Age, ContactInfo = @ContactInfo, Experience = @Experience, Education = @Education, Hobbies = @Hobbies, Ref = @Ref WHERE Id = @Id";

                SQLiteCommand updatecom = new SQLiteCommand(query, con);
                updatecom.Parameters.AddWithValue("@Id", resume.Id);
                updatecom.Parameters.AddWithValue("@FirstName", resume.FirstName);
                updatecom.Parameters.AddWithValue("@LastName", resume.LastName);
                updatecom.Parameters.AddWithValue("@Gender", resume.Gender);
                updatecom.Parameters.AddWithValue("@Age", resume.Age);
                updatecom.Parameters.AddWithValue("@ContactInfo", resume.ContactInfo);
                updatecom.Parameters.AddWithValue("@Experience", resume.Experience);
                updatecom.Parameters.AddWithValue("@Education", resume.Education);
                updatecom.Parameters.AddWithValue("@Hobbies", resume.Hobbies);
                updatecom.Parameters.AddWithValue("@Ref", resume.Ref);

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

        public int DeleteResume(Resume resume)
        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                string query = "DELETE FROM Resumes WHERE id=@Id";
                SQLiteCommand deletecom = new SQLiteCommand(query, con);
                deletecom.Parameters.AddWithValue("@Id", resume.Id);

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

        public List<Resume> ReadAllResumes()
        {
            List<Resume> listResumes = new List<Resume>();
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("Select * from Resumes", con);

                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Create Resume object
                        Resume resume = new Resume();

                        if (Int32.TryParse(reader["Id"].ToString(), out int id))
                        {
                            resume.Id = id;
                        }
                        resume.FirstName = reader["FirstName"].ToString();
                        resume.LastName = reader["LastName"].ToString();
                        resume.Gender = reader["Gender"].ToString();
                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            resume.Age = age;
                        }


                        resume.ContactInfo = reader["ContactInfo"].ToString();


                        resume.Experience = reader["Experience"].ToString();


                        resume.Education = reader["Education"].ToString();


                        resume.Hobbies = reader["Hobbies"].ToString();


                        resume.Ref = reader["Ref"].ToString();

                        listResumes.Add(resume);

                    }

                }
            }
            return listResumes;
        }
    }
}
