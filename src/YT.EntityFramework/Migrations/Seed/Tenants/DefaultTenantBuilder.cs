using System.Linq;
using Abp.Configuration;
using YT.Configuration;
using YT.Editions;
using YT.EntityFramework;
using YT.Managers.MultiTenancy;

namespace YT.Migrations.Seed.Tenants
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
            //ÅäÖÃÏî
          //  AddSettingIfNotExists(YtSettings.General.MenuDefaultActive, "true",defaultTenant.Id);
          //  AddSettingIfNotExists(YtSettings.General.PermissionDefaultActive, "true", defaultTenant.Id);
          //  AddSettingIfNotExists(YtSettings.General.RoleDefaultActive, "true", defaultTenant.Id);
          //  AddSettingIfNotExists(YtSettings.General.UserDefaultActive, "true", defaultTenant.Id);
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
