using System.Collections.Generic;

namespace NewsPortal.Models.ViewModels
{
	public class NewscastListViewModel
	{
		public IEnumerable<News> Newscast { get; set; }
		public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
