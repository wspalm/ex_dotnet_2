using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using midterm_6013532.Data;
using midterm_6013532.Models;


namespace midterm_6013532.Controllers{
    [Route("api/[controller]/[action]")]
    public class BankController : Controller{
        private midterm_6013532DbContext _db;
        public BankController(midterm_6013532DbContext db){
            _db = db;
        }//end of contructor function
        [HttpGet]
        public IActionResult getCustomer(){
            return Json(_db.customers.Select( i => new {
                CustomerId = i.customerId,
                CustomerName = i.customerName , 
                PhoneNumber = i.phoneNumber ,
                Status = i.status.statusName ,
            }).ToList());
        }//end of function
        [HttpGet]
        public IActionResult getAccountTypes(){
            return Json(_db.accountTypes.Select(
                acttype => new{
                    acttype.accountTypeId,
                    acttype.accountTypeName,
                }
            ).ToList());
        }//end of function
        [HttpGet]
        public IActionResult getBankAccount(){
            return Json(_db.bankAccounts.Select(
                act => new{
                    bankAccountId = act.bankAccountId,
                    customerName = act.customer.customerName,
                    bankAccountNo = act.bankAccountNo,
                    balance = act.balance,
                    accountTypeName = act.accountType.accountTypeName,
                    statusName = act.customer.status.statusName,
                }
            ).ToList());
        }//end of function
        [HttpGet]
        public IActionResult getStatus(){
            return Json(_db.status.Select(
                st => new{
                    st.statusId,
                    st.statusName,
                }
            ).ToList());
        }//end of function
        

        ///////////////////////////// Post Section ///////////////////

        [HttpPost]        
        public IActionResult addCustomer([FromBody] CustomerInput input1){
            Customer cus1 = new Customer{
                customerName = input1.customerName,
                phoneNumber = input1.phoneNumber,
                statusId = input1.statusTypeId,
            };//end of obj
            _db.customers.Add(cus1);
            _db.SaveChanges();
            return Json(cus1);
        }//end of function
        
        [HttpPost]
        public IActionResult addAccount([FromBody] BankActInput input1){
            //return Json(input1);
            Console.WriteLine("input customerId : " + input1.customerId);
            Console.WriteLine("input bankActNo : " + input1.bankActNo);
            Console.WriteLine("input balance : " + input1.balance);
            Console.WriteLine("input accountTypeId : " + input1.accountTypeId);

            Console.WriteLine("I've got call");
            AccountType act_result = _db.accountTypes.FirstOrDefault(
                ar => ar.accountTypeId == input1.accountTypeId
            );
            if(act_result == null){
                return Json(new{
                        ErrorCode = 401,
                        Message = "Account Type Not Exist",
                    });
            }//end of if
            Customer customer_result = _db.customers.FirstOrDefault(
                cr => cr.customerId == input1.customerId
            );

            if(customer_result == null){
                return Json(new{
                        ErrorCode = 404,
                        Message = "ID Not Found",
                    });
            }//end of if

            Console.WriteLine("act: " + act_result);
            Console.WriteLine("customerQuery: " + customer_result);
            Console.WriteLine("input customerId : " + input1.customerId);
            Console.WriteLine("input bankActNo : " + input1.bankActNo);
            Console.WriteLine("input balance : " + input1.balance);
            Console.WriteLine("input accountTypeId : " + input1.accountTypeId);

            Console.WriteLine(">> value is not null");


            //if the property is nested
            //we must map the query result into the new object
            //to make it work
            //mapping input property will somehow not connected
            BankAccount act = new BankAccount{
                bankAccountNo = input1.bankActNo,
                customerId = customer_result.customerId,
                customer = customer_result,
                balance = input1.balance,
                accountTypeId = input1.accountTypeId,
                accountType = act_result,
            };
            Console.WriteLine("object created >> ");
            foreach(BankAccount xi in _db.bankAccounts){
                if(xi.bankAccountNo == act.bankAccountNo){
                    return Json(new{
                        ErrorCode = 401,
                        Message = "Bank Account Number Already Exist",
                    });
                }//end of if
            }//end of foreach
            Console.WriteLine(act);
            _db.bankAccounts.Add(act);
            _db.SaveChanges();
            Console.WriteLine("complete!!");
            return Json(act);            
        }//end of function
        
