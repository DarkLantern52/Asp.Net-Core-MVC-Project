using System;
//"PagingInfo.cs" sınıfı, sayfalama bilgilerini içeren bir veri modelidir. Bu sınıf, sayfalama bilgilerini göstermek için kullanılacak
//olan görünümlerde kullanılabilir.
namespace NewsPortal.Models.ViewModels
{
	public class PagingInfo
	{
		public int TotalItems { get; set; }
		public int ItemsPerPage { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages =>
		(int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

	}
}
