using System.ComponentModel.DataAnnotations;

namespace midterm_6013532.Models{
    public class AccountType {
        [Key]
        public int accountTypeId {get;set;}
        public string accountTypeName {get;set;}
    }
}//end of namespace