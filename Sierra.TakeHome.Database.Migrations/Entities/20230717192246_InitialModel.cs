using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sierra.TakeHome.Database.Migrations.Entities {
    
    /// <summary>
    /// 
    /// </summary>
    public partial class InitialModel : Migration {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: Constants.ProductTableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(type: "decimal(5,2)", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey($"PK_{Constants.ProductTableName}", x => new { x.Id });
                });
            
            migrationBuilder.CreateTable(
                name: Constants.OrdersTableName,
                columns: table => new
                {
                    Id = table.Column<int>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Total = table.Column<double>(type: "decimal(5,2)", nullable: false, defaultValue: 0d),
                },
                constraints: table =>
                {
                    table.PrimaryKey($"PK_{Constants.OrdersTableName}", x => new { x.Id });
                });
            
            migrationBuilder.AddForeignKey(
                name: $"FK_{Constants.OrdersTableName}_{Constants.ProductTableName}",
                table: Constants.OrdersTableName,
                column: "ProductId",
                principalTable: Constants.ProductTableName,
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(Constants.ProductTableName);
            migrationBuilder.DropTable(Constants.OrdersTableName);
        }
    }
}