using KF.WPF.Client.Core.APIClient.RestServices;
using KF.WPF.Client.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KF.WPF.Client.Modules.User.ViewModels
{
    public class UserViewViewModel : BindableBase
    {
        #region Properties
        private readonly UserRestService userRestService;
        private string searchString;

        public string SearchString
        {
            get { return searchString; }
            set { SetProperty(ref searchString, value); }
        }

        private List<UserModel> allUsers;

        private ObservableCollection<UserModel> users;
        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        private UserModel selectedUser;
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { SetProperty(ref selectedUser, value); }
        }
        #endregion

        #region ctor

        public UserViewViewModel(UserRestService userRestService)
        {
            this.userRestService = userRestService;
            AddUserCommand = new DelegateCommand(AddUser);
            UpdateUserCommand = new DelegateCommand(UpdateUser);
            DeleteUserCommand = new DelegateCommand(DeleteUser);
            Task.Run(() => this.Initialize()).Wait();
        }

        #endregion

        #region Methods

        private async Task Initialize()
        {
            await GetUsers();
        }

        private async Task GetUsers()
        {
            Users = new ObservableCollection<UserModel>(await userRestService.GetAllUsersAsync());
            allUsers = new List<UserModel>(Users.AsEnumerable());
        }

        #endregion

        #region Commands

        private DelegateCommand _searchStringCommand;
        public DelegateCommand SearchStringCommand =>
            _searchStringCommand ?? (_searchStringCommand = new DelegateCommand(ExecuteSearchString, CanExecuteSearchString)).ObservesProperty(() => SearchString);

        async void ExecuteSearchString()
        {
            var searchUser = allUsers.Where(x => x.Username.ToLower().StartsWith(SearchString.ToLower()));
            Users.Clear();
            Users.AddRange(searchUser);
        }

        bool CanExecuteSearchString()
        {
            return true;
        }

        public DelegateCommand AddUserCommand { get; private set; }

        private async void AddUser()
        {
            try
            {
                //get new user
                UserModel newUser = Users.FirstOrDefault(s => s.UserId == Guid.Empty);

                if (newUser == null)
                {
                    MessageBox.Show("Please enter a new user in the grid.");
                    return;
                }

                // send user 
                await userRestService.CreateUserAsync(newUser);

                //refresh lista
                await GetUsers();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public DelegateCommand UpdateUserCommand { get; private set; }

        private async void UpdateUser()
        {
            try
            {
                //check if selected user
                if (SelectedUser == null)
                {
                    MessageBox.Show("Please select a user for update");
                    return;
                }

                //get selected user id
                Guid userId = selectedUser.UserId;

                //send request to update 

                var user = new UserModel
                {
                    UserId = selectedUser.UserId,
                    Username = selectedUser.Username,
                    Password = selectedUser.Password,
                    IsAdmin = selectedUser.IsAdmin,
                };
                await userRestService.UpdateUserAsync(userId, user);

                //refresh lista
                await GetUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public DelegateCommand DeleteUserCommand { get; private set; }

        private async void DeleteUser()
        {
            try
            {
                //check if selected user
                if (SelectedUser == null)
                {
                    MessageBox.Show("Please select a user");
                    return;
                }

                //get selected user id
                Guid userId = selectedUser.UserId;

                //send request to delete Id via rest service
                await userRestService.DeleteUserAsync(userId);

                //refresh lista
                await GetUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
