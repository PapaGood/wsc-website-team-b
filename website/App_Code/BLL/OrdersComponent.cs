using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// Summary description for OrdersComponent
    /// </summary>
    public class OrdersComponent
    {
        public OrdersComponent()
        {
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            DataSet ds = new DataSet();
            DataSetTableAdapters.ordersTableAdapter adapter = new DataSetTableAdapters.ordersTableAdapter();
            adapter.Fill(ds.orders);
            DataSet.ordersDataTable table = adapter.GetOrderData();

            // load all orders into the list
            foreach (DataSet.ordersRow row in table.Rows)
            {
                // create memory for new order & fill in properties
                Order o = new Order();
                o.ID = row.orderId;
                o.CatalogId = row.catalogId;
                o.ClientId = row.clientId;
                o.PaymentId = row.paymentId;
                o.OrderDate = row.orderDate;
                o.Details = row.details;
                o.Price = row.price;

                // add this order to the list
                orders.Add(o);
            }
            // returns the list w/ 0 or more Orders
            return orders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>An order if found; null if order not found</returns>
        public Order GetOrderById(int id)
        {
            DataSet ds = new DataSet();
            DataSetTableAdapters.ordersTableAdapter adapter = new DataSetTableAdapters.ordersTableAdapter();
            adapter.Fill(ds.orders);
            Order o = new Order();

            // find the order
            DataSet.ordersDataTable table = adapter.GetOrderById(id);

            // if a order was found, then fill in the details 
            if (table.Rows.Count > 0)
            {
                // use the 1st row's data
                DataSet.ordersRow row = (DataSet.ordersRow)table.Rows[0];
                o.ID = row.orderId;
                o.CatalogId = row.catalogId;
                o.ClientId = row.clientId;
                o.PaymentId = row.paymentId;
                o.OrderDate = row.orderDate;
                o.Details = row.details;
                o.Price = row.price;
                return o;
            }
            else
            {
                return null;
            }
        }

        public List<Order> GetOrdersByClientId(int id)
        {
            List<Order> orders = new List<Order>();
            DataSet ds = new DataSet();
            DataSetTableAdapters.ordersTableAdapter adapter = new DataSetTableAdapters.ordersTableAdapter();
            adapter.Fill(ds.orders);

            // find the order
            DataSet.ordersDataTable table = adapter.GetOrdersByClientId(id);

            // load all orders into the list
            foreach (DataSet.ordersRow row in table.Rows)
            {
                // create memory for new order & fill in properties
                Order o = new Order();
                o.ID = row.orderId;
                o.CatalogId = row.catalogId;
                o.ClientId = row.clientId;
                o.PaymentId = row.paymentId;
                o.OrderDate = row.orderDate;
                o.Details = row.details;
                o.Price = row.price;

                // add this order to the list
                orders.Add(o);
            }
            // returns the list w/ 0 or more Orders
            return orders;
        }

        public int UpdateOrder(Order o)
        {
            return UpdateOrder(o.ID, o.CatalogId, o.ClientId, o.PaymentId, o.OrderDate, o.Details, o.Price);
        }

        public int UpdateOrder(int orderId, int catalogId, int clientId, int paymentId, DateTime orderDate, String details, double price)
        {
            DataSet ds = new DataSet();
            DataSetTableAdapters.ordersTableAdapter adapter = new DataSetTableAdapters.ordersTableAdapter();

            return adapter.Update(catalogId, clientId, paymentId, orderDate, details, price, orderId);
        }

        public int InsertOrder(Order o)
        {
            return InsertOrder(o.CatalogId, o.ClientId, o.PaymentId, o.OrderDate, o.Details, o.Price);
        }

        public int InsertOrder(int catalogId, int clientId, int paymentId, DateTime orderDate, String details, double price)
        {
            DataSet ds = new DataSet();
            DataSetTableAdapters.ordersTableAdapter adapter = new DataSetTableAdapters.ordersTableAdapter();

            adapter.Insert(catalogId, clientId, paymentId, orderDate, details, price);
            // this is a *bad* way of getting the record's identity, but MS Access doesn't support batch SQL statements
            // a real DB would be able to combine the insert statement and a select statement in one batch call!
            int identity = (int)adapter.GetIdentity();
            return identity;
        }

        public int DeleteOrder(Order o)
        {
            return DeleteOrder(o.ID);
        }

        public int DeleteOrder(int id)
        {
            DataSet ds = new DataSet();
            DataSetTableAdapters.ordersTableAdapter adapter = new DataSetTableAdapters.ordersTableAdapter();

            return adapter.Delete(id);
        }
    }
}