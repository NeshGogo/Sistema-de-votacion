#pragma checksum "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd58adfabb7d084f9a18c23e1d85c498febbc0e2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Citizens_Index), @"mvc.1.0.view", @"/Views/Citizens/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Citizens/Index.cshtml", typeof(AspNetCore.Views_Citizens_Index))]
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
#line 1 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\_ViewImports.cshtml"
using Sistema_de_votacion;

#line default
#line hidden
#line 2 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\_ViewImports.cshtml"
using Sistema_de_votacion.Models;

#line default
#line hidden
#line 3 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\_ViewImports.cshtml"
using Sistema_de_votacion.ViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd58adfabb7d084f9a18c23e1d85c498febbc0e2", @"/Views/Citizens/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3e121f28c0b078d49d17654fbd9ec3a8c3cd668", @"/Views/_ViewImports.cshtml")]
    public class Views_Citizens_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Sistema_de_votacion.Models.Citizen>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(138, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
  
    ViewData["Title"] = "Index";
    var disabled= await electionService.VerifyElectionOpenAsync() ? "disabled" : "";

#line default
#line hidden
            BeginContext(267, 172, true);
            WriteLiteral("\r\n<h1 class=\"display-4 text-sm-center mb-4\">Lista de Ciudadanos Inscritos en el Padron.</h1>\r\n<br />\r\n\r\n<div class=\"d-flex justify-content-between mt-4\">\r\n    <p>\r\n        ");
            EndContext();
            BeginContext(439, 81, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd58adfabb7d084f9a18c23e1d85c498febbc0e27065", async() => {
                BeginContext(504, 12, true);
                WriteLiteral("Añadir nuevo");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 469, "btn", 469, 3, true);
            AddHtmlAttributeValue(" ", 472, "btn-outline-success", 473, 20, true);
#line 14 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
AddHtmlAttributeValue(" ", 492, disabled, 493, 9, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(520, 16, true);
            WriteLiteral("\r\n    </p>\r\n    ");
            EndContext();
            BeginContext(536, 355, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd58adfabb7d084f9a18c23e1d85c498febbc0e29095", async() => {
                BeginContext(588, 296, true);
                WriteLiteral(@"
        <label class=""form-check form-check-inline"">
            <input name=""activeParam"" type=""checkbox"" class=""form-control-sm"" />
            <span> Activos &nbsp;&nbsp;</span>
            <input class=""btn btn-sm btn-outline-info"" type=""submit"" value=""Buscar"" />
        </label>
    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(891, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
#line 24 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
 if (Model.Count() == 0)
{

#line default
#line hidden
            BeginContext(930, 345, true);
            WriteLiteral(@"    <div class=""alert alert-warning alert-dismissible fade show"" role=""alert"">
        <strong>Advertencia!</strong> No se encontro ningun ciudadano en el padron electoral.
        <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
            <span aria-hidden=""true"">&times;</span>
        </button>
    </div>
");
            EndContext();
#line 32 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
}

#line default
#line hidden
            BeginContext(1278, 160, true);
            WriteLiteral("\r\n\r\n<table class=\"table table-striped table-hover table-bordered text-center\">\r\n    <thead class=\"thead-dark\">\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1439, 39, false);
#line 39 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Dni));

#line default
#line hidden
            EndContext();
            BeginContext(1478, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1534, 40, false);
#line 42 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1574, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1630, 44, false);
#line 45 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.LastName));

#line default
#line hidden
            EndContext();
            BeginContext(1674, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1730, 41, false);
#line 48 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1771, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1827, 44, false);
#line 51 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.IsActive));

#line default
#line hidden
            EndContext();
            BeginContext(1871, 94, true);
            WriteLiteral("\r\n            </th>\r\n            <th>Acciones</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 57 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(2014, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2075, 38, false);
#line 61 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Dni));

#line default
#line hidden
            EndContext();
            BeginContext(2113, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2181, 39, false);
#line 64 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
            EndContext();
            BeginContext(2220, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2288, 43, false);
#line 67 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.LastName));

#line default
#line hidden
            EndContext();
            BeginContext(2331, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2399, 40, false);
#line 70 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
            EndContext();
            BeginContext(2439, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(2507, 43, false);
#line 73 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.IsActive));

#line default
#line hidden
            EndContext();
            BeginContext(2550, 85, true);
            WriteLiteral("\r\n                </td>\r\n                <td class=\"btn-block\">\r\n                    ");
            EndContext();
            BeginContext(2635, 87, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd58adfabb7d084f9a18c23e1d85c498febbc0e217908", async() => {
                BeginContext(2711, 7, true);
                WriteLiteral("Detalle");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 76 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
                                              WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2722, 24, true);
            WriteLiteral(" |\r\n                    ");
            EndContext();
            BeginContext(2746, 96, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd58adfabb7d084f9a18c23e1d85c498febbc0e220419", async() => {
                BeginContext(2832, 6, true);
                WriteLiteral("Editar");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 77 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
                                           WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 4, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2798, "btn", 2798, 3, true);
            AddHtmlAttributeValue(" ", 2801, "btn-sm", 2802, 7, true);
            AddHtmlAttributeValue(" ", 2808, "btn-primary", 2809, 12, true);
#line 77 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
AddHtmlAttributeValue(" ", 2820, disabled, 2821, 9, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2842, 24, true);
            WriteLiteral(" |\r\n                    ");
            EndContext();
            BeginContext(2866, 99, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd58adfabb7d084f9a18c23e1d85c498febbc0e223549", async() => {
                BeginContext(2953, 8, true);
                WriteLiteral("Eliminar");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 78 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
                                             WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 4, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2920, "btn", 2920, 3, true);
            AddHtmlAttributeValue(" ", 2923, "btn-sm", 2924, 7, true);
            AddHtmlAttributeValue(" ", 2930, "btn-danger", 2931, 11, true);
#line 78 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
AddHtmlAttributeValue(" ", 2941, disabled, 2942, 9, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2965, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 81 "C:\Users\jrosa\Documents\Proyectos C# y ASP.NET Core (En otra maquina)\NeshGogo\Sistema-de-votacion\Sistema-de-votacion\Views\Citizens\Index.cshtml"
        }

#line default
#line hidden
            BeginContext(3020, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Sistema_de_votacion.Services.Elections.IElectionService electionService { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Sistema_de_votacion.Models.Citizen>> Html { get; private set; }
    }
}
#pragma warning restore 1591
