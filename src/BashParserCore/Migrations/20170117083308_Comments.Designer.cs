using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BashParserCore.Data;

namespace BashParserCore.Migrations
{
    [DbContext(typeof(BashContext))]
    [Migration("20170117083308_Comments")]
    partial class Comments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BashParserCore.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CommentId");

                    b.Property<int?>("ParentID");

                    b.Property<int?>("PostId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BashParserCore.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Date");

                    b.Property<string>("PostName");

                    b.Property<string>("Rating");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BashParserCore.Models.Comment", b =>
                {
                    b.HasOne("BashParserCore.Models.Comment")
                        .WithMany("embeddedComments")
                        .HasForeignKey("CommentId");

                    b.HasOne("BashParserCore.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");
                });
        }
    }
}
