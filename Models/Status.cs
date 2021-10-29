using System.ComponentModel.DataAnnotations;

namespace midterm_6013532.Models{
    public class Status{
        [Key]
        public int statusId {get;set;}
        public string statusName {get;set;}
    }//end of status
}//end of namespace