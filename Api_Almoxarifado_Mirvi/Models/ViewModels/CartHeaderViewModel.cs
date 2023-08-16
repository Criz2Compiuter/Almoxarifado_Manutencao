namespace Api_Almoxarifado_Mirvi.Models.ViewModels
{
    public class CartHeaderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public double TotalAmount { get; set; } = 0.00d;
    }
}
