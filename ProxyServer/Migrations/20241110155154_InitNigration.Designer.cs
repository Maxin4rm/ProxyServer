﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProxyServer.Data;

#nullable disable

namespace ProxyServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241110155154_InitNigration")]
    partial class InitNigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProxyServer.Models.AccessProperty", b =>
                {
                    b.Property<int>("AccessPropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AccessPropertyId"));

                    b.Property<int>("AccessCount")
                        .HasColumnType("integer");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AccessPropertyId");

                    b.ToTable("AccessProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
