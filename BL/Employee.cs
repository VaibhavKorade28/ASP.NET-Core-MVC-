namespace BL
{
    public class Employee
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Dept { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        public string Password { get; set; }

        public Employee(int id, string name, string dept, string email, double salary, string password)
        {
            Id = id;
            Name = name;
            Dept = dept;
            Email = email;
            Salary = salary;
            Password = password;
        }
        public Employee() { }
    }
}