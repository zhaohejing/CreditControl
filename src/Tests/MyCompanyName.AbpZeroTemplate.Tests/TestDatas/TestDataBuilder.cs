using EntityFramework.DynamicFilters;
using YT.EntityFramework;

namespace YT.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly MilkDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(MilkDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
