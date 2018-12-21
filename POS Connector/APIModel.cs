using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Connector
{

    //============================================================================================
    public class Article
    {
        public string articleId { get; set; }
        public string articleIdAlias { get; set; }
        public string articleName { get; set; }
        public string brand { get; set; }
        public string color { get; set; }
        public string department { get; set; }
        public string departmentType { get; set; }
        public string gender { get; set; }
        public int id { get; set; }
        public int price { get; set; }
        public string size { get; set; }
        public string unit { get; set; }
    }

    //============================================================================================
    public class CostCategory
    {
        public int Id { get; set; }
        public string CostCategoryId { get; set; }
        public string Name { get; set; }
        public string Coa { get; set; }
    }
    //============================================================================================

    public class Inventory
    {
        public string id { get; set; }
        public string articleId { get; set; }
        public string goodQty { get; set; }
        public string rejectQty { get; set; }
        public string whGoodQty { get; set; }
        public string whRejectQty { get; set; }
        public string status { get; set; }
    }
    //============================================================================================

    public class TransactionLine
    {
        public Article article { get; set; }
        public int articleIdFk { get; set; }
        public int id { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int subtotal { get; set; }
        public string transactionId { get; set; }
        public int transactionIdFk { get; set; }
        public int discount { get; set; }
        public int discountType { get; set; }
        public string discountCode { get; set; }
        public string spgId { get; set; }
    }
    //============================================================================================

    public class Transaction
    {
        public string storeCode { get; set; }
        public string SequenceNumber { get; set; }
        public int cash { get; set; }
        public int change { get; set; }
        public string customerIdStore { get; set; }
        public string customerId { get; set; }
        public string currency { get; set; }
        public string date { get; set; }
        public int discount { get; set; }
        public string employeeId { get; set; }
        public int id { get; set; }
        public int paymentType { get; set; }
        public string receiptId { get; set; }
        public string spgId { get; set; }
        public int status { get; set; }
        public string time { get; set; }
        public string timeStamp { get; set; }
        public int total { get; set; }
        public int Edc1 { get; set; }
        public int Edc2 { get; set; }
        public string Bank1 { get; set; }
        public string Bank2 { get; set; }
        public string NoRef1 { get; set; }
        public string NoRef2 { get; set; }
        public string transactionId { get; set; }
        public int transactionType { get; set; }
        public IList<TransactionLine> transactionLines { get; set; }
    }
    //============================================================================================

    public class ReturnOrderLine
    {
        public Article article { get; set; }
        public int articleIdFk { get; set; }
        public int id { get; set; }
        public int quantity { get; set; }
        public string returnOrderId { get; set; }
        public int returnOrderIdFk { get; set; }
        public string unit { get; set; }
    }
    //============================================================================================

    public class RetrunOrder
    {
        public string storeCode { get; set; }
        public string SequenceNumber { get; set; }
        public string date { get; set; }
        public int id { get; set; }
        public string remark { get; set; }
        public string returnOrderId { get; set; }
        public IList<ReturnOrderLine> returnOrderLines { get; set; }
        public int status { get; set; }
        public string time { get; set; }
        public string timeStamp { get; set; }
        public int totalQty { get; set; }
        public string warehouseId { get; set; }
        public string oldSJ { get; set; }
    }
    //============================================================================================

    public class RequestOrderLine
    {
        public Article article { get; set; }
        public int articleIdFk { get; set; }
        public int id { get; set; }
        public int quantity { get; set; }
        public string requestOrderId { get; set; }
        public int requestOrderIdFk { get; set; }
        public string unit { get; set; }
    }
    //============================================================================================

    public class RequestOrder
    {
        public string storeCode { get; set; }
        public string SequenceNumber { get; set; }
        public string date { get; set; }
        public int id { get; set; }
        public string requestDeliveryDate { get; set; }
        public string requestOrderId { get; set; }
        public IList<RequestOrderLine> requestOrderLines { get; set; }
        public int status { get; set; }
        public string time { get; set; }
        public string timeStamp { get; set; }
        public int totalQty { get; set; }
        public string warehouseId { get; set; }
        public string customerIdStore { get; set; }
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string oldSJ { get; set; }
    }
    //============================================================================================


    public class MutasiOrderLine
    {
        public Article article { get; set; }
        public int articleIdFk { get; set; }
        public int id { get; set; }
        public string mutasiOrderId { get; set; }
        public int mutasiOrderIdFk { get; set; }
        public int quantity { get; set; }
        public string unit { get; set; }
    }
    //============================================================================================

    public class MutasiOrder
    {
        public string storeCode { get; set; }
        public string SequenceNumber { get; set; }
        public string date { get; set; }
        public int id { get; set; }
        public string mutasiFromWarehouse { get; set; }
        public string mutasiToWarehouse { get; set; }
        public string mutasiOrderId { get; set; }
        public IList<MutasiOrderLine> mutasiOrderLines { get; set; }
        public string requestDeliveryDate { get; set; }
        public int status { get; set; }
        public string time { get; set; }
        public string timeStamp { get; set; }
        public int totalQty { get; set; }
         public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string oldSJ { get; set; }
    }
    //============================================================================================

    public class DeliveryOrderLine
    {
        public Article article { get; set; }
        public string packingNumber { get; set; }
        public int articleIdFk { get; set; }
        public string deliveryOrderId { get; set; }
        public int deliveryOrderIdFk { get; set; }
        public int id { get; set; }
        public int qtyDeliver { get; set; }
        public int? qtyReceive { get; set; }
        public int? amount { get; set; }
    }
    //============================================================================================

    public class DeliveryOrder
    {
        public string date { get; set; }
        public string deliveryDate { get; set; }
        public string deliveryOrderId { get; set; }
        public IList<DeliveryOrderLine> deliveryOrderLines { get; set; }
        public string deliveryTime { get; set; }
        public int id { get; set; }
        public int status { get; set; }
        public string time { get; set; }
        public object timeStamp { get; set; }
        public int? totalQty { get; set; }
        public object storeCode { get; set; }
        public string warehouseFrom { get; set; }
        public string warehouseTo { get; set; }
        public object CustomerIdStore { get; set; }
        public object employeeId { get; set; }
        public object employeeName { get; set; }
        public int? totalAmount { get; set; }

    }
    //============================================================================================

    public class StoreData
    {
        public string userId { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string deviceId { get; set; }
        public string storeId { get; set; }

    }
    //============================================================================================

    public class Denomination
    {
        public int id { get; set; }
        public int currencyIdFk { get; set; }
        public int nominal { get; set; }
    }
    //============================================================================================
    public class Currency
    {
        public string sign { get; set; }
        public string name { get; set; }
        public IList<Denomination> denominations { get; set; }
    }
    //============================================================================================
    public class Store
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Regional { get; set; }
        public int StoreTypeId { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string WarehouseId { get; set; }
        public string CustomerIdStore { get; set; }
    }
    //============================================================================================
    public class Warehouse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string City { get; set; }
        public string Regional { get; set; }
        public string Division { get; set; }
    }
    //============================================================================================
    public class Bank
    {
        public string bankId { get; set; }
        public string bankName { get; set; }
    }
    //============================================================================================
    public class Possition
    {
        public int id { get; set; }
        public string possitionId { get; set; }
        public string possitionName { get; set; }
    }
    //============================================================================================
    public class Employee
    {
        public int id { get; set; }
        public string employeeId { get; set; }
        public string name { get; set; }
        public Possition possition { get; set; }
    }
    //============================================================================================
    public class StoreMaster_respone
    {
        public Currency currency { get; set; }
        public Store store { get; set; }
        public Warehouse warehouse { get; set; }
        public IList<Bank> banks { get; set; }
        public IList<Employee> employees { get; set; }
    }
    //============================================================================================
    public class Brand
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public IList<object> DiscountRetailLines { get; set; }
    }
    //============================================================================================
    public class Color
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public IList<object> DiscountRetailLines { get; set; }
    }
    //============================================================================================
    public class Department
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public IList<object> DiscountRetailLines { get; set; }
    }
    //============================================================================================
    public class DepartmentType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public IList<object> DiscountRetailLines { get; set; }
    }
    //============================================================================================
    public class Gender
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public IList<object> DiscountRetailLines { get; set; }
    }
    //============================================================================================
    public class Size
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public IList<object> DiscountRetailLines { get; set; }
    }
    //============================================================================================
    public class ItemDimension
    {
        public IList<Brand> brands { get; set; }
        public IList<Color> colors { get; set; }
        public IList<Department> departments { get; set; }
        public IList<DepartmentType> departmentTypes { get; set; }
        public IList<Gender> genders { get; set; }
        public IList<Size> sizes { get; set; }
    }
    //===============================================================================================
    public class Get_Store_Master
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Regional { get; set; }
        public int? StoreTypeId { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string WarehouseId { get; set; }
    }
    //===============================================================================================
    public class PromotionLine
    {
        public int id { get; set; }
        public int promotionIdFk { get; set; }
        public string discountCode { get; set; }
        public string articleId { get; set; }
        public string articleName { get; set; }
        public string brand { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string gender { get; set; }
        public string department { get; set; }
        public string departmentType { get; set; }
        public string customerGroup { get; set; }
        public int qta { get; set; }
        public int amount { get; set; }
        public string bank { get; set; }
        public int discountPercent { get; set; }
        public int discountPrice { get; set; }
        public int specialPrice { get; set; }
        public int articleIdDiscount { get; set; }
        public string articleNameDiscount { get; set; }
    }
    //===============================================================================================
    public class Promotion
    {
        public string description { get; set; }
        public string discountCategory { get; set; }
        public string discountCode { get; set; }
        public string discountName { get; set; }
        public string endDate { get; set; }
        public int id { get; set; }
        public string startDate { get; set; }
        public int status { get; set; }
        public IList<PromotionLine> promotionLines { get; set; }
    }
    //===============================================================================================
    public class PettyCashLine
    {
        public string expenseName { get; set; }
        public int id { get; set; }
        public string pettyCashId { get; set; }
        public int pettyCashIdFk { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int total { get; set; }
    }

    public class PettyCash
    {
        public string storeCode { get; set; }
        public string SequenceNumber { get; set; }
        public string customerIdStore { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string expenseCategoryId { get; set; }
        public string expenseCategory { get; set; }
        public string expenseDate { get; set; }
        public int id { get; set; }
        public string pettyCashId { get; set; }
        public int status { get; set; }
        public string time { get; set; }
        public string timeStamp { get; set; }
        public int totalExpense { get; set; }
        public IList<PettyCashLine> pettyCashLine { get; set; }
    }
}
