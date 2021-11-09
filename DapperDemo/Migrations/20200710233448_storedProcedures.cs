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
	                @Description  varchar(MAX)
                AS
                BEGIN 
                    INSERT INTO Categories (Name, Description) VALUES(@Name, @Description);
	                SELECT @CategoryId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_UpdateCategory
	                @CategoryId int,
                    @Name varchar(MAX),
	                @Description  varchar(MAX)
                AS
                BEGIN 
                    UPDATE Categories  
	                SET 
		                Name = @Name, 
		                Description = @Description
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
