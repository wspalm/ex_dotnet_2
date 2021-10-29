using Microsoft.AspNetCore.Identity;

namespace midterm_6013532.Data{

    public class AppRole:IdentityRole<int>{
 
        public AppRole(string Name):base(Name){}
        
    }
}