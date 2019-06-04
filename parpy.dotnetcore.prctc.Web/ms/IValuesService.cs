using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parpy.dotnetcore.prctc.Web.ms
{
    public interface IValuesService
    {
        Task<string> GetValues();
    }
}
