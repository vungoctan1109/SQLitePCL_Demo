using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo_SQLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTransaction : Page
    {
        public CreateTransaction()
        {
            this.InitializeComponent();
            created_at.SelectedDate = DateTime.Today;
        }
        private async void Handle_Save(object sender, RoutedEventArgs e)
        {
            Entity.PersonalTransaction personalTransaction = new Entity.PersonalTransaction
            {
                Name = name.Text,
                Description = description.Text,
                Detail = detail.Text,
                Money = Convert.ToDouble(money.Text),
                CreatedAt = created_at.Date.DateTime,
                Category = Convert.ToInt32(category.Text)
            };
            Data.DatabaseInitialize.Save(personalTransaction);
            ContentDialog contentDialog = new ContentDialog
            {
                CloseButtonText = "Close"
            };
            contentDialog.Title = "Action success.";
            contentDialog.Content = "Transaction has been saved.";
            await contentDialog.ShowAsync();
        }

        private void Handle_Reset(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            description.Text = "";
            detail.Text = "";
            money.Text = "";
            created_at.SelectedDate = DateTime.Today;
            category.Text = "";
        }
    }
}
