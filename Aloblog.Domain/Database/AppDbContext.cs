using Aloblog.Domain.Entities.Banners;
using Aloblog.Domain.Entities.Blogs;
using Aloblog.Domain.Entities.MainPages;
using Aloblog.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Domain.Database;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public AppDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source=188.213.199.188;Initial Catalog=Blog;User ID=Alobegoo;Password=Aa@123;Trust Server Certificate=True"
        );
        base.OnConfiguring(optionsBuilder);
    }
    public DbSet<Banner> Banners { set; get; }
    public DbSet<BannerSlider> BannerSliders { set; get; }
    public DbSet<MainBanner> MainBanners { set; get; }
    public DbSet<Magazine> Magazines { set; get; }
    public DbSet<WorkFlow> WorkFlows { set; get; }
    public DbSet<WorkFlowItem> WorkFlowItems { set; get; }
    public DbSet<AloBegooService> AloBegooServices { set; get; }
    public DbSet<BrandSlider> BrandSliders { set; get; }
    public DbSet<CategorySection> CategorySections { set; get; }
    public DbSet<DesignTree> DesignTrees { set; get; }
    public DbSet<DesignItem> DesignItems { set; get; }
    public DbSet<Faq> Faqs { set; get; }
    public DbSet<FaqItem> FaqItems { set; get; }
    public DbSet<MediaGrid> MediaGrids { set; get; }
    public DbSet<Blog> Blogs { set; get; }
    public DbSet<BlogDetail> BlogDetails { set; get; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // modelBuilder.Entity<City>().HasQueryFilter(x => !x.IsDelete);

        base.OnModelCreating(modelBuilder);
    }
}