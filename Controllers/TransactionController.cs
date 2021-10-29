using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using midterm_6013532.Data;
using midterm_6013532.Models;

namespace midterm_6013532.Controllers{
    [Route("api/[controller]/[action]")]
    public class TransactionController : Controller{
        //perform db injection
        private midterm_6013532DbContext _db;
        //create contructor function
        public TransactionController(midterm_6013532DbContext db){
            _db = db;
        }//end of contructor function
        [HttpGet]
        public IActionResult getAccounts(){
            return Json(_db.bankAccounts.Select(
                xi => new{
                    CustomerName = xi.customer.customerName,
                    CustomerId = xi.customerId,
                    CustomerAccountNo = xi.bankAccountNo,
                    Balance = xi.balance,
                    Status = xi.customer.status.statusName,
                    AccountType = xi.accountType.accountTypeName
                }
            ).ToList());
        }//end of function

        ////////////////////// POST ///////////////////////
        [HttpPost]
        public IActionResult TransferAdmin([FromBody] Transfer input1){
            Console.WriteLine("fromAct: " + input1.fromAct);
            Console.WriteLine("targetAct: " + input1.targetAct);
            Console.WriteLine("amount: " + input1.amount);
            if(input1.fromAct == input1.targetAct){
                Console.WriteLine("from act == target act");
                return Json(new{
                    Code = 402,
                    Message = "You cannot have same accont number to transfer",
                    Data = "Please re-input",
                });
            }//end of if
            Console.WriteLine("Object about to be searched");
            BankAccount fromAct = _db.bankAccounts.FirstOrDefault(
                fa => fa.bankAccountNo == input1.fromAct
            );
            BankAccount targetAct = _db.bankAccounts.FirstOrDefault(
                ta => ta.bankAccountNo == input1.targetAct
            );
            Console.WriteLine("Query complete , object assigned to value");
            if(fromAct.bankAccountNo == targetAct.bankAccountNo){
                Console.WriteLine("Duplicate input");
                return Json(new{
                    Code = 402,
                    Message = "You cannot have same accont number to transfer",
                    Data = "Please re-input",
                });
            }//end of if
            else if(fromAct.balance < input1.amount || fromAct.balance == 0){
                Console.WriteLine("Insufficient money case");
                return Json(new{
                    Code = 403,
                    Message = "Insufficient Money",
                    Data = "Please deposit more money and try again"
                });
            }//end of else if
            else if(fromAct == null || targetAct == null){
                Console.WriteLine("Account not found >> your objects are null");
                return Json(new{
                    Code = 404,
                    Message = "Account Not Found",
                    Data = "No account exist , please contact bank to register your account",
                });
            }//end of else if
            else if(fromAct.bankAccountNo != targetAct.bankAccountNo && 
            fromAct != null || targetAct != null){
                Console.WriteLine("Success Case");
                fromAct.balance = fromAct.balance - input1.amount;
                targetAct.balance = targetAct.balance + input1.amount;
                _db.SaveChanges();
                Console.WriteLine("SaveChanges");
            }//end of else if
            return getAccounts();
        }//end of function


