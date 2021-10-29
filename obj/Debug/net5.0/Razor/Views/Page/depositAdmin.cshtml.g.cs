#pragma checksum "C:\Users\it\Desktop\midterm_6013532\Views\Page\depositAdmin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a7463df70748dec09f5d21297231007dc6bfd9d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Page_depositAdmin), @"mvc.1.0.view", @"/Views/Page/depositAdmin.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\it\Desktop\midterm_6013532\Views\_ViewImports.cshtml"
using midterm_6013532;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\it\Desktop\midterm_6013532\Views\_ViewImports.cshtml"
using midterm_6013532.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7463df70748dec09f5d21297231007dc6bfd9d1", @"/Views/Page/depositAdmin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa4f2a98f464c38781d0ef8aaceb0f9b855c1ffe", @"/Views/_ViewImports.cshtml")]
    public class Views_Page_depositAdmin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div id=""app1"">
    <h3 align=""center"">List of Bank Account</h3>
    <table class=""table table-striped"">
        <thead class=""thead-dark"">
            <tr>
                <th>Bank Account ID</th>
                <th>Customer Name</th>
                <th>Bank Account No</th>
                <th>Balance</th>
                <th>Account Type Name</th>
                <th>Status Name</th>
            </tr>
        </thead>
            <tr v-for="" i in bankAccount_list"">
                <td>{{i.bankAccountId}}</td>
                <td>{{i.customerName}}</td>
                <td>{{i.bankAccountNo}}</td>
                <td>{{i.balance}}</td>
                <td>{{i.accountTypeName}}</td>
                <td>{{i.statusName}}</td>
            </tr>            
    </table>
    <hr><br>
    <h3 align=""center"">Deposit</h3>
    <div class=""form-group"">
        <label>Bank Account No</label>
        <input class=""form-control"" v-model=""deposit_.targetAct""/>
    </div>
    <div class=""form-g");
            WriteLiteral("roup\">\r\n        <label>Bank Account No</label>\r\n        <input class=\"form-control\" v-model=\"deposit_.amount\"/>\r\n    </div>\r\n    <div align=\"center\">   \r\n    <button ");
            WriteLiteral(@"@click=""deposit()"" class=""btn btn-button btn-outline-success"">Deposit</button>
    </div>
</div>
        <!--
        bankAccountId = act.bankAccountId,
        customerName = act.customer.customerName,
        bankAccountNo = act.bankAccountNo,
        balance = act.balance,
        accountTypeName = act.accountType.accountTypeName,
        statusName = act.customer.status.statusName,
        -->



");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>
        var component = {
            el: ""#app1"",
            data: {
                bankAccount_list: [],
                deposit_: {
                    targetAct: 0,
                    amount: 0,
                },
            },//end of data
            methods: {
                async deposit(){
                    var url = '/api/transaction/depositAdmin';
                    var result = await axios.post(url , this.deposit_);
                    console.log(this.deposit_);
                    location.reload();
                },//end of deposit
            },//end of methods
            async created(){
                var url = '/api/bank/getBankAccount';
                var result = await axios.get(url);
                this.bankAccount_list = result.data;
                console.log(this.bankAccount_list);
            },//end of created
        };//end of component
        var vueJs_ = new Vue(component);
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591