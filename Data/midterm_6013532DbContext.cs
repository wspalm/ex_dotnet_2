using midterm_6013532.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
 
namespace midterm_6013532.Data
{
     public class midterm_6013532DbContext : IdentityDbContext<AppUser,AppRole,int>{
         public midterm_6013532DbContext(DbContextOptions<midterm_6013532DbContext> options):base(options){
            
         }//ef
         protected override void OnModelCreating(ModelBuilder builder)
        {
 
            base.OnModelCreating(builder);
        }
        public DbSet<AppUser> AppUsers {get;set;}

        ////////////////////////////////////////////
        public DbSet<AccountType> accountTypes {get;set;}
        public DbSet<BankAccount> bankAccounts {get;set;}
        public DbSet<Customer> customers {get;set;}
        public DbSet<Status> status {get;set;}
        
    
     }//ec
}//en