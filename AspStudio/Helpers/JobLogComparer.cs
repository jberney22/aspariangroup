using EagleApp.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EagleApp.Helpers
{
    public class JobLogComparer : IEqualityComparer<VWipReport>
    {
        public bool Equals(VWipReport x, VWipReport y)
        {
            
            return Equals(x.Id, y.Id);
            //return x.Id == y.Id && x.DateAddedStr != y.DateAddedStr;
        }

        public int GetHashCode([DisallowNull] VWipReport obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
