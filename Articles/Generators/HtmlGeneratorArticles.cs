using System.Text;

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
            return returnHtml.ToString();
        }        
    }
}