using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dapper;

namespace RatesDAL.Classes
{
    // SQL = [RatesBase]
    public class RatesBase : INotifyPropertyChanged
    {
        // Column [id] is part of, or is the primary key.
        private int _Id;
        public int Id
        {
            get
            {
                return (_Id);
            }
            set
            {
                _Id = value;
                RaisePropertyChanged("Id");
            }
        }

        private string _Currency;
        public string Currency
        {
            get
            {
                return (_Currency);
            }
            set
            {
                _Currency = value;
                RaisePropertyChanged("Currency");
            }
        }

        private Nullable<bool> _Trade;
        public Nullable<bool> Trade
        {
            get
            {
                return (_Trade);
            }
            set
            {
                _Trade = value;
                RaisePropertyChanged("Trade");
            }
        }

        private int _Country;
        public int Country
        {
            get
            {
                return (_Country);
            }
            set
            {
                _Country = value;
                RaisePropertyChanged("Country");
            }
        }

        private Nullable<DateTime> _Created;
        public Nullable<DateTime> Created
        {
            get
            {
                return (_Created);
            }
            set
            {
                _Created = value;
                RaisePropertyChanged("Created");
            }
        }

        private string _CUser;
        [Column(Name = "c_user")]
        public string CUser
        {
            get
            {
                return (_CUser);
            }
            set
            {
                _CUser = value;
                RaisePropertyChanged("CUser");
            }
        }

        private Nullable<DateTime> _Updated;
        public Nullable<DateTime> Updated
        {
            get
            {
                return (_Updated);
            }
            set
            {
                _Updated = value;
                RaisePropertyChanged("Updated");
            }
        }

        private string _UUser;
        [Column(Name = "u_user")]
        public string UUser
        {
            get
            {
                return (_UUser);
            }
            set
            {
                _UUser = value;
                RaisePropertyChanged("UUser");
            }
        }

        private Nullable<DateTime> _Deleted;
        public Nullable<DateTime> Deleted
        {
            get
            {
                return (_Deleted);
            }
            set
            {
                _Deleted = value;
                RaisePropertyChanged("Deleted");
            }
        }

        private string _DUser;
        [Column(Name = "d_user")]
        public string DUser
        {
            get
            {
                return (_DUser);
            }
            set
            {
                _DUser = value;
                RaisePropertyChanged("DUser");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
