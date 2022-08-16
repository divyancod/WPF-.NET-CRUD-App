using Prism.Commands;
using Prism.Mvvm;
using PrismWPFDemo.DbContext;
using PrismWPFDemo.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace PrismWPFDemo.ViewModels
{
    public class MyDashboardViewModel : BindableBase
    {
        private string? _firstName;
        public string? FirstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
            }
        }
        private string? _lastName;
        public string? LastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
            }
        }
        private string? _address;
        public string? Address { get { return _address; } set { SetProperty(ref _address, value); } }

        private string? _gender;
        public string? Gender { get { return _gender; } set { SetProperty(ref _gender, value); } }

        public int _selectedCity { get; set; }
        private int _selectedCountry { get; set; }
        private int _selectedState { get; set; }


        #region Country
        private ObservableCollection<CountryModel> country = new ObservableCollection<CountryModel>();
        public ObservableCollection<string?> Country
        {
            get
            {
                var list = new ObservableCollection<string?>(country.Select(x => x.Country).ToList());
                return list;
            }
        }
        public string? SelectedCountry
        {
            get
            {
                var nob = country.FirstOrDefault(x => x.Id == _selectedCountry);
                if (nob != null)
                {
                    return nob.Country;
                }
                return "";
            }
            set
            {
                var nob = country.FirstOrDefault(x => x.Country == value);
                if (nob != null)
                {
                    _selectedCountry = nob.Id;
                    state = new ObservableCollection<StateModel>(UserDbContext.GetStateList(nob.Id));
                    OnPropertyChanged(new PropertyChangedEventArgs("State"));
                }
            }
        }
        #endregion

        #region State
        private ObservableCollection<StateModel> state = new ObservableCollection<StateModel>();
        public ObservableCollection<string?> State
        {
            get
            {
                var list = new ObservableCollection<string?>(state.Select(x => x.State).ToList());
                return list;
            }
        }
        public string? SelectedState
        {
            get
            {
                var nob = state.FirstOrDefault(x => x.Id == _selectedState);
                if (nob != null)
                    return nob.State;
                return "";
            }
            set
            {
                var nob = state.FirstOrDefault(x => x.State == value);
                if (nob != null)
                {
                    _selectedState = nob.Id;
                    city = new ObservableCollection<CityModel>(UserDbContext.GetCityList(nob.Id));
                    OnPropertyChanged(new PropertyChangedEventArgs("City"));
                }
            }
        }
        #endregion

        #region City
        private ObservableCollection<CityModel> city = new ObservableCollection<CityModel>();
        public ObservableCollection<string?> City
        {
            get
            {
                var list = new ObservableCollection<string?>(city.Select(x => x.City).ToList());
                return list;
            }

        }
        public string? SelectedCity
        {
            get
            {
                var nob = city.FirstOrDefault(x => x.Id == _selectedCity);
                if (nob != null)
                    return nob.City;
                return "";
            }
            set
            {
                var nob = city.FirstOrDefault(x => x.City == value);
                if (nob != null)
                {
                    _selectedCity = nob.Id;
                }
            }
        }

        #endregion

        private DelegateCommand? _commandLoad;
        public DelegateCommand CommandLoad => _commandLoad ?? (_commandLoad = new DelegateCommand(CommandLoadExecute));

        public DelegateCommand? _commandClear;
        public DelegateCommand CommandClear => _commandClear ?? (_commandClear = new DelegateCommand(ClearTexts));

        private ObservableCollection<UsersModel> _usersList;

        public ObservableCollection<UsersModel> UsersList
        {
            get { return _usersList; }
            set
            {
                SetProperty(ref _usersList, value);
            }
        }

        public MyDashboardViewModel()
        {
            UserDbContext.fetchAllData();
            _usersList = new ObservableCollection<UsersModel>(UserDbContext.fetchUsers(null));
            //country = new ObservableCollection<CountryModel>(UserDbContext.countryList);
        }

        private void CommandLoadExecute()
        {
            if (!string.IsNullOrEmpty(_firstName) && !string.IsNullOrEmpty(_lastName)
                && !string.IsNullOrEmpty(_gender) && _selectedCountry != 0 && _selectedState != 0 && _selectedCity != 0
                && !string.IsNullOrEmpty(Address))
            {
                UsersModel model = new UsersModel(_firstName, _lastName, Convert.ToInt32(_gender), _selectedCountry, _selectedState, _selectedCity, Address);
                //UserDbContext.insertUser(model);
                UsersList = new ObservableCollection<UsersModel>(UserDbContext.fetchUsers(null));
                MessageBox.Show("User inserted successfully");
            }
            else
            {
                MessageBox.Show("Error");
            }

        }
        private void ClearTexts()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            Gender = string.Empty;
            //TODO empty country state city
        }
    }
}
