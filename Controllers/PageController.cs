using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using midterm_6013532.Data;
using midterm_6013532.Models;

namespace midterm_6013532.Controllers{
    //todo2
    //[Route("api/[controller]/[action]")]
    public class PageController : Controller{
        //perform database table injection
        private midterm_6013532DbContext _db;
        public PageController(midterm_6013532DbContext db){
            _db = db;
        }//end of contructor function
        
        [HttpGet]
        public IActionResult BankAdmin(){

            //this list will be used to add item in
            List<CustomerOutput> customers = new List<CustomerOutput>();

            //pull database as table as use tolist function
            //make those object into list of object then
            //I will loop through these list to find and form what I need
            //In another word , these lists represent database
            List<Customer> queryList1 = new List<Customer>(_db.customers.ToList());
            List<Status> queryList2 = new List<Status>(_db.status.ToList());
            
            foreach(Customer cs in queryList1){
                foreach(Status st in queryList2){
                    if(cs.statusId == st.statusId){
                        CustomerOutput csOutput = new CustomerOutput{
                            customerId = cs.customerId,
                            customerName = cs.customerName,
                            phoneNumber = cs.phoneNumber,
                            statusName = st.statusName,
                        };//end of obj
                        customers.Add(csOutput);
                    }//end of if
                }//end of inner foreach
            }//end of outter foreach

            var result2 = _db.bankAccounts.Select( x => new{
                bankAccountId = x.bankAccountId,
                bankAccountNo = x.bankAccountNo,
                accountOwnerId = x.customer.customerId,
                accountOwner = x.customer.customerName,
                balance = x.balance,
                accountType = x.accountType.accountTypeName,
                status = x.customer.status.statusName,
            });
            //deliver the list of customer
            ViewBag.list2 = result2.ToList();
            ViewBag.list1 = customers.ToList();
            //I know that it is list but just to make sure that it is list
            return View("bankadmin");
        }//end of bank admin view
        [HttpGet]
        public IActionResult addCustomers(){
            var stat_ = _db.status.Select( t => new{
                statusId = t.statusId,
                statusName = t.statusName,
            });
            ViewBag.typeList = stat_.ToList();
            return View("addCustomer");
        }//end of function

        [HttpGet]
        public IActionResult editCustomer(int id){
            Console.WriteLine(id);
            Customer ctm = _db.customers
            .FirstOrDefault(
                x => x.customerId == id
            );
            List<Status> s_ = _db.status.ToList();            

            ViewBag.statList = s_;

            ViewBag.customer = new EditCusInput{
                targetCustomerId = ctm.customerId,
                newCustomerName = ctm.customerName,
                newCustomerPhone = ctm.phoneNumber,
                newStatus = ctm.statusId
            };
            return View("editCustomer");
            
        }//end of function
        [HttpGet]
        public IActionResult editAccount(int id){
            Console.WriteLine(id);
            BankAccount bk = _db.bankAccounts.FirstOrDefault(
                x => x.bankAccountId == id
            );
            List<AccountType> a_ = _db.accountTypes.ToList();

            ViewBag.list1 = a_;
            ViewBag.act_obj = new EditActInput{
                targetActNo = bk.bankAccountId,
                newBankAccountNo = bk.bankAccountNo,
                newCustomerId = bk.customerId,
                balance = bk.balance,
                newAccountTypeId = bk.accountTypeId
            };
            return View("editAccount");
        }//end of function

        [HttpGet]
        public IActionResult depositAdmin(){
            return View("depositAdmin");
        }//end of function

        [HttpGet]
        public IActionResult withdrawAdmin(){
            return View("withdrawAdmin");
        }//end of function
        
        [HttpGet]
        public IActionResult customerRegister(){
            return View("customerRegister");
        }//end of function

        [HttpGet]
        public IActionResult addAccount(){
            return View("addAccount");
        }//end of function

        [HttpGet]
        public IActionResult Customer(){
            List<Customer> _list1 = _db.customers.ToList();
            List<Status> _list2 = _db.status.ToList();            
            ViewBag.list1 = _list1;
            ViewBag.list2 = _list2;
            return View("customer");
        }//end of function customer view

        [HttpGet]
        public IActionResult customerView(int input){

            Console.WriteLine(input);
            Customer customer = _db.customers
            .Include( s => s.status)
            .FirstOrDefault(
                cs => cs.customerId == input
            );
            CustomerOutput _customerObj = new CustomerOutput(){
                customerId = customer.customerId,
                customerName = customer.customerName,
                phoneNumber = customer.phoneNumber,
                statusName = customer.status.statusName,
            };
            List<CustomerOutput> _customer = new List<CustomerOutput>();
            _customer.Add(_customerObj);
            List<BankAccount> _bankaccounts = new List<BankAccount>();  
            //select all accouont that has same customerId as input          
            foreach(BankAccount xi in _db.bankAccounts){
                if(xi.customerId == input){
                    _bankaccounts.Add(xi);
                }//end of if
            }//end of foreach
            ViewBag.customer_list = _customer;
            ViewBag.account_list = _bankaccounts;
            return View("customerView");
        }//end of function

        
        [HttpGet]
        public IActionResult customerViewing(int id){
            List<CustomerOutput> _customer = new List<CustomerOutput>();
            Customer customer = _db.customers
            .Include( s => s.status)
            .FirstOrDefault(
                cs => cs.customerId == id
            );

            CustomerOutput _customerObj = new CustomerOutput(){
                customerId = customer.customerId,
                customerName = customer.customerName,
                phoneNumber = customer.phoneNumber,
                statusName = customer.status.statusName,
            };

            _customer.Add(_customerObj);

            List<AccountOutPut> actList = new List<AccountOutPut>();
            //select all accouont that has same customerId as input
            
            List<BankAccount> queryList1 = new List<BankAccount>(_db.bankAccounts.ToList()){};
            List<AccountType> queryList2 = new List<AccountType>(_db.accountTypes.ToList()){};

            foreach(BankAccount ba in queryList1){
                if(ba.customerId == id){
                    foreach(AccountType ac in queryList2){
                        if(ac.accountTypeId == ba.accountTypeId){
                            AccountOutPut bnk = new AccountOutPut{
                                bankAccountId = ba.bankAccountId,
                                bankAccountNo = ba.bankAccountNo,
                                balance = ba.balance,
                                accountType = ac.accountTypeName,
                            };//end of object
                            actList.Add(bnk);
                        }//end of inner if
                    }//end of foreach
                }//end of if
            }//end of outter foreach loop

            ViewBag.customer_list = _customer;
            ViewBag.account_list = actList;
            return View("customerView");
        }//end of function 

        [HttpGet]
        public IActionResult transferAdmin(){
            return View("transferAdmin");
        }//end of function
    }//end of class
}//end of namespace