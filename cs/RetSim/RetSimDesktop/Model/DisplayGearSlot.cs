using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetSimDesktop.Model
{
    public class DisplayGearSlot : BindableBase
    {
        private IEnumerable<DisplayGear> shownItems;
        private IEnumerable<DisplayGear> allItems;
        private string searchWord = "";
        private int delay = 200;
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
                if (SetProperty(ref searchWord, value))
                {
                    Task.Run(() => FilterItems(searchWord.Clone().ToString()));
                }
            }
        }

        public void FilterItems(string pattern)
        {
            Thread.Sleep(delay);
            if (searchWord == pattern)
                ShownItems = allItems.Where(display => display.Item.Name.Contains(pattern, StringComparison.InvariantCultureIgnoreCase));
            else
                return;
        }
    }
}
