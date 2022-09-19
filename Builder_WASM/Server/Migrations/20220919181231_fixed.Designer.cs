﻿// <auto-generated />
using System;
using Builder_WASM.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Builder_WASM.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220919181231_fixed")]
    partial class @fixed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Builder_WASM.Shared.Entities.ClientJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AddressBillCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressBillCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressBillPostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressBillProvince")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressBillStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressSiteCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressSiteCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressSitePostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressSiteProvince")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressSiteStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateRegistration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimaryLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specify")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("ClientJobs");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ContractPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GST")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GSTpercent")
                        .HasColumnType("int");

                    b.Property<string>("HeaderAdditional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderCompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderPost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderWebSite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Incorporation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Liability")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Licence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PST")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TAXpercent")
                        .HasColumnType("int");

                    b.Property<string>("WCB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContractPath = "",
                            GST = "",
                            GSTpercent = 5,
                            HeaderAdditional = "",
                            HeaderAddress = "",
                            HeaderCompanyName = "CMO full name",
                            HeaderEmail = "",
                            HeaderName = "CMO",
                            HeaderPhone = "",
                            HeaderPost = "",
                            HeaderWebSite = "",
                            Incorporation = "",
                            Liability = "",
                            Licence = "",
                            LogoPath = "",
                            PST = "",
                            TAXpercent = 12,
                            WCB = ""
                        },
                        new
                        {
                            Id = 2,
                            ContractPath = "",
                            GST = "",
                            GSTpercent = 5,
                            HeaderAdditional = "",
                            HeaderAddress = "",
                            HeaderCompanyName = "NL full name",
                            HeaderEmail = "",
                            HeaderName = "NL",
                            HeaderPhone = "",
                            HeaderPost = "",
                            HeaderWebSite = "",
                            Incorporation = "",
                            Liability = "",
                            Licence = "",
                            LogoPath = "",
                            PST = "",
                            TAXpercent = 12,
                            WCB = ""
                        },
                        new
                        {
                            Id = 3,
                            ContractPath = "",
                            GST = "",
                            GSTpercent = 5,
                            HeaderAdditional = "",
                            HeaderAddress = "",
                            HeaderCompanyName = "Test Company full name",
                            HeaderEmail = "",
                            HeaderName = "Test Company",
                            HeaderPhone = "",
                            HeaderPost = "",
                            HeaderWebSite = "",
                            Incorporation = "",
                            Liability = "",
                            Licence = "",
                            LogoPath = "",
                            PST = "",
                            TAXpercent = 12,
                            WCB = ""
                        });
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DContractor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("DContractors");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DItemId")
                        .HasColumnType("int");

                    b.Property<string>("NameDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RateDescription")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DItemId");

                    b.ToTable("DDescriptions");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DGroupe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("NameGroupe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("DGroupes");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DGroupeId")
                        .HasColumnType("int");

                    b.Property<int?>("DSupplierId")
                        .HasColumnType("int");

                    b.Property<string>("NameItem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DGroupeId");

                    b.HasIndex("DSupplierId");

                    b.ToTable("DItems");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DMethodPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("NameMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PercentMethod")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("DMethodPayments");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DSupplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AddressSupplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("NameSupplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("DSuppliers");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Estimate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AmountPaidByCreditCard")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClientJobId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEstimate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("FinancingUser")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("FinancingYesNo")
                        .HasColumnType("bit");

                    b.Property<int>("GSTpercent")
                        .HasColumnType("int");

                    b.Property<decimal>("LabourDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LabourSubtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaterialDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaterialSubtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TAXpercent")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPayment")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClientJobId");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.EstimateLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstimateId")
                        .HasColumnType("int");

                    b.Property<string>("Groupe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstimateId");

                    b.ToTable("EstimateLines");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AmountPayment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DatePayment")
                        .HasColumnType("datetime2");

                    b.Property<int>("EstimateId")
                        .HasColumnType("int");

                    b.Property<string>("MethodPayment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PercentMethodPayment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstimateId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.UserMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReadByAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("ReadByUser")
                        .HasColumnType("bit");

                    b.Property<int>("UserRegisteredId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRegisteredId");

                    b.ToTable("UserMessages");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.UserRegistered", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("UserRegistereds");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            Password = "123",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User",
                            Password = "123",
                            Role = "User"
                        });
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.ClientJob", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Company", "Company")
                        .WithMany("ClientJobs")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DContractor", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Company", "Company")
                        .WithMany("DContractors")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DDescription", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Dictionary.DItem", "DItem")
                        .WithMany("DDescriptions")
                        .HasForeignKey("DItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DItem");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DGroupe", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Company", "Company")
                        .WithMany("DGroupes")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DItem", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Dictionary.DGroupe", "DGroupe")
                        .WithMany("DItems")
                        .HasForeignKey("DGroupeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Builder_WASM.Shared.Entities.Dictionary.DSupplier", "DSupplier")
                        .WithMany("DItems")
                        .HasForeignKey("DSupplierId");

                    b.Navigation("DGroupe");

                    b.Navigation("DSupplier");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DMethodPayment", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Company", "Company")
                        .WithMany("DMethodPayments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DSupplier", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Company", "Company")
                        .WithMany("DSuppliers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Estimate", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.ClientJob", "ClientJob")
                        .WithMany("Estimates")
                        .HasForeignKey("ClientJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientJob");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.EstimateLine", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Estimate", "Estimate")
                        .WithMany("EstimateLines")
                        .HasForeignKey("EstimateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estimate");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Payment", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Estimate", "Estimate")
                        .WithMany("Payments")
                        .HasForeignKey("EstimateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estimate");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.UserMessage", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.UserRegistered", "UserRegistered")
                        .WithMany("Messages")
                        .HasForeignKey("UserRegisteredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRegistered");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.UserRegistered", b =>
                {
                    b.HasOne("Builder_WASM.Shared.Entities.Company", "Company")
                        .WithMany("UserRegistered")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.ClientJob", b =>
                {
                    b.Navigation("Estimates");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Company", b =>
                {
                    b.Navigation("ClientJobs");

                    b.Navigation("DContractors");

                    b.Navigation("DGroupes");

                    b.Navigation("DMethodPayments");

                    b.Navigation("DSuppliers");

                    b.Navigation("UserRegistered");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DGroupe", b =>
                {
                    b.Navigation("DItems");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DItem", b =>
                {
                    b.Navigation("DDescriptions");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Dictionary.DSupplier", b =>
                {
                    b.Navigation("DItems");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.Estimate", b =>
                {
                    b.Navigation("EstimateLines");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Builder_WASM.Shared.Entities.UserRegistered", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
