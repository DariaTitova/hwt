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
                    returnHtml.Append(divider);
                }

            return returnHtml.ToString();
        }

        //IParentItem для элементов, у которых есть вложенные элементы
        //IShownItm для элементов, которые можно открывать в правом окне
        //IEditebleItem для элементов, которые можно редактировать
        private string BuildHtml(IMenyItem item)
        {
            TagBuilder htmlTag = (item is IParentItem) ? ParentTag((IParentItem)item) : ChildTag(item);
                                    
          

            if (item is IEditableItem)
            {
                htmlTag.InnerHtml += ButtonEdit((IEditableItem)item);
                htmlTag.InnerHtml += ButtonDelete((IEditableItem)item);
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

            if (item is IShownItem shown)
            {
                a.MergeAttribute("onclick", "openPartial('" + shown.ShowView() + "')");
            }

            tag.InnerHtml += a;

            return tag;
        }



        private TagBuilder ButtonEdit(IEditableItem item)
        {
            return ButtonBuilder("primary", item.ChangeView(), "pencil");
        }

        private TagBuilder ButtonDelete(IEditableItem item)
        {
            return ButtonBuilder("danger", item.DeleteView(), "trash");
        }

        private TagBuilder ButtonBuilder(string color, string onClickHref, string icon )
        {
            var buttontag = new TagBuilder("p");
            buttontag.AddCssClass($"btn btn-{color} btn-xs");
            buttontag.MergeAttribute("onclick", "openPartial('" + onClickHref + "')");


            var spantag = new TagBuilder("span");
            spantag.AddCssClass($"glyphicon glyphicon-{icon}");

            buttontag.InnerHtml += spantag;

            return buttontag;
        }


    }
}