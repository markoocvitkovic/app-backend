using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AstraLicenceManager.Entities
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<App> Apps { get; set; }

        public DbSet<Licence> Licences { get; set; }

        public DbSet<AppLevel> AppLevels { get; set; }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<User>()
                .HasIndex(a => a.Email)
                .IsUnique();

             modelBuilder.Entity<Company>()
                .HasIndex(a=>a.SifPar)
                .IsUnique();

             modelBuilder.Entity<Company>()
                    .HasOne(p => p.User).WithOne()      
                    .HasForeignKey<Company>(p => p.InsertUserId).OnDelete(DeleteBehavior.Restrict)              
                    .HasConstraintName("FK_Company_User_InsertUserId");  
                         
             modelBuilder.Entity<App>()
                    .HasOne(p => p.User).WithOne()
                    .HasForeignKey<App>(p => p.InsertUserId).OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_App_User_InsertUserId");        
           
             modelBuilder.Entity<App>()
                    .HasOne(p => p.Company).WithOne()
                    .HasForeignKey<Company>(p => p.CompanyId).OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_App_Company_CompanyId");        

             modelBuilder.Entity<AppLevel>()
                    .HasOne(p => p.User).WithOne()
                    .HasForeignKey<AppLevel>(p => p.InsertUserId).OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AppLevel_User_InsertUserId");  
                    
             modelBuilder.Entity<AppLevel>()
                    .HasOne(p => p.Company).WithOne()
                    .HasForeignKey<AppLevel>(p => p.CompanyId) .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AppLevel_Company_CompanyId");       

             modelBuilder.Entity<AppLevel>()
                    .HasOne(p => p.App).WithOne()
                    .HasForeignKey<AppLevel>(p => p.AppId).OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AppLevel_App_AppId");             
                    
             modelBuilder.Entity<Licence>()
                    .HasOne(p => p.Company).WithOne()
                    .HasForeignKey<Licence>(p => p.CompanyId).OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Licence_Company_CompanyId");
            
             modelBuilder.Entity<Licence>()
                  .HasOne(p => p.User).WithOne()
                  .HasForeignKey<Licence>(p => p.InsertUserId).OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Licence_User_UserId");    
                   
             modelBuilder.Entity<Licence>()                 
                    .HasOne(p => p.App).WithOne()
                    .HasForeignKey<Licence>(p => p.AppId).OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Licence_App_AppId");
             
             modelBuilder.Entity<Licence>()
                    .HasOne(p => p.AppLevel).WithOne()
                    .HasForeignKey<Licence>(p => p.AppLevelId).OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Licence_AppLevel_AppLevelId");  
                    
            }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(@"Host=127.0.0.1;Port=5432;Username=postgres;Password=astra;Database=AstraLicence");
    }       
}                    
      
         
              
                  
                    
                                     
                 

                               
                        
    
