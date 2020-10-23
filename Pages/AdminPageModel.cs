using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Advanced.Pages
{
    [Authorize(Roles="Admins")]
    public class AdminPageModel : PageModel
    {
    }
}
