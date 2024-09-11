using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
	/// <inheritdoc />
	public partial class addprincat3 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{

			migrationBuilder.InsertData(
				table: "roles",
				columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
				values: new object[] { Guid.NewGuid().ToString(), "Customer", "Customer".ToUpper(), Guid.NewGuid().ToString() }
				);
			migrationBuilder.InsertData(
			   table: "roles",
			   columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
			   values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() }
			   );
			migrationBuilder.InsertData(
			   table: "roles",
			   columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
			   values: new object[] { Guid.NewGuid().ToString(), "Employee", "Employee".ToUpper(), Guid.NewGuid().ToString() }
			   );

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DELETE FROM [roles]");
		}
	}
}
