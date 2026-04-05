namespace Lapihub.ViewModels
{
    public class AdminUserListItemViewModel
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public int OrdersCount { get; set; }

        public int CartItemsCount { get; set; }

        public bool IsAdmin { get; set; }
    }
}