#pragma checksum "C:\Users\it\Desktop\midterm_6013532\Views\Page\editAccount.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "600c3f204f99048b6b76001b5ba3add5be179d7c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Page_editAccount), @"mvc.1.0.view", @"/Views/Page/editAccount.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"600c3f204f99048b6b76001b5ba3add5be179d7c", @"/Views/Page/editAccount.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa4f2a98f464c38781d0ef8aaceb0f9b855c1ffe", @"/Views/_ViewImports.cshtml")]
    public class Views_Page_editAccount : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute(":value", new global::Microsoft.AspNetCore.Html.HtmlString("x.accountTypeId"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-for", new global::Microsoft.AspNetCore.Html.HtmlString("x in list1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div id='app1'>
     <div class=""form-group"">

        <label>Account ID</label>
        <input readonly class=""form-control"" v-model=""act_obj.targetActNo""/>
        <label>Bank Account No</label>
        <input class=""form-control"" v-model=""act_obj.newBankAccountNo""/>
        <label>Customer ID</label>
        <input class=""form-control"" v-model=""act_obj.newCustomerId""/>
        <label>Balance</label>
        <input class=""form-control"" v-model=""act_obj.balance""/>
        <label>Account Type</label>
        <select class=""form-control"" v-model=""act_obj.newAccountTypeId"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "600c3f204f99048b6b76001b5ba3add5be179d7c4380", async() => {
                WriteLiteral("\r\n                    {{x.accountTypeName}}\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </select>\r\n\r\n    </div>\r\n    <br>\r\n    <button ");
            WriteLiteral("@click=\"save_act()\" class=\"btn btn-button btn-outline-primary\">Save Change</button>\r\n\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>
        var component = {
            el: '#app1',
            data: {
                act_obj: {
                    targetActNo: 0,
                    newBankAccountNo: 0,
                    newCustomerId: 0,
                    balance: 0,
                    newAccountTypeId: 0,
                },//end of act_obj
                list1: [],
            },//end of data
            methods: {

            },//end of method
            created(){
                this.act_obj = ");
#nullable restore
#line 43 "C:\Users\it\Desktop\midterm_6013532\Views\Page\editAccount.cshtml"
                          Write(Html.Raw(Json.Serialize(@ViewBag.act_obj)));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n                this.list1 = ");
#nullable restore
#line 44 "C:\Users\it\Desktop\midterm_6013532\Views\Page\editAccount.cshtml"
                        Write(Html.Raw(Json.Serialize(@ViewBag.list1)));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n            },//end of created\r\n        };\r\n        var vuejs = new Vue(component);\r\n    </script>\r\n");
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
