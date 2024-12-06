using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_18
{
    public interface IRequestModel
    {
        int RequestID { get; set; }
        string HomeTechType { get; set; }
        string HomeTechModel { get; set; }
        string ProblemDescription { get; set; }
        string RequestStatus { get; set; }
    }
}
