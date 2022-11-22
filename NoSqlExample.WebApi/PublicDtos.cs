using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoSqlExample.WebApi
{
    public record PostDTO(
        int userId,
        int id,
        string title,
        string body
        );

}