        [HttpPost]
        public IActionResult editAccount([FromBody] EditActInput input1){

            BankAccount target_account = _db.bankAccounts
                                    .FirstOrDefault( 
                                        xo => xo.bankAccountId == input1.targetActNo
                                    );
            //check if the data exist in customers table
            //new holder of the account must exist in database
            //in order to change holder
            Customer target_customer = _db.customers
                                    .FirstOrDefault(
                                        tr => tr.customerId == input1.targetActNo
                                    );
            if(target_account == null){
                return Json(new{
                    ErrorCode = 404,
                    Message = "Editing Target Not Found",
                });
            }//end of if
            else if(target_customer == null){
                return Json(new{
                    ErrorCode = 401,
                    Message = "This Customer Doesn't Exist In Database",
                });
            }//end of else if
            else if(target_account != null && target_customer != null){                
                // target_account.bankAccountNo = input1.newBankAccountNo;
                // target_account.customerId = input1.newCustomerId;
                // target_account.balance = input1.balance;
                // target_account.accountTypeId = input1.newAccountTypeId;
                if(input1.newAccountTypeId != 0){
                   target_account.accountTypeId = input1.newAccountTypeId;
                }//end of if
                if(input1.newCustomerId != 0){
                   target_account.customerId = input1.newCustomerId;
                }//end of if
                if(input1.balance != 0){
                   target_account.balance = input1.balance;
                }//end of if
                if(input1.newBankAccountNo != 0){
                   target_account.bankAccountNo = input1.newBankAccountNo;
                }//end of if                
                _db.SaveChanges();                
            }//end of else if
            return Json(
                _db.bankAccounts.Select(
                    b => new {
                        CustomerName = b.customer.customerName,
                        BankAccountId = b.bankAccountId,
                        BankAccountNo = b.bankAccountNo,
                        CustomerID = b.customerId,
                        Balance = b.balance,
                        AccountType = b.accountType,
                    }
                ).ToList());
        }//end of function


        [HttpPost]
        public IActionResult editCustomer_([FromBody] EditCusInput input1){
            //return Json(input1);
            Customer cs = new Customer{
                customerName = input1.newCustomerName,
                phoneNumber = input1.newCustomerPhone,
                statusId = input1.newStatus,
            };
            _db.customers.Update(cs);
            _db.SaveChanges();
            return Json(new{
                message = "updated",
            });
        }//end of function
        [HttpPost]
        public IActionResult editCustomer([FromBody] EditCusInput input1){
            //return Json(input1);

            Console.WriteLine("I got call , function editcustomer");
            Customer old_customer = _db.customers
                                        .Include(x => x.status)
                                        .FirstOrDefault( tc =>
                                        tc.customerId == input1.targetCustomerId);
            Console.WriteLine(input1.targetCustomerId);            
            if(old_customer == null){
                return Json(new{
                    ErrorCode = 404,
                    Message = "Editing Target Not Found",
                });
            }//end of if
            Console.WriteLine(input1);
            if(input1.newCustomerName.Trim() != ""){
                old_customer.customerName = input1.newCustomerName;
            }//end of if
            if(input1.newCustomerPhone != old_customer.phoneNumber &&
            input1.newCustomerPhone != -1){
                old_customer.phoneNumber = input1.newCustomerPhone;
            }//end of if
            if(input1.newStatus != old_customer.statusId && 
            input1.newStatus != -1){
                old_customer.statusId = input1.newStatus;
            }//end of if
            Console.WriteLine(old_customer);

            //return Json(old_customer);

            _db.customers.Update(old_customer);
            _db.SaveChanges();
            return Json(_db.customers.Select(
                        cs => new{
                            CustomerId = cs.customerId,
                            CustomerName = cs.customerName,
                            CustomerPhone = cs.phoneNumber,
                            CustomerStatus = cs.status.statusName,
                        }
                    ).ToList());
        }//end of function

