#pragma checksum "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\Shoppinglist\_shoppingListItemPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9321345ea8134be49a138a3f5449c940f23a2565"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shoppinglist__shoppingListItemPartialView), @"mvc.1.0.view", @"/Views/Shoppinglist/_shoppingListItemPartialView.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9321345ea8134be49a138a3f5449c940f23a2565", @"/Views/Shoppinglist/_shoppingListItemPartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96ed2e025da74c65b67554b91eb4fcbe6a215cb9", @"/Views/_ViewImports.cshtml")]
    public class Views_Shoppinglist__shoppingListItemPartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div>
	<div id=""addedItems"">

	</div>
	<div>
		<label>Item to add</label>
		<input id=""addMe"" />
		<span class=""text-danger""></span>
	</div>
	<div>
		<input type=""button"" id=""addSLI"" onclick=""addFunction()"" value=""Add to list"" class=""btn btn-primary"" />
	</div>
</div>


");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n");
#nullable restore
#line 19 "F:\shoppinglist\Shoppinglist\Shoppinglist\Views\Shoppinglist\_shoppingListItemPartialView.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
            WriteLiteral(@"<script type=""text/javascript"" language=""javascript"">
  var itemArr = [];
  function addFunction() {
    var add = document.getElementById(""addMe"").value;
    itemArr.push(add);
    $(""#addedItems"").append(add + ""<br>"");
    //for (var x = 0; x < itemArr.length; x++) {
    //   console.log(itemArr[x]);
    //}
    //ajax-post to controller or how do i append this list to the model list?
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
