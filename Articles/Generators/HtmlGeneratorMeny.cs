using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Articles.Interfaces
{
    public class HtmlGeneratorMeny
    {
        private readonly List<IParentItem> rootsItems;


        public HtmlGeneratorMeny(List<IParentItem> root)
        {
            this.rootsItems = root;
        }

        public string GenerateMeny()
        {
            StringBuilder returnHtml = new StringBuilder();

            if (rootsItems.Count > 0)
                foreach (var root in rootsItems)
                {
                    returnHtml.Append(BuildHtml(root));

                    var divider = new TagBuilder("li");
                    divider.AddCssClass("divider");
                    returnHtml.Append ( divider);
                }

            return returnHtml.ToString();
        }


        // <li>
        //    <label class="tree-toggle nav-header glyphicon-icon-rpad">
        //        <span class="glyphicon glyphicon-folder-close m5"></span>Bootstrap
        //        <span class="menu-collapsible-icon glyphicon glyphicon-chevron-down"></span>
        //    </label>
        //    <ul class="nav nav-list tree bullets">
        //        <li><a href = "#" > JavaScript </ a ></ li >
        //        < li >< a href="#">CSS<span class="badge">42</span></a></li>
        //        <li>
        //            <label class="tree-toggle nav-header">Buttons</label>
        //            <ul class="nav nav-list tree">
        //                <li><a href = "#" > Colors </ a ></ li >
        //                < li >< a href="#">Sizes</a></li>
        //                <li>
        //                    <label class="tree-toggle nav-header">Forms</label>
        //                    <ul class="nav nav-list tree">
        //                        <li><a href = "#" > Horizontal </ a ></ li >
        //                        < li >< a href="#">Vertical</a></li>
        //                    </ul>
        //                </li>
        //            </ul>
        //        </li>
        //    </ul>
        //</li>
        //<li class="divider"></li>

        //IParentItem для элементов, у которых есть вложенные элементы
        //IShownItm для элементов, которые можно открывать в правом окне
        //IEditebleItem для элементов, которые можно редактировать
        private string BuildHtml(IMenyItem item)
        {
            TagBuilder htmlTag;

            if (item is IParentItem)
            {
                htmlTag= ParentTag((IParentItem)item);

            }
            else
            {
                htmlTag = ChildTag(item);
            }

            if (item is IShownItem)
            {
                IShownItem shown = (IShownItem)item;
                htmlTag.MergeAttribute("onclick", "openPartial('"+shown.View()+"')");
            }

            if (item is IEditableItem)
            {
                //htmlTag.MergeAttribute("src", src);
                //htmlTag.MergeAttribute("alt", alt);
            }
           

            return htmlTag.ToString();
        }

        private TagBuilder ParentTag(IParentItem parent)
        {

            var tag = new TagBuilder("li");

            var label = new TagBuilder("label");
            label.AddCssClass("tree-toggle nav-header glyphicon-icon-rpad");
            label.SetInnerText(parent.MenyText());
            tag.InnerHtml += label;


            TagBuilder innerhtmlTag = new TagBuilder("ul");
            innerhtmlTag.AddCssClass("nav nav-list tree bullets");
            foreach (var child in parent.ToList())
                innerhtmlTag.InnerHtml += BuildHtml(child);

            tag.InnerHtml += innerhtmlTag;

            return tag;

         }


        private TagBuilder ChildTag(IMenyItem item)
        {
            var tag = new TagBuilder("li");

            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");
            a.SetInnerText(item.MenyText());

            tag.InnerHtml += a;

            return tag;


        }



    }
}