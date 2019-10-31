using Microsoft.EntityFrameworkCore;
using TC.Entity.Entities;

namespace TC.DataAccess.DatabaseContext
{
    public partial class TestingCenterDbContext : DbContext
    {
        public TestingCenterDbContext(DbContextOptions<TestingCenterDbContext> options) : base(options) { }
        //public void Configure(EntityTypeBuilder<UserTest> builder)
        //{
        //    // This Converter will perform the conversion to and from Json to the desired type
        //    builder.Property(e => e.Answers).HasConversion(
        //        v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
        //        v => JsonConvert.DeserializeObject<IList<UserTestAnswer>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        //}
        public DbSet<TestInfo> TestInfo { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
