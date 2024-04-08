namespace Program
{
    class Program
    {
        static void Main()
        {
            var program = new Program();
            var Customers = program.GetCustomers(new DateTime(2004, 12, 4),500);
            foreach (var customer in Customers)
            {
                Console.WriteLine(customer.Amount);
                Console.WriteLine(customer.ManagerName);
                Console.WriteLine(customer.CustomerName);
            }
        }
        //я бы рекомендовал назвать эту функцию GetCustomersViewModels
        public List<CustomerViewModel> GetCustomers(DateTime beginDate,decimal sumAmount)
        {
            
            using(var Db = new PostgresContext())
            {
                var result = Db.Orders.Where(o=>o.Date>=beginDate).Select(x => new CustomerViewModel()
                {
                    CustomerName = x.Customer.Name,
                    ManagerName = x.Customer.Manager.Name,
                    Amount = x.Customer.Orders.Where(x=>x.Date>=beginDate).Sum(x=>x.Amount)
                });

                return result.Where(x=>x.Amount>sumAmount).ToList();
            }
        }
    }
}