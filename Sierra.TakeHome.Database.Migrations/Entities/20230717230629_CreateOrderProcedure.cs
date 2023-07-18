using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sierra.TakeHome.Database.Migrations.Entities {
    
    /// <summary>
    /// 
    /// </summary>
    public partial class CreateOrderProcedure : Migration {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql($@"
                CREATE PROCEDURE [dbo].[AddNewOrder]
                    @productId uniqueidentifier,
                    @customerId uniqueidentifier,
                    @quantity smallint = 0
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @orderId UNIQUEIDENTIFIER = NEWID();
                    DECLARE @productPrice DECIMAL(5,2) = (SELECT Price FROM {Constants.ProductTableName} WHERE Id = @productId)
                    DECLARE @total DECIMAL(5,2) = @productPrice * @quantity
                    
                    INSERT INTO {Constants.OrdersTableName} VALUES (@orderId, @productId, @customerId, @quantity, @total)
                    SELECT * FROM {Constants.OrdersTableName} WHERE Id = @orderId
                END
                GO
            ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql($@"
                DROP PROCEDURE AddNewOrder
                GO
            ");
        }
    }
}