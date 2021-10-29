using System.ComponentModel.DataAnnotations;

namespace midterm_6013532.Models{
    public class BankAccount {
        [Key]
        public int bankAccountId {get;set;}
        public int bankAccountNo {get;set;}
        public int customerId {get;set;} 
        public Customer customer {get;set;}
        public double balance {get;set;}
        public int accountTypeId {get;set;}
        public AccountType accountType {get;set;}
        
    }//end of bank class
}//end of namespace