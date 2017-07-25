using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesCommands.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesCommands.Commands
{
    public abstract class CommandPageModel : PageModel
    {
        public Task<IActionResult> OnPostAsync()
        {
            var commandName = RouteData.Values["command"].ToString();
            var commandArgs = Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString(),
                StringComparer.OrdinalIgnoreCase);

            return InvokeCommandMethod(this, commandName, commandArgs) as Task<IActionResult>;
        }

        private static object InvokeCommandMethod(PageModel model, string name, IDictionary<string, string> args)
        {
            var commandMethod = model.GetType().GetMethod(name);
            var commandMethodParams = commandMethod.GetParameters();
            var @params = new List<object>();

            if (commandMethodParams.Any())
            {
                for (int i = 0; i < commandMethodParams.Count(); i++)
                {
                    var param = commandMethodParams[i];

                    if (args.ContainsKey(param.Name))
                    {
                        var formValue = args[param.Name].ToString();
                        var value = Convert.ChangeType(formValue, param.ParameterType);
                        @params.Add(value);
                    }
                }
            }

            return commandMethod.Invoke(model, @params.ToArray());       
        }
    }
}
