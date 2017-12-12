using System.Data.Common;
using System.Data.Entity;
using Abp.Application.Editions;
using Abp.Organizations;
using Abp.Zero.EntityFramework;
using YT.Authorizations;
using YT.Managers.MultiTenancy;
using YT.Managers.Roles;
using YT.Managers.Users;
using YT.Models;
using YT.MultiTenancy;
using YT.Navigations;
using YT.Organizations;
using YT.Storage;

namespace YT.EntityFramework
{
    /* Constructors of this DbContext is important and each one has it's own use case.
     * - Default constructor is used by EF tooling on design time.
     * - constructor(nameOrConnectionString) is used by ABP on runtime.
     * - constructor(existingConnection) is used by unit tests.
     * - constructor(existingConnection,contextOwnsConnection) can be used by ABP if DbContextEfTransactionStrategy is used.
     * See http://www.aspnetboilerplate.com/Pages/Documents/EntityFramework-Integration for more.
     */
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MilkDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
       /// <summary>
       /// 对象存储
       /// </summary>
        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }
        /// <summary>
        /// 菜单配置
        /// </summary>
        public virtual IDbSet<Menu> Menus { get; set; }
        /// <summary>
        /// 权限配置
        /// </summary>
        public virtual IDbSet<YtPermission> YtPermissions { get; set; }
        /// <summary>
        /// 资质附件
        /// </summary>
        public  virtual  IDbSet<FormProfile> FormProfiles { get; set; }
        /// <summary>
        /// 公告管理
        /// </summary>
        public  virtual  IDbSet<Adsence> Adsences { get; set; }
        
        public new virtual IDbSet<Organization> OrganizationUnits { get; set; }

        public virtual IDbSet<Customer> Customers { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<Order> Orders { get; set; }
        public virtual IDbSet<ChargeRecord> ChargeRecords { get; set; }
        public virtual IDbSet<ApplyCharge> ApplyCharges { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<CustomerForm> CustomerForms { get; set; }

        public virtual  IDbSet<CustomerCost> CustomerCosts { get; set; }
        /// <summary>
        /// 客户定价
        /// </summary>
        public  virtual  IDbSet<CustomerPreferencePrice> CustomerPreferencePrices { get; set; }
        public MilkDbContext()
            : base("Default")
        {

        }

        public MilkDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public MilkDbContext(DbConnection existingConnection)
           : base(existingConnection, false)
        {

        }

        public MilkDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Ignore(a => a.Surname);
            modelBuilder.Entity<User>().Property(a => a.EmailAddress).IsOptional();
          
        }
    }
}
