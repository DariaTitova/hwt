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
                    returnHtml.Append(BuildHtml(root));

            return returnHtml.ToString();
        }


        //IParentItem для элементов, у которых есть вложенные элементы
        //IShownItm для элементов, которые можно открывать в правом окне
        //IEditebleItem для элементов, которые можно редактировать
        private string BuildHtml(IMenyItem item)
        {
            TagBuilder htmlTag = new TagBuilder("li");
            htmlTag.InnerHtml += item.MenyText();


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

            if (item is IParentItem)
            {
                IParentItem parent = (IParentItem)item;

                TagBuilder innerhtmlTag = new TagBuilder("ul");
                innerhtmlTag.AddCssClass("nested");

                foreach (var child in parent.ToList())
                    innerhtmlTag.InnerHtml += BuildHtml(child);

                htmlTag.InnerHtml += innerhtmlTag;
            }

            return htmlTag.ToString();
        }



    }
}