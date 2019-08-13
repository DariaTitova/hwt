using Articles.interfaces;
using Articles.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Articles.Abstractions
{
    public class HtmlGenerator
    {
        private readonly IParentItem root;

        public HtmlGenerator(IParentItem root)
        {
            this.root = root;
        }

        public string GenerateMeny()
        {
            StringBuilder returnHtml = new StringBuilder();
            returnHtml.Append(ParentHtml(root));
            return returnHtml.ToString();
        }

        private string ChildHtml(IMenyItem item)
        {
            return $"<li  class='link'>{item.Name()}</li>";
        }

        private string ParentHtml(IParentItem item)
        {
            StringBuilder html = new StringBuilder($"<li> <span class='caret'>{item.Name()}</span>  <ul class='nested'>");
            foreach (var child in item.ToList())
            {
                if (child is IParentItem)
                    html.Append(ParentHtml((IParentItem)child));
                else
                    html.Append(ChildHtml(child));               
            }
            html.Append("</ul></li>");
            return html.ToString();
        }      
    }
}