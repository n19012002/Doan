using System;
using System.Collections.Generic;
using Doan.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Doan.Models;

public partial class HarmicContext : DbContext
{
    public HarmicContext()
    {
    }

    public HarmicContext(DbContextOptions<HarmicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public DbSet<AdminMenu> AdminMenus { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<EventCategory> EventCategories { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Slider> Sliders { get; set; }

    public virtual DbSet<TbAccount> TbAccounts { get; set; }

    public virtual DbSet<TbBlog> TbBlogs { get; set; }

    public virtual DbSet<TbBlogComment> TbBlogComments { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbContact> TbContacts { get; set; }

    public virtual DbSet<TbEvent> TbEvents { get; set; }

    public virtual DbSet<TbMenu> TbMenus { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbSubscribe> TbSubscribes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ASUSFX517\\SQLEXPRESS;Initial Catalog=Harmic;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.AboutId).HasName("PK__About__717FC95C74B8FA0D");

            entity.ToTable("About");

            entity.Property(e => e.AboutId).HasColumnName("AboutID");
            entity.Property(e => e.Contents).HasMaxLength(255);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Images).HasMaxLength(50);
            entity.Property(e => e.Links).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

       

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71877CF5DAD5");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Instructor).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<EventCategory>(entity =>
        {
            entity.ToTable("EventCategory");

            entity.Property(e => e.Alias).HasMaxLength(150);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__290C888465FC4DC6");

            entity.ToTable("Profile");

            entity.Property(e => e.ProfileId)
                .ValueGeneratedNever()
                .HasColumnName("ProfileID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.Account).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Profile_tb_Account");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA161E303D");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("ServiceID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Slider>(entity =>
        {
            entity.HasKey(e => e.Sliderid).HasName("PK__Sliders__3213E83FFC214AEC");

            entity.Property(e => e.Sliderid).HasColumnName("sliderid");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.Subtitle)
                .HasMaxLength(255)
                .HasColumnName("subtitle");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TbAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("tb_Account");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.TbAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tb_Account_tb_Role");
        });

        modelBuilder.Entity<TbBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK_tb_Post");

            entity.ToTable("tb_Blog");

            entity.Property(e => e.Alias).HasMaxLength(250);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Account).WithMany(p => p.TbBlogs)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tb_Post_tb_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.TbBlogs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tb_Post_tb_Category");
        });

        modelBuilder.Entity<TbBlogComment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("tb_BlogComment");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Blog).WithMany(p => p.TbBlogComments)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tb_BlogComment_tb_Blog");
        });

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_tb_Menu");

            entity.ToTable("tb_Category");

            entity.Property(e => e.Alias).HasMaxLength(150);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<TbContact>(entity =>
        {
            entity.HasKey(e => e.ContactId);

            entity.ToTable("tb_Contact");

            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<TbEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C8709C69EE67");

            entity.ToTable("tb_Event");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.Agenda).HasMaxLength(250);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationLink)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Speakers).HasMaxLength(250);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.EventCategory).WithMany(p => p.TbEvents)
                .HasForeignKey(d => d.EventCategoryId)
                .HasConstraintName("FK_Event_EventCategory");
        });

        modelBuilder.Entity<TbMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK_tb_Menu_1");

            entity.ToTable("tb_Menu");

            entity.Property(e => e.Alias).HasMaxLength(150);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role");

            entity.ToTable("tb_Role");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TbSubscribe>(entity =>
        {
            entity.HasKey(e => e.SubscribeId);

            entity.ToTable("tb_Subscribe");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
