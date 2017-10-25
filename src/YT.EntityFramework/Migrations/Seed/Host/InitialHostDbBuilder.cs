using YT.EntityFramework;

namespace YT.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly MilkDbContext _context;

        public InitialHostDbBuilder(MilkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
