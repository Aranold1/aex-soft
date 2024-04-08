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
                var result = Db.Customers.
                    Where(c => c.Orders.Any(o => o.Date >= beginDate)).
                    Select(c => new CustomerViewModel
                    {
                        CustomerName = c.Name,
                        ManagerName = c.Manager.Name,
                        Amount = c.Orders.Where(o => o.Date >= beginDate).Sum(x => x.Amount)
                    });

                return result.Where(x=>x.Amount>sumAmount).ToList();
            }
        }
    }
}