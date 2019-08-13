using Articles.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Articles.Abstractions
{
    public class HtmlGenerator
    {
        private readonly AbstractParentItem root;

        public HtmlGenerator(AbstractParentItem root)
        {
            this.root = root;
        }

        // <li>
        //    <span class="caret">Каталог статей</span>
        //    <ul class="nested">
        //        <li>Cтатья</li>
        //        <li>Статья</li>
        //        <li>
        //            <span class="caret">Еще каталог</span>
        //            <ul class="nested">
        //                <li>Black Tea</li>
        //                <li>White Tea</li>
        //                <li>
        //                    <span class="caret">Green Tea</span>
        //                    <ul class="nested">
        //                        <li>Sencha</li>
        //                        <li>Gyokuro</li>
        //                        <li>Matcha</li>
        //                        <li>Pi Lo Chun</li>
        //                    </ul>
        //                </li>
        //            </ul>
        //        </li>
        //        <li>
        //            <span class="caret">Еще каталог</span>
        //            <ul class="nested">
        //                <li>Black Tea</li>
        //                <li>White Tea</li>
        //                <li>
        //                    <span class="caret">Green Tea</span>
        //                    <ul class="nested">
        //                        <li>Sencha</li>
        //                        <li>Gyokuro</li>
        //                        <li>Matcha</li>
        //                        <li>Pi Lo Chun</li>
        //                    </ul>
        //                </li>
        //            </ul>
        //        </li>
        //    </ul>
        //</li>
        public string GenerateMeny()
        {
            StringBuilder returnHtml = new StringBuilder();
            returnHtml.Append(ParentHtml(root));
            return returnHtml.ToString();
        }

        private string ChildHtml(AbstractMenyItem item)
        {
            return $"<li>{item.Name}</li>";
        }

        private string ParentHtml(AbstractParentItem item)
        {
            StringBuilder html = new StringBuilder($"<li> <span class='caret'>{item.Name}</span>  <ul class='nested'>");
            foreach (var child in item.ToList())
            {
                if (child is AbstractParentItem)
                    html.Append(ParentHtml((AbstractParentItem)child));
                else
                    html.Append(ChildHtml(child));               
            }
            html.Append("</ul></li>");
            return html.ToString();
        }
    }
}