using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petrus.ViewModels
{
    public class RequestListViewModel
    {
        public IList<RequestDetailsViewModel> Requests { get; set; }
    }
}
