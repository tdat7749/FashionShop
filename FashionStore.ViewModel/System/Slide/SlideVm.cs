using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.Slide
{
    public class SlideVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public string CreatedAt { get; set; }
        public string Status { get; set; }
    }
}
