using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.SharedLibirary.Responses
{
    public record  Response(bool flag = false, string Message = null!);
}
