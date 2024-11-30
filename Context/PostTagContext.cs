using Microsoft.EntityFrameworkCore;
using PostTags.Models;

namespace PostTags.Context
{
    public class PostTagContext:DbContext
    {
        private readonly IConfiguration _config;
        private readonly ILogger<PostTagContext> _logger;

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        public PostTagContext(DbContextOptions<PostTagContext> options, IConfiguration config,ILogger<PostTagContext> logger): base(options)
        {
            
            _config = config;
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string con_string = _config.GetSection("Database:Default").Value;
            if (con_string == null)
            {
                _logger.LogError("Empty connection string");
            }
            else
                optionsBuilder.UseMySQL(con_string);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            //table level constraints.
            modelBuilder.Entity<Post>()
                .Property(pt => pt.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Tag>()
                .Property(pt => pt.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<PostTag>()
                .Property(pt => pt.PostId)
                .HasColumnType("int");
            modelBuilder.Entity<PostTag>()
                .Property(pt => pt.TagId)
                .HasColumnType("int");


        }
    }
}
