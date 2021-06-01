﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TramsDataApi.DatabaseModels;

namespace TramsDataApi.Migrations.TramsDb
{
    [DbContext(typeof(TramsDbContext))]
    [Migration("20210525103557_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.AcademyTransferProjectUrns", "'AcademyTransferProjectUrns', '', '10000000', '1', '10000000', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TramsDataApi.DatabaseModels.AcademyTransferProjectIntendedTransferBenefits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FkAcademyTransferProjectId")
                        .HasColumnName("fk_AcademyTransferProjectId")
                        .HasColumnType("int");

                    b.Property<string>("SelectedBenefit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FkAcademyTransferProjectId");

                    b.ToTable("AcademyTransferProjectIntendedTransferBenefits","sdd");
                });

            modelBuilder.Entity("TramsDataApi.DatabaseModels.AcademyTransferProjects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ComplexLandAndBuildingFurtherSpecification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("ComplexLandAndBuildingShouldBeConsidered")
                        .HasColumnType("bit");

                    b.Property<string>("FinanceAndDebtFurtherSpecification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("FinanceAndDebtShouldBeConsidered")
                        .HasColumnType("bit");

                    b.Property<string>("HighProfileFurtherSpecification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("HighProfileShouldBeConsidered")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("HtbDate")
                        .HasColumnType("date");

                    b.Property<string>("OtherBenefitValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherTransferTypeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutgoingTrustUkprn")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<string>("ProjectRationale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("RddOrEsfaIntervention")
                        .HasColumnType("bit");

                    b.Property<string>("RddOrEsfaInterventionDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TargetDateForTransfer")
                        .HasColumnType("date");

                    b.Property<DateTime?>("TransferFirstDiscussed")
                        .HasColumnType("date");

                    b.Property<string>("TrustSponsorRationale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfTransfer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Urn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR AcademyTransferProjectUrns");

                    b.Property<string>("WhoInitiatedTheTransfer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK__AcademyT__C5B214360AF6201A");

                    b.HasIndex("Urn")
                        .HasName("AcademyTransferProjectUrn");

                    b.ToTable("AcademyTransferProjects","sdd");
                });

            modelBuilder.Entity("TramsDataApi.DatabaseModels.TransferringAcademies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FkAcademyTransferProjectId")
                        .HasColumnName("fk_AcademyTransferProjectId")
                        .HasColumnType("int");

                    b.Property<string>("IncomingTrustUkprn")
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<string>("OutgoingAcademyUkprn")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.HasKey("Id");

                    b.HasIndex("FkAcademyTransferProjectId");

                    b.ToTable("TransferringAcademies","sdd");
                });

            modelBuilder.Entity("TramsDataApi.DatabaseModels.AcademyTransferProjectIntendedTransferBenefits", b =>
                {
                    b.HasOne("TramsDataApi.DatabaseModels.AcademyTransferProjects", "FkAcademyTransferProject")
                        .WithMany("AcademyTransferProjectIntendedTransferBenefits")
                        .HasForeignKey("FkAcademyTransferProjectId")
                        .HasConstraintName("FK__AcademyTr__fk_Ac__4316F928");
                });

            modelBuilder.Entity("TramsDataApi.DatabaseModels.TransferringAcademies", b =>
                {
                    b.HasOne("TramsDataApi.DatabaseModels.AcademyTransferProjects", "FkAcademyTransferProject")
                        .WithMany("TransferringAcademies")
                        .HasForeignKey("FkAcademyTransferProjectId")
                        .HasConstraintName("FK__Transferr__fk_Ac__403A8C7D");
                });
#pragma warning restore 612, 618
        }
    }
}
