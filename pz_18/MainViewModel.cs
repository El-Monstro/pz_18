using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using pz_18;

namespace pz_18
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Request> Requests { get; set; }
        public ICommand LoadDataCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Requests = new ObservableCollection<Request>();
            LoadDataCommand = new RelayCommand(LoadData);
        }

        private void LoadData()
        {
            string connectionString = "Server=192.168.147.54;Database=Demos;User Id=is;Password=1;";

            Requests.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Requests";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Requests.Add(new Request
                            {
                                RequestID = (int)reader["requestID"],
                                HomeTechType = reader["homeTechType"].ToString(),
                                HomeTechModel = reader["homeTechModel"].ToString(),
                                ProblemDescription = reader["problemDescription"].ToString(),
                                RequestStatus = reader["requestStatus"].ToString()
                            });
                        }
                    }
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
