using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Articles.Interfaces
{
    public class HtmlGeneratorMeny
    {
        private readonly List<IParentItem> rootsItems;
        public bool ShowEdit = true;

        public HtmlGeneratorMeny(List<IParentItem> root)
        {
            this.rootsItems = root;
        }

        public string GenerateMeny()
        {
            StringBuilder returnHtml = new StringBuilder();

            if (rootsItems!=null && rootsItems.Count > 0)
                foreach (var root in rootsItems)
                {
                    returnHtml.Append(BuildHtml(root));
                }
            return returnHtml.ToString();
        }

        //IParentItem для элементов, у которых есть вложенные элементы
        //IShownItm для элементов, которые можно открывать в правом окне
        //IEditebleItem для элементов, которые можно редактировать
        private string BuildHtml(IMenyItem item)
        {
            var mainLi = (item is IParentItem) ? ParentTag((IParentItem)item) : ChildTag(item);
            return mainLi.ToString();
        }

        private TagBuilder ParentTag(IParentItem parent)
        {
            var mainTag = new TagBuilder("li");

            var label = new TagBuilder("label");
            label.SetInnerText(parent.MenyText());
            label.AddCssClass(" nav-header");


            var editButtons = new TagBuilder("div");

            if (parent is IEditableItem && ShowEdit)
                editButtons.InnerHtml += EditTag((IEditableItem)parent);

 


                                          
            var children = new TagBuilder("ul");
 
            foreach (var child in parent.ToList())
                children.InnerHtml += BuildHtml(child);


            mainTag.InnerHtml += SetColoumns(label, editButtons,MakeRow(new TagBuilder("div")));
            mainTag.InnerHtml += MakeRow(children);




            return mainTag; 

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

            var editButtons = new TagBuilder("div");

            if (item is IEditableItem&&ShowEdit)
                editButtons = EditTag((IEditableItem)item);


            tag.InnerHtml += SetColoumns(a, editButtons, MakeRow(new TagBuilder("div")));
            return tag;
        }

        

        private TagBuilder EditTag(IEditableItem item)
        {
            var container = new TagBuilder("div");
            container.AddCssClass("form-group");


            container.InnerHtml += ButtonEdit(item);
            container.InnerHtml += ButtonDelete(item);

            return container;

        }

   

        private TagBuilder MakeRow(TagBuilder tag)
        {
            tag.AddCssClass("row");
            return tag;
        }

        private TagBuilder SetColoumns(TagBuilder column1, TagBuilder column2, TagBuilder parent)
        {
            column1.AddCssClass("col-md-8");
            column2.AddCssClass("col-md-4");
            parent.InnerHtml += column1;
            parent.InnerHtml += column2;
            return parent;
        }



        private TagBuilder ButtonEdit(IEditableItem item)
        {
            return ButtonBuilder("primary", item.ChangeView(), "pencil");
        }

        private TagBuilder ButtonDelete(IEditableItem item)
        {
            return ButtonBuilder("danger", item.DeleteView(), "trash", "delete - link");
        }

        private TagBuilder ButtonBuilder(string color, string onClickHref, string icon, string classstr="")
        {
            var buttontag = new TagBuilder("a");
            buttontag.AddCssClass($"btn btn-{color} btn-xs {classstr}");
            buttontag.MergeAttribute("onclick", "openPartial('" + onClickHref + "')");


            var spantag = new TagBuilder("span");
            spantag.AddCssClass($"glyphicon glyphicon-{icon}");

            buttontag.InnerHtml += spantag;

            return buttontag;
        }


    }
}