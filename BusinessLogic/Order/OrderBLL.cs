using Nettbutikk.DataAccess;
using Nettbutikk.Model;

namespace Nettbutikk.BusinessLogic
{
    public class OrderBLL : IOrderLogic
    {
        private IOrderRepo _repo;
        private IProductRepo _productrepo;
        private ICustomerRepo _customerrepo;

        public OrderBLL()
        {
            _repo = new OrderRepo();
            _productrepo = new ProductRepo();
            _customerrepo = new CustomerRepo();
        }

        public OrderBLL(IOrderRepo stub)
        {
            _repo = stub;
            _productrepo = new ProductRepoStub();
            _customerrepo = new CustomerRepoStub();
        }

        public Order GetReciept(int orderId)
        {
            return _repo.GetReciept(orderId);
        }

        public int PlaceOrder(Order order)
        {
            return _repo.PlaceOrder(order);
        }
        public List<OrderModel> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }

        public bool UpdateOrderline(OrderlineModel orderline)
        {
            return _repo.UpdateOrderline(orderline);
        }

        public double GetOrderSumTotal(int orderId)
        {
            return _repo.GetOrderSumTotal(orderId);
        }

        public bool DeleteOrder(int orderId)
        {
            return _repo.DeleteOrder(orderId);
        }

        public List<ProductModel> GetAllProducts()
        {
            return _productrepo.GetAllProducts();
        }

        public OrderModel GetOrder(int OrderId)
        {
            return _repo.GetOrder(OrderId);
        }

        public List<OrderModel> GetOrders(int CustomerId)
        {
            return _repo.GetOrders(CustomerId);
        }
    }
}
