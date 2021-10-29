using System.ComponentModel.DataAnnotations;

namespace midterm_6013532.Models{
    public class BankActInput {
        public int bankActNo {get;set;}
        public int customerId {get;set;}
        public double balance {get;set;}
        public int accountTypeId {get;set;} 
    }//end of class
}//end of namespace