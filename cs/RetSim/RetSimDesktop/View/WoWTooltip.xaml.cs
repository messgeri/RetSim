﻿using RetSim.Data;
using RetSim.Items;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace RetSimDesktop
{
    public partial class WoWTooltip : UserControl
    {

        public Window PlacementTarget
        {
            get => (Window)GetValue(PlacementTargetProperty);
            set => SetValue(PlacementTargetProperty, value);
        }

        public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register(
            "PlacementTarget",
            typeof(Window),
            typeof(WoWTooltip),
            new PropertyMetadata(new PropertyChangedCallback(PlacementTarget_PropertyChanged)));

        public int ItemId
        {
            get => (int)GetValue(ItemIdProperty);
            set => SetValue(ItemIdProperty, value);
        }

        public static readonly DependencyProperty ItemIdProperty = DependencyProperty.Register(
            "ItemId",
            typeof(int),
            typeof(WoWTooltip),
            new PropertyMetadata(new PropertyChangedCallback(TooltipSettings_PropertyChanged)));

        public int XOffset
        {
            get => (int)GetValue(XOffsetProperty);
            set => SetValue(XOffsetProperty, value);
        }

        public static readonly DependencyProperty XOffsetProperty = DependencyProperty.Register(
            "XOffset",
            typeof(int),
            typeof(WoWTooltip));

        public int YOffset
        {
            get => (int)GetValue(YOffsetProperty);
            set => SetValue(YOffsetProperty, value);
        }

        public static readonly DependencyProperty YOffsetProperty = DependencyProperty.Register(
            "YOffset",
            typeof(int),
            typeof(WoWTooltip));

        public WoWTooltip()
        {
            InitializeComponent();
            InitializeAsync();

            TooltipPopUp.SetBinding(Popup.HorizontalOffsetProperty, new Binding("XOffset")
            {
                Source = this,
            });
            TooltipPopUp.SetBinding(Popup.VerticalOffsetProperty, new Binding("YOffset")
            {
                Source = this,
            });
        }

        private async void InitializeAsync()
        {
            await Browser.EnsureCoreWebView2Async(null);
            Browser.DefaultBackgroundColor = System.Drawing.Color.Transparent;
            Browser.NavigateToString(@"<head><script>const whTooltips = {colorLinks: false, iconizeLinks: false, renameLinks: false};</script><script src=""https://wow.zamimg.com/widgets/power.js""></script></head><body><script>function test(xPos, yPos) {const event = new MouseEvent('mouseover', {view: window,bubbles: true,cancelable: true, clientX : xPos, clientY : yPos});const cb = document.getElementById('placeholder');const cancelled = !cb.dispatchEvent(event);}</script><a id=""placeholder""></a></body>");
            await Browser.ExecuteScriptAsync("document.querySelector('body').style.overflow='hidden'");
        }

        private static void PlacementTarget_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WoWTooltip wowTooltip)
            {
                wowTooltip.PlacementTarget.MouseMove += wowTooltip.Parent_MouseMove;
                wowTooltip.PlacementTarget.SizeChanged += wowTooltip.Parent_SizeChanged;
                wowTooltip.Browser.Height = Math.Max(wowTooltip.PlacementTarget.ActualWidth - wowTooltip.TooltipPopUp.HorizontalOffset - 20, 0);
                wowTooltip.Browser.Width = Math.Max(wowTooltip.PlacementTarget.ActualHeight - wowTooltip.TooltipPopUp.HorizontalOffset + 40, 0);
            }
        }

        public static void TooltipSettings_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WoWTooltip wowTooltip && wowTooltip.DataContext is RetSimUIModel retSimUIModel)
            {
                wowTooltip.TooltipPopUp.IsOpen = wowTooltip.ItemId != 0;
                string gemString = "";

                if (Items.AllItems.ContainsKey(wowTooltip.ItemId) && MediaMetaData.ItemsMetaData.ContainsKey(wowTooltip.ItemId))
                {
                    var item = Items.AllItems[wowTooltip.ItemId];
                    if(item.Socket1 != null)
                    {
                        gemString += "&gems=";

                        List<Socket> sockets = new(item.Sockets);
                        foreach (var socketColor in MediaMetaData.ItemsMetaData[item.ID].AlternativeGemOrder)
                        {
                            for (int i = 0; i < sockets.Count; i++) 
                            {
                                if(sockets[i].Color == socketColor)
                                {
                                    if(sockets[i].SocketedGem != null) 
                                    {
                                        gemString += $"{sockets[i].SocketedGem.ID}:";
                                    }
                                    else
                                    {
                                        gemString += "0:";
                                    }
                                    sockets.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                }

                string enchantString = "";

                if (Items.AllItems.ContainsKey(wowTooltip.ItemId) && retSimUIModel.SelectedGear != null)
                {
                    switch (Items.AllItems[wowTooltip.ItemId].Slot)
                    {
                        case Slot.Head:
                            if (retSimUIModel.SelectedGear.HeadEnchant != null && retSimUIModel.SelectedGear.HeadEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.HeadEnchant.EnchantmentID}";
                            break;
                        case Slot.Shoulders:
                            if (retSimUIModel.SelectedGear.ShouldersEnchant != null && retSimUIModel.SelectedGear.ShouldersEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.ShouldersEnchant.EnchantmentID}";
                            break;
                        case Slot.Back:
                            if (retSimUIModel.SelectedGear.BackEnchant != null && retSimUIModel.SelectedGear.BackEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.BackEnchant.EnchantmentID}";
                            break;
                        case Slot.Chest:
                            if (retSimUIModel.SelectedGear.ChestEnchant != null && retSimUIModel.SelectedGear.ChestEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.ChestEnchant.EnchantmentID}";
                            break;
                        case Slot.Wrists:
                            if (retSimUIModel.SelectedGear.WristsEnchant != null && retSimUIModel.SelectedGear.WristsEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.WristsEnchant.EnchantmentID}";
                            break;
                        case Slot.Hands:
                            if (retSimUIModel.SelectedGear.HandsEnchant != null && retSimUIModel.SelectedGear.HandsEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.HandsEnchant.EnchantmentID}";
                            break;
                        case Slot.Legs:
                            if (retSimUIModel.SelectedGear.LegsEnchant != null && retSimUIModel.SelectedGear.LegsEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.LegsEnchant.EnchantmentID}";
                            break;
                        case Slot.Feet:
                            if (retSimUIModel.SelectedGear.FeetEnchant != null && retSimUIModel.SelectedGear.FeetEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.FeetEnchant.EnchantmentID}";
                            break;
                        case Slot.Finger:
                            if(retSimUIModel.TooltipSettings.RingEnchant != null && retSimUIModel.TooltipSettings.RingEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.TooltipSettings.RingEnchant.EnchantmentID}";
                            break;
                        case Slot.Weapon:
                            if (retSimUIModel.SelectedGear.WeaponEnchant != null && retSimUIModel.SelectedGear.WeaponEnchant.ID != -1)
                                enchantString += $"&ench={retSimUIModel.SelectedGear.WeaponEnchant.EnchantmentID}";
                            break;
                        default:
                            break;
                    }
                }
                wowTooltip.UpdateTooltip(wowTooltip.ItemId, gemString, enchantString);
            }
        }


        private void Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Browser.Width = Math.Max(PlacementTarget.ActualWidth - TooltipPopUp.HorizontalOffset - 20, 0);
            Browser.Height = Math.Max(PlacementTarget.ActualHeight - TooltipPopUp.HorizontalOffset + 40, 0);
        }

        private async void UpdateTooltip(int itemId, string gemString, string enchantString)
        {
            if (Browser != null)
            {
                await Browser.EnsureCoreWebView2Async(null);
                await Browser.ExecuteScriptAsync(@"document.getElementById('placeholder').href = ""https://tbc.wowhead.com/item=" + itemId + enchantString + gemString + @""";");
                //var heightString = await Browser.ExecuteScriptAsync(@"document.getElementsByClassName(""wowhead-tooltip"")[0].clientHeight");
                //var widthString = await Browser.ExecuteScriptAsync(@"document.getElementsByClassName(""wowhead-tooltip"")[0].clientWidth");
                //await Browser.ExecuteScriptAsync("test(35, 0);");
                await Browser.ExecuteScriptAsync("document.querySelector('body').style.overflow='hidden'");
            }
        }

        private async void UpdateTooltipPosition(Point position)
        {
            if (Browser != null)
            {
                await Browser.EnsureCoreWebView2Async(null);
                await Browser.ExecuteScriptAsync("test(" + (position.X - TooltipPopUp.HorizontalOffset) + ", " + (position.Y - TooltipPopUp.VerticalOffset) + ");");
            }
        }

        private void Parent_MouseMove(object sender, MouseEventArgs e)
        {
            TooltipPopUp.IsOpen = ItemId != 0;

            UpdateTooltipPosition(e.GetPosition(PlacementTarget));
        }
    }
}
