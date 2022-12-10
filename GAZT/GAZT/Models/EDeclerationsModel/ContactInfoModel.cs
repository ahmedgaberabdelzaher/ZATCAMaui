using System;
using GalaSoft.MvvmLight;

namespace EGAZT.Models.EDeclerationsModel
{
    public class ContactInfoModel: ViewModelBase
    {
        string _phoneNumber;
        public string phoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; RaisePropertyChanged(); } }

        string _address;
        public string address { get { return _address; } set { _address = value; RaisePropertyChanged(); } }

        string _email;
        public string email { get { return _email; } set { _email = value; RaisePropertyChanged(); } }

        bool _IsTermsChecked;
        public bool IsTermsChecked { get { return _IsTermsChecked; } set { _IsTermsChecked = value; RaisePropertyChanged(); } }
    }
}

