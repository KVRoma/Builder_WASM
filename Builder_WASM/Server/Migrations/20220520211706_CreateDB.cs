using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Builder_WASM.Server.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderAdditional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderWebSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PST = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WCB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Liability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Licence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Incorporation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TAXpercent = table.Column<int>(type: "int", nullable: false),
                    GSTpercent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimaryLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressBillStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressBillCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressBillProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressBillPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressBillCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressSiteStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressSiteCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressSiteProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressSitePostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressSiteCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specify = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientJobs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DContractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DContractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DContractors_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DGroupes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGroupe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DGroupes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DGroupes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DMethodPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PercentMethod = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NameMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMethodPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMethodPayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSupplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressSupplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DSuppliers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRegistereds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegistereds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRegistereds_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Estimates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEstimate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialSubtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TAXpercent = table.Column<int>(type: "int", nullable: false),
                    MaterialDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabourSubtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GSTpercent = table.Column<int>(type: "int", nullable: false),
                    LabourDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinancingYesNo = table.Column<bool>(type: "bit", nullable: false),
                    FinancingUser = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaidByCreditCard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientJobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estimates_ClientJobs_ClientJobId",
                        column: x => x.ClientJobId,
                        principalTable: "ClientJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DGroupeId = table.Column<int>(type: "int", nullable: false),
                    DSapplierId = table.Column<int>(type: "int", nullable: true),
                    DSupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DItems_DGroupes_DGroupeId",
                        column: x => x.DGroupeId,
                        principalTable: "DGroupes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DItems_DSuppliers_DSupplierId",
                        column: x => x.DSupplierId,
                        principalTable: "DSuppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EstimateLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Groupe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    EstimateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimateLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstimateLines_Estimates_EstimateId",
                        column: x => x.EstimateId,
                        principalTable: "Estimates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MethodPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentMethodPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Estimates_EstimateId",
                        column: x => x.EstimateId,
                        principalTable: "Estimates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RateDescription = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DDescriptions_DItems_DItemId",
                        column: x => x.DItemId,
                        principalTable: "DItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "ContractPath", "GST", "GSTpercent", "HeaderAdditional", "HeaderAddress", "HeaderCompanyName", "HeaderEmail", "HeaderName", "HeaderPhone", "HeaderPost", "HeaderWebSite", "Incorporation", "Liability", "Licence", "LogoPath", "PST", "TAXpercent", "WCB" },
                values: new object[,]
                {
                    { 1, "", "", 5, "", "", "", "", "CMO", "", "", "", "", "", "", "", "", 12, "" },
                    { 2, "", "", 5, "", "", "", "", "NL", "", "", "", "", "", "", "", "", 12, "" },
                    { 3, "", "", 5, "", "", "", "", "Test Company", "", "", "", "", "", "", "", "", 12, "" }
                });

            migrationBuilder.InsertData(
                table: "UserRegistereds",
                columns: new[] { "Id", "CompanyId", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, null, "Test", "123", "Admin" },
                    { 2, null, "Test2", "123", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientJobs_CompanyId",
                table: "ClientJobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DContractors_CompanyId",
                table: "DContractors",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DDescriptions_DItemId",
                table: "DDescriptions",
                column: "DItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DGroupes_CompanyId",
                table: "DGroupes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DItems_DGroupeId",
                table: "DItems",
                column: "DGroupeId");

            migrationBuilder.CreateIndex(
                name: "IX_DItems_DSupplierId",
                table: "DItems",
                column: "DSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DMethodPayments_CompanyId",
                table: "DMethodPayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DSuppliers_CompanyId",
                table: "DSuppliers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateLines_EstimateId",
                table: "EstimateLines",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_ClientJobId",
                table: "Estimates",
                column: "ClientJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EstimateId",
                table: "Payments",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegistereds_CompanyId",
                table: "UserRegistereds",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DContractors");

            migrationBuilder.DropTable(
                name: "DDescriptions");

            migrationBuilder.DropTable(
                name: "DMethodPayments");

            migrationBuilder.DropTable(
                name: "EstimateLines");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "UserRegistereds");

            migrationBuilder.DropTable(
                name: "DItems");

            migrationBuilder.DropTable(
                name: "Estimates");

            migrationBuilder.DropTable(
                name: "DGroupes");

            migrationBuilder.DropTable(
                name: "DSuppliers");

            migrationBuilder.DropTable(
                name: "ClientJobs");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
