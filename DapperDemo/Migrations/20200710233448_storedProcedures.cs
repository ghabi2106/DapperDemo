using Microsoft.EntityFrameworkCore.Migrations;

namespace DapperDemo.Migrations
{
    public partial class storedProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROC usp_GetCategory
                    @CategoryId int
                AS 
                BEGIN 
                    SELECT *
                    FROM Categories
                    WHERE CategoryId = @CategoryId
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_GetALLCategory
                AS 
                BEGIN 
                    SELECT *
                    FROM Categories
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_AddCategory
                    @CategoryId int OUTPUT,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN 
                    INSERT INTO Categories (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);
	                SELECT @CategoryId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_UpdateCategory
	                @CategoryId int,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN 
                    UPDATE Categories  
	                SET 
		                Name = @Name, 
		                Address = @Address,
		                City=@City, 
		                State=@State, 
		                PostalCode=@PostalCode
	                WHERE CategoryId=@CategoryId;
	                SELECT @CategoryId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_RemoveCategory
                    @CategoryId int
                AS 
                BEGIN 
                    DELETE
                    FROM Categories
                    WHERE CategoryId  = @CategoryId
                END
                GO	
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
