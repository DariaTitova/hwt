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


        //<div style = "overflow-y: scroll; overflow-x: hidden; height: 500px;" >
        //        < ul class="nav nav-list">
        //            <li><label class="tree-toggler nav-header">Header 1</label>
        //                <ul class="nav nav-list tree">
        //                    <li><a href = "#" > Link </ a ></ li >
        //                    < li >< a href="#">Link</a></li>
        //                    <li><label class="tree-toggler nav-header">Header 1.1</label>
        //                        <ul class="nav nav-list tree">
        //                            <li><a href = "#" > Link </ a ></ li >
        //                            < li >< a href="#">Link</a></li>
        //                            <li><label class="tree-toggler nav-header">Header 1.1.1</label>
        //                                <ul class="nav nav-list tree">
        //                                    <li><a href = "#" > Link </ a ></ li >
        //                                    < li >< a href="#">Link</a></li>
        //                                </ul>
        //                            </li>
        //                        </ul>
        //                    </li>
        //                </ul>
        //            </li>
        //            <li class="divider"></li>
        //            <li><label class="tree-toggler nav-header">Header 2</label>
        //                <ul class="nav nav-list tree">
        //                    <li><a href = "#" > Link </ a ></ li >
        //                    < li >< a href="#">Link</a></li>
        //                    <li><label class="tree-toggler nav-header">Header 2.1</label>
        //                        <ul class="nav nav-list tree">
        //                            <li><a href = "#" > Link </ a ></ li >
        //                            < li >< a href="#">Link</a></li>
        //                            <li><label class="tree-toggler nav-header">Header 2.1.1</label>
        //                                <ul class="nav nav-list tree">
        //                                    <li><a href = "#" > Link </ a ></ li >
        //                                    < li >< a href="#">Link</a></li>
        //                                </ul>
        //                            </li>
        //                        </ul>
        //                    </li>
        //                    <li><label class="tree-toggler nav-header">Header 2.2</label>
        //                        <ul class="nav nav-list tree">
        //                            <li><a href = "#" > Link </ a ></ li >
        //                            < li >< a href="#">Link</a></li>
        //                            <li><label class="tree-toggler nav-header">Header 2.2.1</label>
        //                                <ul class="nav nav-list tree">
        //                                    <li><a href = "#" > Link </ a ></ li >
        //                                    < li >< a href="#">Link</a></li>
        //                                </ul>
        //                            </li>
        //                        </ul>
        //                    </li>
        //                </ul>
        //            </li>
        //        </ul>
        //    </div>

        //IParentItem для элементов, у которых есть вложенные элементы
        //IShownItm для элементов, которые можно открывать в правом окне
        //IEditebleItem для элементов, которые можно редактировать
        private string BuildHtml(IMenyItem item)
        {
            TagBuilder htmlTag = new TagBuilder("li");


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

                var label = new TagBuilder("label");
                label.AddCssClass("tree - toggler nav - header");
                label.SetInnerText(item.MenyText());
                htmlTag.InnerHtml += label;


                TagBuilder innerhtmlTag = new TagBuilder("ul");
                innerhtmlTag.AddCssClass("nav nav-list tree");
                foreach (var child in parent.ToList())
                    innerhtmlTag.InnerHtml += BuildHtml(child);
                htmlTag.InnerHtml += innerhtmlTag;

            }
            else
            {
                htmlTag.SetInnerText(item.MenyText());
            }

            return htmlTag.ToString();
        }



    }
}