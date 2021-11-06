﻿using RetSim.Items;
using RetSimDesktop.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void RedSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Red, RetSim.Data.Items.Gems.Values);
        }
        private void YellowSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Yellow, RetSim.Data.Items.Gems.Values);
        }
        private void BlueSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Blue, RetSim.Data.Items.Gems.Values);
        }
        private void MetaSocketSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectGemForSocketColor(SocketColor.Meta, RetSim.Data.Items.MetaGems.Values);
        }

        private static void SelectGemForSocketColor(SocketColor color, IEnumerable<Gem> gemList)
        {
            GemPickerWindow gemPicker = new(gemList);
            if (gemPicker.ShowDialog() == true)
            {
                foreach (var item in RetSim.Data.Items.AllItems.Values)
                {
                    for (int i = 0; i < item.Sockets.Length; i++)
                    {
                        if (item.Sockets[i] != null && item.Sockets[i].Color == color)
                        {
                            item.Sockets[i].SocketedGem = gemPicker.SelectedGem;
                        }
                    }
                }
            }
        }
    }
}
