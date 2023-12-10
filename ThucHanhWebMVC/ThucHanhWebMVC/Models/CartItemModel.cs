using System.Drawing;

namespace ThucHanhWebMVC.Models
{
    public class CartItemModel
    {
        public string ProductID {  get; set; }
        public string ProductName { get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public decimal Total {
            get { return Quantity*Price; }
        }
        public string Image {  get; set; }
        public CartItemModel()
        {

        }
        public CartItemModel(TDanhMucSp product) 
        {
            ProductID = product.MaSp;
            ProductName = product.TenSp;
            Price = (decimal)product.GiaNhoNhat;
            Quantity = 1;
            Image = product.AnhDaiDien;
        }
        
    }
}
