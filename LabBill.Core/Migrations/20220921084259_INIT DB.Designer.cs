// <auto-generated />
using System;
using LabBill.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LabBill.Core.Migrations
{
    [DbContext(typeof(BillContext))]
    [Migration("20220921084259_INIT DB")]
    partial class INITDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7");

            modelBuilder.Entity("LabBill.Core.Domains.AssetInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AssetLink")
                        .HasColumnType("TEXT");

                    b.Property<int?>("BillId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("AssetInfo");
                });

            modelBuilder.Entity("LabBill.Core.Domains.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BillState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BillType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brief")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("LabBill.Core.Domains.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("LabBill.Core.Domains.AssetInfo", b =>
                {
                    b.HasOne("LabBill.Core.Domains.Bill", null)
                        .WithMany("AssetInfos")
                        .HasForeignKey("BillId");
                });

            modelBuilder.Entity("LabBill.Core.Domains.Bill", b =>
                {
                    b.HasOne("LabBill.Core.Domains.Person", "Person")
                        .WithMany("Bills")
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("LabBill.Core.Domains.Bill", b =>
                {
                    b.Navigation("AssetInfos");
                });

            modelBuilder.Entity("LabBill.Core.Domains.Person", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
