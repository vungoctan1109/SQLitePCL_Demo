﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class ListTransaction : Page
    {
        private List<Entity.PersonalTransaction> PersonalTransactions;
        public ListTransaction()
        {
            this.InitializeComponent();
            PersonalTransactions = Data.DatabaseInitialize.SelectAll();
            double totalMoney = 0;
            for (int i = 0; i < PersonalTransactions.Count; i++)
            {
                totalMoney += PersonalTransactions[i].Money;
            }
            total.Text = $"Total Money: {totalMoney} VND";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            PersonalTransactions = Data.DatabaseInitialize.Filter(startDate.Date.DateTime, endDate.Date.DateTime, Convert.ToInt32(category.Text));
            double totalMoney = 0;
            for (int i = 0; i < PersonalTransactions.Count; i++)
            {
                totalMoney += PersonalTransactions[i].Money;
            }
            total.Text = $"Total Money: {totalMoney} VND";
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = PersonalTransactions;
            
        }
    }
}
