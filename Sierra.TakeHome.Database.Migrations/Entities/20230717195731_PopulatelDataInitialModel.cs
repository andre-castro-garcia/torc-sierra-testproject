using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sierra.TakeHome.Database.Migrations.Entities {
    /// <summary>
    /// 
    /// </summary>
    public partial class PopulatelDataInitialModel : Migration {
        
        /// <summary>
        /// 
        /// </summary>
        private static readonly List<Guid> ProductsIds = new() {
            Guid.Parse("bd2a76b3-22fb-4400-8990-eb6d0e0217e2"),
            Guid.Parse("7e4cb59d-449c-45a1-a6c6-d769aa9f4b41"),
            Guid.Parse("c9e1d2a9-a178-4aa0-8b67-76e90fc75121"),
            Guid.Parse("c01acff0-d149-471a-bc82-f5a531a17d64"),
            Guid.Parse("1020fce0-aae5-4885-8c88-38901ebc2a8f"),
            Guid.Parse("cad4aae7-f804-462b-9308-3de8e0d70b9a"),
            Guid.Parse("d35ee195-daa1-4d9a-a8b1-23f52bad22d5"),
            Guid.Parse("6d7b62b2-be07-46c4-a715-9d8896c31b2d"),
            Guid.Parse("d879a539-193f-48a9-9c69-e7468884a297"),
            Guid.Parse("467fc4b8-ac15-456e-b016-0d1634743e60")
        };
        
        /// <summary>
        /// 
        /// </summary>
        private readonly List<Tuple<Guid, string, double>> _products = new() {
            new Tuple<Guid, string, double>(ProductsIds[0], "Product A", 6.76),
            new Tuple<Guid, string, double>(ProductsIds[1], "Product B", 4.56),
            new Tuple<Guid, string, double>(ProductsIds[2], "Product C", 0.15),
            new Tuple<Guid, string, double>(ProductsIds[3], "Product D", 1.34),
            new Tuple<Guid, string, double>(ProductsIds[4], "Product E", 5.67),
            new Tuple<Guid, string, double>(ProductsIds[5], "Product F", 2.98),
            new Tuple<Guid, string, double>(ProductsIds[6], "Product G", 0.67),
            new Tuple<Guid, string, double>(ProductsIds[7], "Product H", 8.54),
            new Tuple<Guid, string, double>(ProductsIds[8], "Product I", 8.63),
            new Tuple<Guid, string, double>(ProductsIds[9], "Product J", 0.07)
        };


        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder) {
            foreach (var product in _products) {
                migrationBuilder.Sql($@"
                    INSERT INTO {Constants.ProductTableName} 
                    VALUES ('{product.Item1}', '{product.Item2}', {product.Item3})
                ");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder) {
            foreach (var productId in ProductsIds) {
                migrationBuilder.Sql($@"
                    DELETE FROM {Constants.ProductTableName} 
                    WHERE Id = '{productId}'
                ");
            }
        }
    }
}