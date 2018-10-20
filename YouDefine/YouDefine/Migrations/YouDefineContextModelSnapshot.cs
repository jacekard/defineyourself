﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YouDefine.Data;

namespace YouDefine.Migrations
{
    [DbContext(typeof(YouDefineContext))]
    partial class YouDefineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YouDefine.Models.Bug", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Information");

                    b.Property<bool>("IsFixed");

                    b.Property<DateTime>("ReportDate");

                    b.HasKey("Id");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("YouDefine.Models.Definition", b =>
                {
                    b.Property<long>("DefinitionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<long>("IdeaId");

                    b.Property<int>("Likes");

                    b.Property<string>("Text");

                    b.HasKey("DefinitionId");

                    b.HasIndex("IdeaId");

                    b.ToTable("Definitions");
                });

            modelBuilder.Entity("YouDefine.Models.Idea", b =>
                {
                    b.Property<long>("IdeaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("Likes");

                    b.Property<string>("Title");

                    b.HasKey("IdeaId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("YouDefine.Models.Definition", b =>
                {
                    b.HasOne("YouDefine.Models.Idea")
                        .WithMany("Definitions")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