        [HttpPost]
        public IActionResult editStatus([FromBody] EditStat input1){
            Status target_status = _db.status.FirstOrDefault(
                ts => ts.statusId == input1.targetId
            );
            if(target_status != null){
                target_status.statusName = input1.newStatusName;
                _db.SaveChanges();
            }//end of if
            else if(target_status == null){
                return Json(new{
                    ErrorCode = 404,
                    Message = "Account Not Found",
                });
            }//end of else if
            return Json(_db.status.ToList());
        }//end of function

        [HttpPost]
        public IActionResult editAccountType([FromBody] EditActType input1){
            AccountType target_actType = _db.accountTypes.FirstOrDefault(
                ta => ta.accountTypeId == input1.targetActId
            );
            if(target_actType != null){
                target_actType.accountTypeName = input1.newActTypeName;
                _db.SaveChanges();
            }//end of if
            else if(target_actType == null){
                return Json(new{
                    ErrorCode = 404,
                    Message = "Account Not Found",
                });
            }//end of else if
            return Json(_db.accountTypes.ToList());
        }//end of function

        [HttpPost]
        public IActionResult deleteAccount([FromBody] idSelector input1){

            BankAccount targetAccount = _db.bankAccounts.FirstOrDefault(
                tact => tact.bankAccountId == input1.targetID
            );
            if(targetAccount == null){
                return Json(new{
                    ErrorCode = 404,
                    Message = "Account Not Found"
                });
            }//end of if
            else if(targetAccount != null){
                _db.bankAccounts.Remove(targetAccount);
                _db.SaveChanges();
            }//end of else if
            return getBankAccount();
        }//end of function
        
        [HttpPost]
        public IActionResult deleteCustomer([FromBody] idSelector input1){
            Console.WriteLine("Hi deletecustomer function got called");
            Console.WriteLine("With Data : >>" + input1);
            Customer targetCustomer = _db.customers.FirstOrDefault(
                tc => tc.customerId == input1.targetID
            );
            Console.WriteLine("Your obj created with :" + targetCustomer );
            if(targetCustomer == null){
                Console.WriteLine("Your obj is null");
                return Json(new{
                    ErrorCode = 404,
                    Message = "Customer Not Found"
                });
            }
            else if(targetCustomer != null){
                Console.WriteLine("Enter Correct Condition");
                _db.customers.Remove(targetCustomer);
                _db.SaveChanges();
                Console.WriteLine("save change");
            }
            return getCustomer();
        }//end of delete customer

        [HttpPost]
        public IActionResult removeCustomer([FromBody] CustomerOutput input1){
            Console.WriteLine("removeCustomer Function Executed");

            Customer targetCustomer = _db.customers.FirstOrDefault(
                tc => tc.customerId == input1.customerId
            );
            if(targetCustomer == null){
                Console.WriteLine("Your object is null");
                return Json(new{
                Code = 400,
                Message = "Target Customer Not Found",
                });
            }//end of if
            else{
                Console.WriteLine("Enter the correct condition");
                _db.customers.Remove(targetCustomer);
                _db.SaveChanges();
                Console.WriteLine("Remove Complete");
                return Json(new{
                    Code = 200,
                    Message = "Customer Deleted",
                });
            }//end of else
        }//end of delete customer

        [HttpPost]
        public IActionResult removeCustomer2(int id){
            Console.WriteLine("removeCustomer Function Executed");
            Console.WriteLine(id);
            return Json(id);

            Customer targetCustomer = _db.customers.FirstOrDefault(
                tc => tc.customerId == id
            );
            if(targetCustomer == null){
                Console.WriteLine("Your object is null");
                return Json(new{
                Code = 400,
                Message = "Target Customer Not Found",
                });
            }//end of if
            else{
                Console.WriteLine("Enter the correct condition");
                _db.customers.Remove(targetCustomer);
                _db.SaveChanges();
                Console.WriteLine("Remove Complete");
                return Json(new{
                    Code = 200,
                    Message = "Customer Deleted",
                });
            }//end of else
        }//end of delete customer

    }//end of controller
}//end of namespace