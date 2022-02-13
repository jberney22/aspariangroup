using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.TagHelper
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("input", Attributes = ForAttributeName)]
    public class EagleTextBoxTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";
        [HtmlAttributeName("asp-is-readonly")]
        public bool IsDisabled { set; get; }

        public EagleTextBoxTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsDisabled)
            {
                var d = new TagHelperAttribute("readonly", "readonly");
                output.Attributes.Add(d);
            }
            base.Process(context, output);
        }

        
    }
}
