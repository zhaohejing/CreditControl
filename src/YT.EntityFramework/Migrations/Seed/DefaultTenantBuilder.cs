using System.Linq;
using Abp.Configuration;
using Abp.Net.Mail;
using YT.Editions;
using YT.EntityFramework;
using YT.Managers.MultiTenancy;

namespace YT.Migrations.Seed
{
    public class DefaultTenantBuilder
    {
        private readonly MilkDbContext _context;

        public DefaultTenantBuilder(MilkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
         
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new Tenant(Tenant.DefaultTenantName,
                    Tenant.DefaultTenantName);

                var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
            //邮件发送管理
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "454258314@qq.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "华夏");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Port, "587");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Host, "smtp.qq.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UserName, "454258314@qq.com");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Password, "vvkqelenlrambhgj");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Domain, "");
            AddSettingIfNotExists(EmailSettingNames.Smtp.EnableSsl, "true");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UseDefaultCredentials, "false");
        }
        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
