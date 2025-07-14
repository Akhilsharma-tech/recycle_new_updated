using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public class Panel : IHtmlContent
    {
        private static TagBuilder BuildWrapper(string cssClass)
        {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass(cssClass);
            return wrapper;
        }

        private readonly TagBuilder title;
        private readonly TagBuilder actions;
        private readonly TagBuilder body;

        public Panel()
        {
            title = BuildWrapper("portlet-title");
            actions = BuildWrapper("actions");
            body = BuildWrapper("portlet-body clearfix");
        }

        public Panel Title(string content)
        {
            var caption = BuildWrapper("caption");
            caption.InnerHtml.Append(content);
            title.InnerHtml.Clear();
            title.InnerHtml.AppendHtml(caption);
            return this;
        }

        public Panel Content(string content)
        {
            body.InnerHtml.Clear();
            body.InnerHtml.AppendHtml(content);
            return this;
        }

        public Panel Actions(string content)
        {
            actions.InnerHtml.Clear();
            actions.InnerHtml.AppendHtml(content);
            return this;
        }

        private TagBuilder Render()
        {
            var panel = BuildWrapper("row");
            var column = BuildWrapper("col-md-12");
            var contentWrapper = BuildWrapper("portlet blue-recycler box full-height-content");

            title.InnerHtml.AppendHtml(actions);

            contentWrapper.InnerHtml.AppendHtml(title);
            contentWrapper.InnerHtml.AppendHtml(body);
            column.InnerHtml.AppendHtml(contentWrapper);
            panel.InnerHtml.AppendHtml(column);

            return panel;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            Render().WriteTo(writer, encoder);
        }

        public override string ToString()
        {
            using var writer = new StringWriter();
            WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
