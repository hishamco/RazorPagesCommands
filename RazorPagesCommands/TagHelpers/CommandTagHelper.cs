using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesCommands.TagHelpers
{
    [HtmlTargetElement("button", Attributes = CommandNameAttributeName)]
    [HtmlTargetElement("button", Attributes = CommandArgumentsAttributeName + "*")]
    public class CommandTagHelper : TagHelper
    {
        private const string CommandNameAttributeName = "asp-command";
        private const string CommandArgumentsAttributeName = "asp-command-";
        public const string CommandNameParam = "__command";

        [HtmlAttributeName(CommandNameAttributeName)]
        public string Name { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = CommandArgumentsAttributeName)]
        public IDictionary<string, string> Arguments { get; set; } =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pagePath = ViewContext.HttpContext.Request.Path.ToString();
            output.Attributes.Add("name", CommandNameParam);
            output.Attributes.Add("value", Name);

            if (Arguments != null && Arguments.Count != 0)
            {
                var queryString = string.Join("&", Arguments.Select(r => $"{r.Key}={r.Value}"));
                var formActionAttribute = output.Attributes["formaction"];

                if (formActionAttribute != null)
                {
                    output.Attributes.Remove(formActionAttribute);
                }

                output.Attributes.Add("formaction", $"{pagePath}?{queryString}");
            }
        }
    }
}
