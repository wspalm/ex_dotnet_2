using System.ComponentModel.DataAnnotations;

namespace midterm_6013532.Models{
    public class Customer{
        [Key]
        public int customerId {get;set;}
        public string customerName {get;set;}
        public int phoneNumber {get;set;}
        public int statusId {get;set;}
        public Status status {get;set;}
    }//end of customer model
}//end of namespace