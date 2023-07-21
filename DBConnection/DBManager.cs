using MySql.Data.MySqlClient;
using BL;
namespace DBConnection
{
    public class DBManager
    {
        public static string conString = @"server=localhost;port=3306;user=root; password=Hitman@45;database=test";
        public static bool AddEmployee(Employee emp)
        {
            bool status = false;
            string query = "Insert into employee (name,salary,dept,email,password) Values( '" + emp.Name + "','"
            + emp.Salary + "','" +emp.Dept + "','"+emp.Email + "','" + emp.Password + "')";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = conString;
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();  //DML
                status = true;
            }catch(Exception ex)
            {
                throw ex;
               
            }
            finally
            {
                conn.Close();
            }
            return status;



        }

        public static bool DeleteById(int id)
        {
            bool status = false;
            string query = string.Format("delete from employee where id='{0}'", id);
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = conString;
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
                status = true;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }

        public static List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            MySqlConnection conn= new MySqlConnection();
            conn.ConnectionString = conString;
            string query = "select * from employee";
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = conn;
                conn.Open();
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    double salary = double.Parse(reader["salary"].ToString());
                    string dept = reader["dept"].ToString();
                    string email = reader["email"].ToString();
                    string password = reader["password"].ToString();
                    Employee emp = new Employee(id, name,dept, email,salary,password);
                      list.Add(emp);
                }
            }catch(Exception e)
            {
                throw e;

            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public static Employee GetEmployee(string email, string password)
        {
            Employee emp = null;
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = conString;
            try {
                string query = string.Format("select * from employee where email='{0}' and password= '{1}'",email,password);
                conn.Open();
                MySqlCommand command = new MySqlCommand(query,conn);  

                MySqlDataReader reader = command.ExecuteReader();   
                if(reader.Read())
                {
                    emp = new Employee
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString(),
                        Salary = double.Parse(reader["salary"].ToString()),
                        Email = reader["email"].ToString(),
                        Dept = reader["dept"].ToString(),
                        Password = reader["password"].ToString()
                    };
                    return emp;
                }
            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return emp;
        }
        public static Employee GetById(int id)
        {
            Employee emp = null;
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = conString;
            string query = string.Format("select * from employee where id='{0}'", id);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    emp = new Employee
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString(),
                        Salary = double.Parse(reader["salary"].ToString()),
                        Email = reader["email"].ToString(),
                        Dept = reader["dept"].ToString(),
                        Password = reader["password"].ToString()
                        
                    };
                    return emp;
                }
            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();   
            }
            return emp;
        }

        public static bool UpdateEmpDetails(int id, string name, double sal, string email, string dept, string password)
        {
            Console.WriteLine(id);
            
            bool status = false;
            string query = string.Format("update employee set name='{0}', email='{1}',dept='{2}',salary='{3}',password='{4}' where id='{5}'", name, email, dept
                , sal, password,id);
            MySqlConnection conn=new MySqlConnection();
             conn.ConnectionString = conString;
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);

                command.ExecuteNonQuery();
                status=true;

            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }
    }
}