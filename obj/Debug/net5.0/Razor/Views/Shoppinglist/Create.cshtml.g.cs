#pragma checksum "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\Shoppinglist\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41ffeeb768dc953702834fbc604ab38e14528605"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"41ffeeb768dc953702834fbc604ab38e14528605", @"/Views/Shoppinglist/Create.cshtml")]
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
            WriteLiteral(@"
<h1>Create</h1>

<h4>Shoppinglist</h4>
<hr />
<div class=""row"">
	<div class=""col-md-4"">
		<div class=""text-danger""></div>
		<div class=""form-group"">
			<label>Name for the list</label>
			<input id=""listName"" />
			<span class=""control-label"">Add item to list</span> <br />
			<input id=""addMe"" class=""form-control"" />  <br />
			<input type=""button"" id=""addSLI"" onclick=""addFunction()"" value=""Add to list"" class=""btn btn-primary"" />
		</div>
		<div class=""form-group"">
			<input type=""button"" value=""submitForm"" onclick=""submitForm()"" class=""btn btn-primary"" />
		</div>
	</div>
	<div class=""col-md-4"">
		<h5>Items on your shoppinglist</h5> <br />
		<div id=""addedItems"">
		</div>
	</div>
</div>

<div>
	");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "41ffeeb768dc953702834fbc604ab38e145286054404", async() => {
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
#line 37 "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\Shoppinglist\Create.cshtml"
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
		$.ajax({
			type: ""POST"",
			url: '/Shoppinglist/Create',
			data: { listname: document.getElementById(""listName"").value, shoppinglist: JSON.stringify(itemArr)},
			success: function (data) {
				alert(""Success"");
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
