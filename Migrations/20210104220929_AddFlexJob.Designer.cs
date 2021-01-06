﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebAdminPanel.Models;

namespace WebAdminPanel.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210104220929_AddFlexJob")]
    partial class AddFlexJob
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("WebAdminPanel.Models.Base.AccountBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountBase");
                });

            modelBuilder.Entity("WebAdminPanel.Models.Base.JobBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CoverLetter")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Jobs");

                    b.HasDiscriminator<string>("Discriminator").HasValue("JobBase");
                });

            modelBuilder.Entity("WebAdminPanel.Models.Base.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Sites");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Site");
                });

            modelBuilder.Entity("WebAdminPanel.Models.BotSignal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("BotTypeSignal")
                        .HasColumnType("integer");

                    b.Property<string>("CoverrLetter")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IgnoreAlreadySended")
                        .HasColumnType("boolean");

                    b.Property<string>("JobLinks")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BotSignals");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BotSignal");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.AccountFlexJob", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.Base.AccountBase");

                    b.Property<int>("SiteId")
                        .HasColumnType("integer");

                    b.HasIndex("SiteId");

                    b.HasDiscriminator().HasValue("AccountFlexJob");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.AccountPowerToFly", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.Base.AccountBase");

                    b.Property<int>("SiteId")
                        .HasColumnType("integer")
                        .HasColumnName("AccountPowerToFly_SiteId");

                    b.HasIndex("SiteId");

                    b.HasDiscriminator().HasValue("AccountPowerToFly");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.JobFlexJob", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.Base.JobBase");

                    b.Property<int>("SignalId")
                        .HasColumnType("integer");

                    b.HasIndex("SignalId");

                    b.HasDiscriminator().HasValue("JobFlexJob");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.JobPowerToFly", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.Base.JobBase");

                    b.Property<int>("SignalId")
                        .HasColumnType("integer")
                        .HasColumnName("JobPowerToFly_SignalId");

                    b.HasIndex("SignalId");

                    b.HasDiscriminator().HasValue("JobPowerToFly");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.SiteFlexJob", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.Base.Site");

                    b.HasDiscriminator().HasValue("SiteFlexJob");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.SitePowerToFly", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.Base.Site");

                    b.HasDiscriminator().HasValue("SitePowerToFly");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.BotSignalFlexJob", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.BotSignal");

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.HasIndex("AccountId");

                    b.HasDiscriminator().HasValue("BotSignalFlexJob");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.BotSignalPowerToFly", b =>
                {
                    b.HasBaseType("WebAdminPanel.Models.BotSignal");

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("BotSignalPowerToFly_AccountId");

                    b.HasIndex("AccountId");

                    b.HasDiscriminator().HasValue("BotSignalPowerToFly");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.AccountFlexJob", b =>
                {
                    b.HasOne("WebAdminPanel.Models.FlexJob.SiteFlexJob", "Site")
                        .WithMany("Accounts")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.AccountPowerToFly", b =>
                {
                    b.HasOne("WebAdminPanel.Models.PowerToFly.SitePowerToFly", "Site")
                        .WithMany("Accounts")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.JobFlexJob", b =>
                {
                    b.HasOne("WebAdminPanel.Models.FlexJob.BotSignalFlexJob", "Signal")
                        .WithMany("Jobs")
                        .HasForeignKey("SignalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Signal");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.JobPowerToFly", b =>
                {
                    b.HasOne("WebAdminPanel.Models.PowerToFly.BotSignalPowerToFly", "Signal")
                        .WithMany("Jobs")
                        .HasForeignKey("SignalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Signal");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.BotSignalFlexJob", b =>
                {
                    b.HasOne("WebAdminPanel.Models.FlexJob.AccountFlexJob", "Account")
                        .WithMany("Signals")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.BotSignalPowerToFly", b =>
                {
                    b.HasOne("WebAdminPanel.Models.PowerToFly.AccountPowerToFly", "Account")
                        .WithMany("Signals")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.AccountFlexJob", b =>
                {
                    b.Navigation("Signals");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.AccountPowerToFly", b =>
                {
                    b.Navigation("Signals");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.SiteFlexJob", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.SitePowerToFly", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("WebAdminPanel.Models.FlexJob.BotSignalFlexJob", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("WebAdminPanel.Models.PowerToFly.BotSignalPowerToFly", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
