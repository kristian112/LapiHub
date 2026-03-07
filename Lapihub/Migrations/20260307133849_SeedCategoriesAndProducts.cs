using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lapihub.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoriesAndProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Храна за кучета" },
                    { 2, "Храна за котки" },
                    { 3, "Лакомства" },
                    { 4, "Играчки" },
                    { 5, "Хигиена и грижа" },
                    { 6, "Аксесоари" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Royal Canin", 1, "Балансирана суха храна за пораснали кучета с пилешко месо и витамини.", "https://placehold.co/600x400?text=Dog+Food+1", "Пълноценна суха храна за кучета Adult Chicken", 42.90m },
                    { 2, "Purina Pro Plan", 1, "Специално разработена формула за кучета от малки породи.", "https://placehold.co/600x400?text=Dog+Food+2", "Суха храна за кучета Mini Adult", 36.50m },
                    { 3, "Brit Care", 1, "Подходяща за кучета с чувствителен стомах и специални хранителни нужди.", "https://placehold.co/600x400?text=Dog+Food+3", "Храна за кучета Sensitive Digestion", 39.80m },
                    { 4, "Pedigree", 1, "Вкусен пауч с телешко за ежедневно хранене на кучета.", "https://placehold.co/600x400?text=Dog+Food+4", "Пауч за кучета с телешко", 3.20m },
                    { 5, "Royal Canin", 2, "Пълноценна храна за домашни котки с контрол на теглото и космените топки.", "https://placehold.co/600x400?text=Cat+Food+1", "Суха храна за котки Indoor", 29.90m },
                    { 6, "Purina One", 2, "Подходяща за кастрирани котки, с вкус на сьомга.", "https://placehold.co/600x400?text=Cat+Food+2", "Храна за котки Sterilised Salmon", 21.40m },
                    { 7, "Whiskas", 2, "Мека и ароматна храна за котки с пиле в сочен сос.", "https://placehold.co/600x400?text=Cat+Food+3", "Пауч за котки с пиле в сос", 2.40m },
                    { 8, "N&D", 2, "Беззърнена формула с патешко месо за активни котки.", "https://placehold.co/600x400?text=Cat+Food+4", "Храна за котки Grain Free Duck", 34.70m },
                    { 9, "Pedigree", 3, "Дентални пръчици за поддържане на устната хигиена при кучета.", "https://placehold.co/600x400?text=Treats+1", "Лакомства за кучета Dental Sticks", 8.90m },
                    { 10, "Dreamies", 3, "Хрупкави отвън и меки отвътре лакомства за котки.", "https://placehold.co/600x400?text=Treats+2", "Меки лакомства за котки с риба", 4.60m },
                    { 11, "Brit", 3, "Натурални лакомства за награда по време на тренировки.", "https://placehold.co/600x400?text=Treats+3", "Натурални сушени хапки за кучета", 7.80m },
                    { 12, "Churu", 3, "Любимо течно лакомство за котки, подходящо и за капризни любимци.", "https://placehold.co/600x400?text=Treats+4", "Кремообразно лакомство за котки", 6.30m },
                    { 13, "Kong", 4, "Издръжлива играчка за кучета, подходяща за дъвчене и гонене.", "https://placehold.co/600x400?text=Toy+1", "Каучукова топка за кучета", 14.90m },
                    { 14, "Trixie", 4, "Играчка въже за активна игра и укрепване на зъбите.", "https://placehold.co/600x400?text=Toy+2", "Въже за дърпане", 9.50m },
                    { 15, "Ferplast", 4, "Забавна играчка за котки, стимулираща ловния инстинкт.", "https://placehold.co/600x400?text=Toy+3", "Интерактивна мишка за котки", 11.20m },
                    { 16, "Catit", 4, "Мека играчка за котки с котешка мента за допълнителен интерес.", "https://placehold.co/600x400?text=Toy+4", "Плюшена рибка с котешка мента", 7.40m },
                    { 17, "Beaphar", 5, "Нежен шампоан за кучета с чувствителна кожа.", "https://placehold.co/600x400?text=Care+1", "Шампоан за кучета с алое вера", 12.60m },
                    { 18, "Sanicat", 5, "Бучкуващ пясък за котки с добра абсорбация и контрол на миризмите.", "https://placehold.co/600x400?text=Care+2", "Пясък за котки Ultra Clumping", 16.30m },
                    { 19, "Trixie", 5, "Практични мокри кърпички за почистване на лапи след разходка.", "https://placehold.co/600x400?text=Care+3", "Мокри кърпички за лапи", 5.90m },
                    { 20, "Ferplast", 5, "Удобна четка за разресване на козина при кучета и котки.", "https://placehold.co/600x400?text=Care+4", "Четка за разресване", 13.70m },
                    { 21, "Trixie", 6, "Удобна каишка за ежедневни разходки с добро сцепление и здравина.", "https://placehold.co/600x400?text=Accessory+1", "Каишка за куче Comfort Walk", 18.90m },
                    { 22, "Savic", 6, "Стабилна и лесна за почистване купичка за вода или храна.", "https://placehold.co/600x400?text=Accessory+2", "Керамична купичка за храна", 10.80m },
                    { 23, "Ferplast", 6, "Меко и удобно легло за кучета с модерен дизайн.", "https://placehold.co/600x400?text=Accessory+3", "Легло за куче Cozy Bed", 49.90m },
                    { 24, "Camon", 6, "Лек и удобен нашийник за котка с регулируема дължина.", "https://placehold.co/600x400?text=Accessory+4", "Нашийник за котка с звънче", 8.20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
