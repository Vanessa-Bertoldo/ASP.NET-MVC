using Microsoft.EntityFrameworkCore;

namespace web.students.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected DatabaseContext()
        {

        }
    }
}
