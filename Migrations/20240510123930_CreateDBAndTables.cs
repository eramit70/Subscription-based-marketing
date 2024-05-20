using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscription_based_marketing.Migrations
{
    /// <inheritdoc />
    public partial class CreateDBAndTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adminstrators",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminAccountCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminLastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminstrators", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "SellerAccounts",
                columns: table => new
                {
                    SellerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerPhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    SellerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerAccountCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerLastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerAccounts", x => x.SellerID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserLastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetails",
                columns: table => new
                {
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDuration = table.Column<int>(type: "int", nullable: false),
                    ServiceFeature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceTermsCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceLevel = table.Column<int>(type: "int", nullable: false),
                    ServiceIsPublish = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetails", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_ServiceDetails_SellerAccounts_SellerID",
                        column: x => x.SellerID,
                        principalTable: "SellerAccounts",
                        principalColumn: "SellerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAccessControl",
                columns: table => new
                {
                    ServiceAccessControlID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    serviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAccessControl", x => x.ServiceAccessControlID);
                    table.ForeignKey(
                        name: "FK_ServiceAccessControl_SellerAccounts_SellerID",
                        column: x => x.SellerID,
                        principalTable: "SellerAccounts",
                        principalColumn: "SellerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAccessControl_ServiceDetails_serviceID",
                        column: x => x.serviceID,
                        principalTable: "ServiceDetails",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAccessControl_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionDetails",
                columns: table => new
                {
                    SubscriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    serviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionBillingFrequency = table.Column<int>(type: "int", nullable: false),
                    SubscriptionNextBillingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDetails", x => x.SubscriptionID);
                    table.ForeignKey(
                        name: "FK_SubscriptionDetails_ServiceDetails_serviceID",
                        column: x => x.serviceID,
                        principalTable: "ServiceDetails",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionDetails_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAccessControl_SellerID",
                table: "ServiceAccessControl",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAccessControl_serviceID",
                table: "ServiceAccessControl",
                column: "serviceID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAccessControl_UserId",
                table: "ServiceAccessControl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetails_SellerID",
                table: "ServiceDetails",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDetails_serviceID",
                table: "SubscriptionDetails",
                column: "serviceID");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDetails_UserID",
                table: "SubscriptionDetails",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminstrators");

            migrationBuilder.DropTable(
                name: "ServiceAccessControl");

            migrationBuilder.DropTable(
                name: "SubscriptionDetails");

            migrationBuilder.DropTable(
                name: "ServiceDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SellerAccounts");
        }
    }
}
