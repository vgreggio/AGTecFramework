using Microsoft.EntityFrameworkCore;

namespace AGTec.Common.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(DbContext dbContext)
        {
            dbContext.Database.Migrate();
        }
    }
}
