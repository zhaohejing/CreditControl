using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using YT.Configuration;
using YT.EntityFramework;

namespace YT.Migrations.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly MilkDbContext _context;

        public DefaultSettingsCreator(MilkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            AddSettingIfNotExists(YtSettings.General.MenuDefaultActive, "true");
            AddSettingIfNotExists(YtSettings.General.PermissionDefaultActive, "true");
            AddSettingIfNotExists(YtSettings.General.RoleDefaultActive, "true");
            AddSettingIfNotExists(YtSettings.General.UserDefaultActive, "true");
         
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