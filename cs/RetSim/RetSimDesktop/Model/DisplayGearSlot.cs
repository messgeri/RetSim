﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetSimDesktop.Model
{
    public class DisplayGearSlot : BindableBase
    {
        private IEnumerable<DisplayGear> shownItems;
        private IEnumerable<DisplayGear> allItems;
        private string searchWord = "";
        public IEnumerable<DisplayGear> ShownItems
        {
            get => shownItems;
            set => SetProperty(ref shownItems, value);  
        }
        public IEnumerable<DisplayGear> AllItems
        {
            get => allItems;
            set
            {
                SetProperty(ref allItems, value);
                ShownItems = value;
                SearchWord = "";
            }
        }

        public string SearchWord
        {
            get => searchWord;
            set
            {
                if(value != null && value != searchWord)
                {
                    SetProperty(ref searchWord, value);
                    FilterItems(searchWord);
                }
            }
        }

        public void FilterItems(string pattern)
        {
            ShownItems = allItems.Where(display => display.Item.Name.Contains(pattern, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
