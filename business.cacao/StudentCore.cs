using dataAccess.cacao;
using models.cacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace business.cacao
{
    public class StudentCore
    {
        readonly Access dataAccess = new Access();

        public List<Student> GetStudents()
        {
            try
            {
                List<Student> students = new List<Student>();
                var command = dataAccess.CreateSqlCommand("SELECT * FROM Student;");

                SqlDataReader reader = dataAccess.ExecuteCommand(command);

                if (!reader.HasRows)
                    return students;

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        name = reader["name"].ToString(),
                        origin_score = Convert.ToInt32(reader["origin_score"].ToString()),
                        final_score = Convert.ToInt32(reader["final_score"].ToString())
                    });
                }

                return students;
            }
            finally
            {
                dataAccess.Dispose();
            }
        }

        public Student GetStudent(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    dataAccess.CreateParameter("@id", DbType.Int32, id)
                };
                var command = dataAccess.CreateSqlCommand("SELECT * FROM Student s WHERE s.id = @id;", parameters);

                SqlDataReader reader = dataAccess.ExecuteCommand(command);

                if (!reader.HasRows)
                    return null;

                reader.Read();

                return new Student
                {
                    id = Convert.ToInt32(reader["id"].ToString()),
                    name = reader["name"].ToString(),
                    origin_score = Convert.ToInt32(reader["origin_score"].ToString()),
                    final_score = Convert.ToInt32(reader["final_score"].ToString())
                };
            }
            finally
            {
                dataAccess.Dispose();
            }
        }

        public void DeleteStudent(int id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    dataAccess.CreateParameter("@id", DbType.Int32, id)
                };
                var command = dataAccess.CreateSqlCommand("DELETE FROM Student WHERE id = @id;", parameters);

                dataAccess.ExecuteNoneCommand(command);
            }
            finally
            {
                dataAccess.Dispose();
            }
        }

        public void AddStudent(Student info)
        {
            try
            {
                info.final_score = info.GetNearestMult();
                List<SqlParameter> dataStudent = new List<SqlParameter>
                {
                    dataAccess.CreateParameter("@name", DbType.String, info.name),
                    dataAccess.CreateParameter("@origin_score", DbType.Int32, info.origin_score),
                    dataAccess.CreateParameter("@final_score", DbType.Int32, info.final_score)
                };

                var command = dataAccess.CreateSqlCommand("INSERT INTO Student VALUES (@name, @origin_score, @final_score)", dataStudent);

                dataAccess.ExecuteNoneCommand(command);
            }
            finally
            {
                dataAccess.Dispose();
            }
        }
    }
}
