using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NewsPortal.Models.ViewModels;
using System.Collections.Generic;
/*
"PageLinkTagHelper.cs" sınıfı, sayfalama işlemleri için kullanılacak bir etiket yardımcısı oluşturmaktadır. Bu sınıf, "IUrlHelperFactory"
ve "ViewContext" arayüzlerini kullanarak, sayfalama işlemleri için gerekli olan bağlantıları oluşturur. "PagingInfo" ve "PageAction" adlı
özellikleri, sayfalama işlemleri için gerekli olan bilgileri sağlar. Etiket yardımcısı, "div" etiketi içinde kullanılmak üzere tasarlanmıştır
ve "page-model" adlı bir özniteliği vardır. Etiket yardımcısı, işlem yapılacak sayfa sayısı kadar bağlantı oluşturur ve bunları "div" etiketi
içine ekler. Bu sayede kullanıcı, sayfalama işlemlerini kolayca gerçekleştirebilir.
 */
namespace NewsPortal.Infrastructure
{
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper
	{
		private IUrlHelperFactory urlHelperFactory;
		public PageLinkTagHelper(IUrlHelperFactory helperFactory)
		{
			urlHelperFactory = helperFactory;
		}
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PagingInfo PageModel { get; set; }
		public string PageAction { get; set; }
        //*
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }
           = new Dictionary<string, object>();
        //*
        //-
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
		//-
        public override void Process(TagHelperContext context,
		TagHelperOutput output)
		{
			IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			TagBuilder result = new TagBuilder("div");
			for (int i = 1; i <= PageModel.TotalPages; i++)
			{
				TagBuilder tag = new TagBuilder("a");
                //*
                PageUrlValues["newsPage"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                //*

                //-
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage
                    ? PageClassSelected : PageClassNormal);
                }
				//-
                tag.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(tag);
			}
			output.Content.AppendHtml(result.InnerHtml);
		}
	}
}
