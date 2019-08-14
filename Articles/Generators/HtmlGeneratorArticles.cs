﻿using System.Text;

namespace Articles.Interfaces
{
    public class HtmlGeneratorArticles
    {
        private IShownItem article;

        public HtmlGeneratorArticles(IShownItem article)
        {
            this.article = article;
        }

        public string GenerateArticle()
        {
            StringBuilder returnHtml = new StringBuilder();
            returnHtml.Append(
                $"<h2>{article.Head()}</h2>"               
                );
            returnHtml.Append(
               $"<p>{article.Body()}</p>"
               );
            return returnHtml.ToString();
        }        
    }
}