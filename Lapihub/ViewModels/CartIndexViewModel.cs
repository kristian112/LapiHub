namespace Lapihub.ViewModels
{
    public class CartIndexViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal TotalAmount => Items.Sum(i => i.LineTotal);
    }
}