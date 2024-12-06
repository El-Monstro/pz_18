using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace pz_18
{
    public class Request : INotifyPropertyChanged
    {
        public int RequestID { get; set; }
        public string HomeTechType { get; set; }
        public string HomeTechModel { get; set; }
        public string ProblemDescription { get; set; }
        public string RequestStatus { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}