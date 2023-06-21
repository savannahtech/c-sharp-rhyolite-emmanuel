using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial114365 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GpsLatitude",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GpsLongitude",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeaseId",
                table: "LeasePayments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LeaseApplicants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PrimaryPhoneNo = table.Column<string>(type: "text", nullable: true),
                    SecondaryPhoneNo = table.Column<string>(type: "text", nullable: true),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyName = table.Column<string>(type: "text", nullable: true),
                    PropertyUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyUnitName = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseApplicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalNotificationSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SendPaymentSmsAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendPaymentEmailAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendRentExpiryReminderSmsAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendRentExpiryReminderEmailAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendRentOnboardSmsAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendRentOnboardEmailAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendLeaseRenewalSmsAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendLeaseRenewalEmailAlert = table.Column<bool>(type: "boolean", nullable: false),
                    SendTenantBirthdaySmsAlert = table.Column<bool>(type: "boolean", nullable: false),
                    TenantBirthdaySmsAlertTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    SmsLowBalanceLimit = table.Column<decimal>(type: "numeric", nullable: false),
                    SendSmsLowBalanceLimitAlert = table.Column<decimal>(type: "numeric", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalNotificationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorIdentifier = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    VendorCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorCategoryName = table.Column<string>(type: "text", nullable: true),
                    ExpenseAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNo = table.Column<string>(type: "text", nullable: true),
                    PrimaryPhoneNo = table.Column<string>(type: "text", nullable: true),
                    SecondaryPhoneNo = table.Column<string>(type: "text", nullable: true),
                    PrimaryEmail = table.Column<string>(type: "text", nullable: true),
                    SecondaryEmail = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    RegionOrState = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    InsuranceProvider = table.Column<string>(type: "text", nullable: true),
                    PolicyNo = table.Column<string>(type: "text", nullable: true),
                    PolicyExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyName = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorName = table.Column<string>(type: "text", nullable: true),
                    InvoiceNo = table.Column<string>(type: "text", nullable: true),
                    WorkToBeDone = table.Column<string>(type: "text", nullable: true),
                    VendorNotes = table.Column<string>(type: "text", nullable: true),
                    AssignedToUserId = table.Column<long>(type: "bigint", nullable: false),
                    AssignedToUserName = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: true),
                    AttachmentFiles = table.Column<List<string>>(type: "jsonb", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaseApplicants");

            migrationBuilder.DropTable(
                name: "RentalNotificationSettings");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "VendorCategories");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "GpsLatitude",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "GpsLongitude",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LeaseId",
                table: "LeasePayments");
        }
    }
}
