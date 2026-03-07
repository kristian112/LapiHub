using Lapihub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lapihub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItem>()
                .HasIndex(ci => new { ci.UserId, ci.ProductId })
                .IsUnique();

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Храна за кучета" },
                new Category { Id = 2, Name = "Храна за котки" },
                new Category { Id = 3, Name = "Лакомства" },
                new Category { Id = 4, Name = "Играчки" },
                new Category { Id = 5, Name = "Хигиена и грижа" },
                new Category { Id = 6, Name = "Аксесоари" }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Пълноценна суха храна за кучета Adult Chicken",
                    Brand = "Royal Canin",
                    CategoryId = 1,
                    Price = 42.90m,
                    Description = "Балансирана суха храна за пораснали кучета с пилешко месо и витамини.",
                    ImageUrl = "https://placehold.co/600x400?text=Dog+Food+1"
                },
                new Product
                {
                    Id = 2,
                    Name = "Суха храна за кучета Mini Adult",
                    Brand = "Purina Pro Plan",
                    CategoryId = 1,
                    Price = 36.50m,
                    Description = "Специално разработена формула за кучета от малки породи.",
                    ImageUrl = "https://placehold.co/600x400?text=Dog+Food+2"
                },
                new Product
                {
                    Id = 3,
                    Name = "Храна за кучета Sensitive Digestion",
                    Brand = "Brit Care",
                    CategoryId = 1,
                    Price = 39.80m,
                    Description = "Подходяща за кучета с чувствителен стомах и специални хранителни нужди.",
                    ImageUrl = "https://placehold.co/600x400?text=Dog+Food+3"
                },
                new Product
                {
                    Id = 4,
                    Name = "Пауч за кучета с телешко",
                    Brand = "Pedigree",
                    CategoryId = 1,
                    Price = 3.20m,
                    Description = "Вкусен пауч с телешко за ежедневно хранене на кучета.",
                    ImageUrl = "https://placehold.co/600x400?text=Dog+Food+4"
                },

                new Product
                {
                    Id = 5,
                    Name = "Суха храна за котки Indoor",
                    Brand = "Royal Canin",
                    CategoryId = 2,
                    Price = 29.90m,
                    Description = "Пълноценна храна за домашни котки с контрол на теглото и космените топки.",
                    ImageUrl = "https://placehold.co/600x400?text=Cat+Food+1"
                },
                new Product
                {
                    Id = 6,
                    Name = "Храна за котки Sterilised Salmon",
                    Brand = "Purina One",
                    CategoryId = 2,
                    Price = 21.40m,
                    Description = "Подходяща за кастрирани котки, с вкус на сьомга.",
                    ImageUrl = "https://placehold.co/600x400?text=Cat+Food+2"
                },
                new Product
                {
                    Id = 7,
                    Name = "Пауч за котки с пиле в сос",
                    Brand = "Whiskas",
                    CategoryId = 2,
                    Price = 2.40m,
                    Description = "Мека и ароматна храна за котки с пиле в сочен сос.",
                    ImageUrl = "https://placehold.co/600x400?text=Cat+Food+3"
                },
                new Product
                {
                    Id = 8,
                    Name = "Храна за котки Grain Free Duck",
                    Brand = "N&D",
                    CategoryId = 2,
                    Price = 34.70m,
                    Description = "Беззърнена формула с патешко месо за активни котки.",
                    ImageUrl = "https://placehold.co/600x400?text=Cat+Food+4"
                },

                new Product
                {
                    Id = 9,
                    Name = "Лакомства за кучета Dental Sticks",
                    Brand = "Pedigree",
                    CategoryId = 3,
                    Price = 8.90m,
                    Description = "Дентални пръчици за поддържане на устната хигиена при кучета.",
                    ImageUrl = "https://placehold.co/600x400?text=Treats+1"
                },
                new Product
                {
                    Id = 10,
                    Name = "Меки лакомства за котки с риба",
                    Brand = "Dreamies",
                    CategoryId = 3,
                    Price = 4.60m,
                    Description = "Хрупкави отвън и меки отвътре лакомства за котки.",
                    ImageUrl = "https://placehold.co/600x400?text=Treats+2"
                },
                new Product
                {
                    Id = 11,
                    Name = "Натурални сушени хапки за кучета",
                    Brand = "Brit",
                    CategoryId = 3,
                    Price = 7.80m,
                    Description = "Натурални лакомства за награда по време на тренировки.",
                    ImageUrl = "https://placehold.co/600x400?text=Treats+3"
                },
                new Product
                {
                    Id = 12,
                    Name = "Кремообразно лакомство за котки",
                    Brand = "Churu",
                    CategoryId = 3,
                    Price = 6.30m,
                    Description = "Любимо течно лакомство за котки, подходящо и за капризни любимци.",
                    ImageUrl = "https://placehold.co/600x400?text=Treats+4"
                },

                new Product
                {
                    Id = 13,
                    Name = "Каучукова топка за кучета",
                    Brand = "Kong",
                    CategoryId = 4,
                    Price = 14.90m,
                    Description = "Издръжлива играчка за кучета, подходяща за дъвчене и гонене.",
                    ImageUrl = "https://placehold.co/600x400?text=Toy+1"
                },
                new Product
                {
                    Id = 14,
                    Name = "Въже за дърпане",
                    Brand = "Trixie",
                    CategoryId = 4,
                    Price = 9.50m,
                    Description = "Играчка въже за активна игра и укрепване на зъбите.",
                    ImageUrl = "https://placehold.co/600x400?text=Toy+2"
                },
                new Product
                {
                    Id = 15,
                    Name = "Интерактивна мишка за котки",
                    Brand = "Ferplast",
                    CategoryId = 4,
                    Price = 11.20m,
                    Description = "Забавна играчка за котки, стимулираща ловния инстинкт.",
                    ImageUrl = "https://placehold.co/600x400?text=Toy+3"
                },
                new Product
                {
                    Id = 16,
                    Name = "Плюшена рибка с котешка мента",
                    Brand = "Catit",
                    CategoryId = 4,
                    Price = 7.40m,
                    Description = "Мека играчка за котки с котешка мента за допълнителен интерес.",
                    ImageUrl = "https://placehold.co/600x400?text=Toy+4"
                },

                new Product
                {
                    Id = 17,
                    Name = "Шампоан за кучета с алое вера",
                    Brand = "Beaphar",
                    CategoryId = 5,
                    Price = 12.60m,
                    Description = "Нежен шампоан за кучета с чувствителна кожа.",
                    ImageUrl = "https://placehold.co/600x400?text=Care+1"
                },
                new Product
                {
                    Id = 18,
                    Name = "Пясък за котки Ultra Clumping",
                    Brand = "Sanicat",
                    CategoryId = 5,
                    Price = 16.30m,
                    Description = "Бучкуващ пясък за котки с добра абсорбация и контрол на миризмите.",
                    ImageUrl = "https://placehold.co/600x400?text=Care+2"
                },
                new Product
                {
                    Id = 19,
                    Name = "Мокри кърпички за лапи",
                    Brand = "Trixie",
                    CategoryId = 5,
                    Price = 5.90m,
                    Description = "Практични мокри кърпички за почистване на лапи след разходка.",
                    ImageUrl = "https://placehold.co/600x400?text=Care+3"
                },
                new Product
                {
                    Id = 20,
                    Name = "Четка за разресване",
                    Brand = "Ferplast",
                    CategoryId = 5,
                    Price = 13.70m,
                    Description = "Удобна четка за разресване на козина при кучета и котки.",
                    ImageUrl = "https://placehold.co/600x400?text=Care+4"
                },

                new Product
                {
                    Id = 21,
                    Name = "Каишка за куче Comfort Walk",
                    Brand = "Trixie",
                    CategoryId = 6,
                    Price = 18.90m,
                    Description = "Удобна каишка за ежедневни разходки с добро сцепление и здравина.",
                    ImageUrl = "https://placehold.co/600x400?text=Accessory+1"
                },
                new Product
                {
                    Id = 22,
                    Name = "Керамична купичка за храна",
                    Brand = "Savic",
                    CategoryId = 6,
                    Price = 10.80m,
                    Description = "Стабилна и лесна за почистване купичка за вода или храна.",
                    ImageUrl = "https://placehold.co/600x400?text=Accessory+2"
                },
                new Product
                {
                    Id = 23,
                    Name = "Легло за куче Cozy Bed",
                    Brand = "Ferplast",
                    CategoryId = 6,
                    Price = 49.90m,
                    Description = "Меко и удобно легло за кучета с модерен дизайн.",
                    ImageUrl = "https://placehold.co/600x400?text=Accessory+3"
                },
                new Product
                {
                    Id = 24,
                    Name = "Нашийник за котка с звънче",
                    Brand = "Camon",
                    CategoryId = 6,
                    Price = 8.20m,
                    Description = "Лек и удобен нашийник за котка с регулируема дължина.",
                    ImageUrl = "https://placehold.co/600x400?text=Accessory+4"
                }
            );
        }
    }
}