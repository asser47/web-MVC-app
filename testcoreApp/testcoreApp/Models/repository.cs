namespace testcoreApp.Models
{
    public class Repository
    {
        private static List<Employee> employeelist = new List<Employee>();

        public static IEnumerable<Employee> GetEmployees()
        {
            return employeelist;
        }
        public static void AddEmployee(Employee employee)
        {
            employeelist.Add(employee);
        }
    }
}
