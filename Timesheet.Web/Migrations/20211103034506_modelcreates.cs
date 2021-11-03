using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.Web.Migrations
{
    public partial class modelcreates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: false),
                    BillingAddress = table.Column<string>(nullable: false),
                    ContactName = table.Column<string>(nullable: false),
                    ContactTelephone = table.Column<string>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(nullable: false),
                    Forename = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    TimesheetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinutesWorked = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.TimesheetId);
                    table.ForeignKey(
                        name: "FK_Timesheets_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Timesheets_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Timesheets_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "BillingAddress", "CompanyName", "ContactEmail", "ContactName", "ContactTelephone" },
                values: new object[,]
                {
                    { 1, "3 Hopeless Lane, Fiddler District, Middlesbrough", "Fiddler Fingers", "fw@example.com", "Free Willy", "01234 121212" },
                    { 2, "42 Cloud Lane, Cloud District, Middlesbrough", "Pie in the Sky", "sally@example.com", "Sally Pie", "01234 341245" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "4 Wonky Way, Middlesbrough", "Willy's Hovel" },
                    { 2, "3 Hopeless Lane, Fiddler District, Middlesbrough", "Fiddler Fingers Head Office" },
                    { 3, "13 Stack It Road, Storage District, Middlesbrough", "Fiddler Fingers Warehouse" },
                    { 4, "1 Crust Avenue, Busy District, Middlesbrough", "Sally's Place" },
                    { 5, "3 Crust Avenue, Busy District, Middlesbrough", "Sally's Takeout" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "Email", "Forename", "Surname" },
                values: new object[,]
                {
                    { 1, "tyrone.davison@example.com", "Tyrone", "Davison" },
                    { 2, "zafar.khan@example.com", "Zafar", "Khan" },
                    { 3, "james.fairbairn@example.com", "James", "Fairbairn" }
                });

            migrationBuilder.InsertData(
                table: "Timesheets",
                columns: new[] { "TimesheetId", "ClientId", "LocationId", "MinutesWorked", "StaffId" },
                values: new object[] { 1, 2, 4, 80, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_ClientId",
                table: "Timesheets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_LocationId",
                table: "Timesheets",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_StaffId",
                table: "Timesheets",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