        [HttpPost]
        public IActionResult TransferCustomer([FromBody] Transfer input1){
            if(input1.fromAct == input1.targetAct){
                return Json(new{
                    Code = 402,
                    Message = "You cannot have same accont number to transfer",
                    Data = "Please re-input",
                });
            }//end of if

            BankAccount fromAct = _db.bankAccounts.FirstOrDefault(
                fa => fa.bankAccountNo == input1.fromAct
            );
            BankAccount targetAct = _db.bankAccounts.FirstOrDefault(
                ta => ta.bankAccountNo == input1.targetAct
            );
            if(fromAct == null && targetAct == null){
                return Json(new{
                        Code = 404,
                        Message = "From Account and Destination Account Not Found",
                        Data = "Make sure every input is correct"
                    });
            }//end of if
            else if(fromAct == null){
                    return Json(new{
                        Code = 404,
                        Message = "From Account Not Found",
                        Data = "",
                    });
            }//end of else if
            else if(targetAct == null){
                return Json(new{
                        Code = 404,
                        Message = "Destination Account Not Found",
                        Data = "",
                    });
            }//end of else if

            //Performing two nested selection
            Customer customer_check = _db.customers.FirstOrDefault(
                cc => cc.customerId == fromAct.customerId
            );
            if(customer_check == null){
                return Json(new{
                    Code = 666,
                    Message = "Customer is null or Not Found",
                    Data = "",
                });
            }//end of if
            Status status_check = _db.status.FirstOrDefault(
                sc => sc.statusId == customer_check.statusId
            );            

            if( status_check.statusId == 1 || 
                status_check.statusId == 3 ||
                status_check.statusId == 6 ||
                status_check.statusId == 7  ){
                if(fromAct.bankAccountNo == targetAct.bankAccountNo){
                    return Json(new{
                        Code = 402,
                        Message = "You cannot have same accont number to transfer",
                        Data = "Please make sure every input is correct",
                   });
                }//end of if
                else if(fromAct.balance < input1.amount || fromAct.balance == 0){
                    return Json(new{
                        Code = 403,
                        Message = "Insufficient Money",
                        Data = "Please deposit more money and try again",
                    });
                }//end of else if
                else if(fromAct.bankAccountNo != targetAct.bankAccountNo && 
                    fromAct != null || targetAct != null){
                        fromAct.balance = fromAct.balance - input1.amount;
                        targetAct.balance = targetAct.balance + input1.amount;
                        _db.SaveChanges();
                        return Json(new{
                            Code = 200,
                            Message = "Success !!",
                            Data = "Balance left in transfering account " 
                            + fromAct.balance + " And " 
                            + "Balance left in receiving account " 
                            + targetAct.balance,
                        });
                }//end of else if
            }//end of outter if
            
            else if(status_check.statusId == 2){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for withdrawing only",
                    Data = "",
                });
            }//end of else if
            else if(status_check.statusId == 4){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for deposit only",
                    Data = "",
                });
            }//end of else if
            else if(status_check.statusId == 5){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for deposit and withdraw only",
                    Data = "",
                });
            }//end of else if
            else if(status_check.statusId == 8){
                return Json(new{
                    Code = 411,
                    Message = "You accounts are waiting for Admin approval",
                    Data = "",
                });
            }//end of else if
            else if(status_check.statusId == 9){
                return Json(new{
                    Code = 999,
                    Message = "You accounts are blocked of any usage, please contact Bank for any further information",
                    Data = "",
                });
            }//end of else if
            return getAccounts();//this for the unexpected condition
        }//end of function

        [HttpPost]
        public IActionResult DepositAdmin([FromBody] Deposit input1){
            Console.WriteLine("targetAct: " + input1.targetAct);
            Console.WriteLine("amount: " + input1.amount);
            BankAccount act = _db.bankAccounts.FirstOrDefault(
                ac => ac.bankAccountNo == input1.targetAct
            );
            Console.WriteLine(">> Object Created : " + act);
            if( act == null ){
                Console.WriteLine("Your obj is null");
                return Json(new{
                    Code = 404,
                    Message = "Account Not Found",
                    Data = "",
                });
            }//end of if
            else if(act != null){
                Console.WriteLine("enter correct condition");
                act.balance = act.balance + input1.amount;
                _db.SaveChanges();
                Console.WriteLine("--SaveChange--");
            }//end of else if
            return getAccounts();
        }//end of function


        [HttpPost]
        public IActionResult DepositCustomer([FromBody] Deposit input1){
 
            BankAccount act = _db.bankAccounts.FirstOrDefault(
                ac => ac.bankAccountNo == input1.targetAct
            );

            //when you are using cross object referencing
            //before you are going to intoduce referencing from firstone
            //to secondone , you must perform null check
            //because if you don't perform when the first is null
            //the second object will thorwn error exception
            if( act == null ){
                return Json(new{
                    Code = 404,
                    Message = "Account Not Found",
                    Data = "",
                });
            }//end of if

            //Performing one nested selection
            Customer customer_check = _db.customers.FirstOrDefault(
                cc => cc.customerId == act.customerId
            );

            if( customer_check.statusId == 2){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for withdrawing only",
                    Data = "",
                });
            }//end of else if
            else if( customer_check.statusId == 3){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for transfering only",
                    Data = "",
                });
            }//end of else if
            else if( customer_check.statusId == 7){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for withdraw and transfer only",
                    Data = "",
                });
            }//end of else if
            else if( customer_check.statusId == 8){
                return Json(new{
                    Code = 411,
                    Message = "You accounts are waiting for Admin approval",
                    Data = "",
                });
            }//end of else if
            else if( customer_check.statusId == 9){
                return Json(new{
                    Code = 999,
                    Message = "You accounts are blocked of any usage, please contact Bank for any further information",
                    Data = "",
                });
            }//end of else if
            else if(customer_check.statusId == 1 || customer_check.statusId == 4 || customer_check.statusId == 5 ||
            customer_check.statusId == 6 ){
                act.balance = act.balance + input1.amount;
                _db.SaveChanges();
                return Json(new{
                    Code = 200,
                    Message = "Success !!",
                    Data = "Balance: " + act.balance,
                });
            }//end of else if
            return Json(act);            
        }//end of function

        [HttpPost]
        public IActionResult WithDrawAdmin([FromBody] Deposit input1){

            BankAccount act = _db.bankAccounts.FirstOrDefault(
                ac => ac.bankAccountNo == input1.targetAct
            );
            if( act == null ){
                return Json(new{
                    Code = 404,
                    Message = "Account Not Found",
                    Data = "",
                });
            }//end of if
            else if(input1.amount > act.balance){
                return Json(new{
                    Code = 400,
                    Message = "Insufficient Money",
                    Data = "Please deposit more and try again",
                });
            }//end of else if
            else if(act != null){
                act.balance = act.balance - input1.amount;
                _db.SaveChanges();
            }//end of else if
            return getAccounts();
        }//end of function
        [HttpPost]
        public IActionResult WithDrawCustomer([FromBody] Deposit input1){
            BankAccount act = _db.bankAccounts
            .Include(x => x.customer)
            .Include(x => x.accountType)
            .FirstOrDefault(
                ac => ac.bankAccountNo == input1.targetAct
            );
            
            if( act == null ){
                return Json(new{
                    Code = 404,
                    Message = "Account Not Found",
                    Data = "",
                });
            }//end of if

            //Performing one nested selection
            // Customer customer_check = _db.customers.FirstOrDefault(
            //     cc => cc.customerId == act.customerId
            // );
            Console.WriteLine(act.customer.statusId);

            if(act.balance < input1.amount || act.balance <= 0){
                return Json(new{
                    Code = 400,
                    Message = "Insuffcient Money",
                    Data = "Please deposit more and try again",
                });
            }//end of else if
            else if( act.customer.statusId == 4){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for Deposit only",
                    Data = "",
                });
            }//end of else if
            else if( act.customer.statusId == 3){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for transfering only",
                    Data = "",
                });
            }//end of else if
            else if( act.customer.statusId == 7){
                return Json(new{
                    Code = 400,
                    Message = "You accounts are allowed for withdraw and transfer only",
                    Data = "",
                });
            }//end of else if
            else if( act.customer.statusId == 8){
                return Json(new{
                    Code = 411,
                    Message = "You accounts are waiting for Admin approval",
                    Data = "",
                });
            }//end of else if
            else if(act.customer.statusId == 9){
                return Json(new{
                    Code = 999,
                    Message = "You accounts are blocked of any usage, please contact Bank for any further information",
                    Data = "",
                });
            }//end of else if

            else if(act.customer.statusId == 5 || act.customer.statusId == 6 || act.customer.statusId == 1 || 
            act.customer.statusId == 2){
                act.balance = act.balance - input1.amount;
                _db.SaveChanges();
                return Json(new{
                    Code = 200,
                    Message = "Success !!",
                    Data = "Balance Left : " + act.balance,
                });
            }//end of else if
            return Json(act);            
        }//end of function


    }//end of class
}//end of nsmespace