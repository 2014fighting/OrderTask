using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OrderTask.Model.DbModel;
using OrderTask.Model.DbModel.BisnessModel;

namespace OrderTask.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 参考文档 https://docs.microsoft.com/en-us/ef/core/modeling/relationships
    /// </summary>
    public class OrderTaskContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<RoleInfo> RoleInfo { get; set; }

        public DbSet<Table> Table { get; set; }

        public DbSet<SubSystem> SubSystem { get; set; }
        public DbSet<RolePower> RolePower { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<DepartMent> DepartMent { get; set; }
        public DbSet<Button> Button { get; set; }
        public DbSet<Column> Column { get; set; }


        public DbSet<DataManage> DataManage { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLog> OrderLog { get; set; }
        public DbSet<OrderType> OrderType { get; set; }
        public DbSet<ReceivePerson> ReceivePerson { get; set; }


        public OrderTaskContext(DbContextOptions<OrderTaskContext> options) : base(options)
        {
        }
        //private string _connection;

        //public CodeFrameContext(string connection) => this._connection = connection;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!string.IsNullOrWhiteSpace(_connection))
        //        optionsBuilder.UseMySql(_connection);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(t => new { t.UserId, t.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.UserInfo)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.RoleInfo)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.RoleId);

            modelBuilder.Entity<UserInfo>().HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<UserInfo>().HasIndex(u => u.TrueName).IsUnique();

        
            //modelBuilder.Entity<UserInfo>()
            //    .HasOne(p => p.CreateUserInfo)
            //    .WithOne().HasForeignKey("CreateUserId");

            //modelBuilder.Entity<UserInfo>()
            //    .HasOne(p => p.UpdateUser)
            //    .WithOne().HasForeignKey("UpdateUserId");



        }
    }

}
