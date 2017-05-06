using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IdentityServer4PersistanceStorageSample.Data.Contexts;

namespace IdentityServer4PersistanceStorageSample.Data.Migrations.IdentityServer.CustomUserDb
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20170506172955_InitialIdentityServerCustomDbMigration")]
    partial class InitialIdentityServerCustomDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdentityServer4PersistanceStorageSample.Data.Entities.CustomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("Provider")
                        .IsRequired();

                    b.Property<string>("SubjectId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PublicUser_CustomUser");
                });
        }
    }
}
