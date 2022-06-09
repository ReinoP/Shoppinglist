#pragma checksum "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\Shoppinglist\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "484fc85f3b457dc11b3493c0a9017227e6f59674"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shoppinglist_Create), @"mvc.1.0.view", @"/Views/Shoppinglist/Create.cshtml")]
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
#line 1 "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\_ViewImports.cshtml"
using ShoppinglistApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\_ViewImports.cshtml"
using ShoppinglistApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"484fc85f3b457dc11b3493c0a9017227e6f59674", @"/Views/Shoppinglist/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96ed2e025da74c65b67554b91eb4fcbe6a215cb9", @"/Views/_ViewImports.cshtml")]
    public class Views_Shoppinglist_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShoppinglistApp.Models.Shoppinglist>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\Shoppinglist\Create.cshtml"
  
	ViewData["Title"] = "Create";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Create</h1>\r\n\r\n<h4>Shoppinglist</h4>\r\n<hr />\r\n<div class=\"row\">\r\n\t<div class=\"col-md-4\">\r\n");
            WriteLiteral("\t\t<div class=\"text-danger\"></div>\r\n\r\n\t\t<div class=\"form-group\">\r\n\t\t\t<label>Name for the list</label>\r\n\t\t\t<input id=\"listName\" />\r\n");
            WriteLiteral(@"			<span class=""control-label"">Add item to list</span> <br />
			<input id=""addMe"" class=""form-control"" />  <br />
			<input type=""button"" id=""addSLI"" onclick=""addFunction()"" value=""Add to list"" class=""btn btn-primary"" />
		</div>
		<div class=""form-group"">
			<input type=""button"" value=""Create"" onclick=""submitForm()"" class=""btn btn-primary"" />
		</div>
");
            WriteLiteral("\t</div>\r\n\t<div class=\"col-md-4\">\r\n\t\t<h5>Items on your shoppinglist</h5> <br />\r\n\t\t<div id=\"addedItems\">\r\n\t\t</div>\r\n\t</div>\r\n</div>\r\n\r\n<div>\r\n\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "484fc85f3b457dc11b3493c0a9017227e6f596744558", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 41 "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\Shoppinglist\Create.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
            WriteLiteral(@"<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js""></script>


<script type=""text/javascript"" language=""javascript"">
	var itemArr = [];
	function addFunction() {
		var item = document.getElementById(""addMe"").value;
		itemArr.push(item);
		$(""#addedItems"").append(item + ""<br>"");
	}


	function submitForm() {
		var model = new Object();
		model.listName = document.getElementById(""listName"").value;
		model.items = itemArr;
		
		$.ajax({
			type: ""POST"",
			url: '/Shoppinglist/Create',
			data: { array:JSON.stringify(model) },
			success: function (data) {
				alert(data);
			},
			failure: function (errMsg) {
				alert(errMsg);
			}
		});
	}
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShoppinglistApp.Models.Shoppinglist> Html { get; private set; }
    }
}
#pragma warning restore 1591
