using KF.WPF.Client.Core.APIClient.RestServices;
using KF.WPF.Client.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
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

        #region Commands

        public DelegateCommand AddUserCommand { get; private set; }

        private async void AddUser()
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

        public DelegateCommand UpdateUserCommand { get; private set; }

        private async void UpdateUser()
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
            await userRestService.UpdateUserAsync(userId, SelectedUser);

            //refresh lista
            await GetUsers();
        }

        public DelegateCommand DeleteUserCommand { get; private set; }

        private async void DeleteUser()
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

        #endregion

        public UserViewViewModel(UserRestService userRestService)
        {
            this.userRestService = userRestService;
            AddUserCommand = new DelegateCommand(AddUser);
            UpdateUserCommand = new DelegateCommand(UpdateUser);
            DeleteUserCommand = new DelegateCommand(DeleteUser);
            Task.Run(() => this.Initialize()).Wait();
        }

        private async Task Initialize()
        {
            await GetUsers();
        }

        private async Task GetUsers()
        {
            Users = new ObservableCollection<UserModel>(await userRestService.GetAllUsersAsync());
        }

        

        



        


    }
}
