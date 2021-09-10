using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Book.WEb.Helpers
{
    public static class BookHelpers
    {
        public static MvcHtmlString BookView(this HtmlHelper helper, Model.Book book)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.AddCssClass("card");
            
            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("card-body");
            innerDiv.InnerHtml = 
                $"{book.Title}";
            mainDiv.InnerHtml =
                $"<h2>{book.Id}/{book.Title}/{book.Year}</h2>" + 
                innerDiv.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(mainDiv.ToString(TagRenderMode.Normal));
        }
    }
}