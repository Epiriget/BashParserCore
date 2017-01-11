using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BashParserCore.Data;

namespace BashParserCore.Migrations
{
    [DbContext(typeof(BashContext))]
    [Migration("20170109052449_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BashParserCore.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PostID");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("PostID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BashParserCore.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Date");

                    b.Property<string>("PostName");

                    b.Property<string>("Rating");

                    b.Property<string>("Text");

                    b.HasKey("PostID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BashParserCore.Models.Comment", b =>
                {
                    b.HasOne("BashParserCore.Models.Post", "post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID");
                });
        }
    }
}
