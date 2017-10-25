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
       /// ����洢
       /// </summary>
        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }
        /// <summary>
        /// �˵�����
        /// </summary>
        public virtual IDbSet<Menu> Menus { get; set; }
        /// <summary>
        /// Ȩ������
        /// </summary>
        public virtual IDbSet<YtPermission> YtPermissions { get; set; }

        public new virtual IDbSet<Organization> OrganizationUnits { get; set; }

        /// <summary>
        /// ��Ƭ
        /// </summary>
        public virtual IDbSet<Card> Cards { get; set; }
        /// <summary>
        /// �ͻ�
        /// </summary>
        public virtual IDbSet<Customer> Customers { get; set; }
        /// <summary>
        /// �ƹ�Ա 
        /// </summary>
        public virtual IDbSet<Promoter> Promoters { get; set; }
        /// <summary>
        /// ���ʿ�
        /// </summary>
        public  virtual  IDbSet<SpecialCard> SpecialCards { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public  virtual  IDbSet<Area> Areas { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public virtual IDbSet<Order> Order { get; set; }
        /// <summary>
        /// ������ϸ
        /// </summary>
        public  virtual  IDbSet<OrderItem> OrderItem { get; set; }
        /// <summary>
        /// ��ֵ��¼
        /// </summary>
        public  virtual  IDbSet<ChargeRecord> ChargeRecord { get; set; }
        /// <summary>
        /// Ⱥ����¼
        /// </summary>
        public  virtual  IDbSet<WeChatRecord> WeChatRecord { get; set; }
        /// <summary>
        /// ���ܼ�¼
        /// </summary>
        public  virtual  IDbSet<Straw> Straw { get; set; }
        /// <summary>
        /// �ֻ���֤��
        /// </summary>
        public  virtual  IDbSet<VilidateCode> VilidateCode { get; set; }
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
