using Microsoft.EntityFrameworkCore;

namespace Coffe_site.Models
{
    public class OrderService : IOrderService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _logger;


        public OrderService(IConfiguration logger, AppDbContext context)
        {

            _context = context;
            _logger = logger;

        }

        public async Task<object[]> GetListOfOrders(int userId)
        {

            var orderList = from c in _context.Orders
                       where c.user_id == userId
                       orderby c.Date
                      
                            select c;
            ;

            var list = new List<object>();



            if (orderList != null)
            {


                foreach (var order in orderList)
                {
                    var OrderToDisplay = new Order()
                    {
                        Date = order.Date,
                        Count = order.Count,
                        Delivery_Price = order.Delivery_Price,
                        address_Id = order.address_Id,
                        item_id = order.item_id,
                        user_id = userId,
                        Pickup = order.Pickup,
                        

                    };

                    list.Add(OrderToDisplay);

                    var product = await _context.Items.FirstOrDefaultAsync(x => x.Product_Id == OrderToDisplay.item_id);
                    list.Add(product);
                    var address = await _context.Address.FirstOrDefaultAsync(x => x.Id == OrderToDisplay.address_Id);
                    list.Add(address);


                }
                return list.ToArray();
            }
            else
            {
                return list.ToArray();

            }

        }

       
        
    }
}
