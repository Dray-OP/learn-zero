﻿using Asd.AbpZeroTemplate.Models.Users;
using Asd.AbpZeroTemplate.ViewModels;
using Xamarin.Forms;

namespace Asd.AbpZeroTemplate.Views
{
    public partial class UsersView : ContentPage, IXamarinView
    {
        public UsersView()
        {
            InitializeComponent();
        }

        public async void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            await ((UsersViewModel) BindingContext).LoadMoreUserIfNeedsAsync(e.Item as UserListModel);
        }
    }
}