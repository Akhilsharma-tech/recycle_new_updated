using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;
using System.Text;

namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public abstract class ModalDialog : IHtmlContent
    {
        private readonly TagBuilder mainWrapper;
        private readonly TagBuilder dialogWrapper;
        private readonly TagBuilder contentWrapper;
        private readonly TagBuilder headerWrapper;
        private readonly TagBuilder bodyWrapper;
        private readonly TagBuilder footerWrapper;

        private static TagBuilder BuildWrapper(string cssClass)
        {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass(cssClass);
            return wrapper;
        }

        private static TagBuilder BuildMainWrapper()
        {
            var wrapper = BuildWrapper("modal fade margin-top-40");
            wrapper.Attributes.Add("tabindex", "-1");
            wrapper.Attributes.Add("role", "dialog");
            wrapper.Attributes.Add("aria-labelledby", "modalLabel");
            wrapper.Attributes.Add("aria-hidden", "true");
            return wrapper;
        }

        public bool IsHeaderVisible { get; set; }

        protected ModalDialog()
        {
            mainWrapper = BuildMainWrapper();
            dialogWrapper = BuildWrapper("modal-dialog");
            contentWrapper = BuildWrapper("modal-content");
            headerWrapper = BuildWrapper("modal-header bg-grey");
            bodyWrapper = BuildWrapper("modal-body");
            footerWrapper = BuildWrapper("modal-footer");
            IsHeaderVisible = true;
        }

        public virtual ModalDialog Id(string name)
        {
            mainWrapper.Attributes["id"] = name;
            return this;
        }

        public virtual ModalDialog Title(string title)
        {
            var closeButton = new TagBuilder("button");
            closeButton.Attributes["type"] = "button";
            closeButton.AddCssClass("close");
            closeButton.Attributes["data-dismiss"] = "modal";
            closeButton.Attributes["aria-hidden"] = "true";
            closeButton.InnerHtml.AppendHtml("&times;");

            var h4 = new TagBuilder("h4");
            h4.AddCssClass("modal-title");
            h4.Attributes["id"] = "modalLabel";
            h4.InnerHtml.Append(title);

            headerWrapper.InnerHtml.Clear();
            headerWrapper.InnerHtml.AppendHtml(closeButton);
            headerWrapper.InnerHtml.AppendHtml(h4);

            return this;
        }

        public virtual ModalDialog Content(string content)
        {
            bodyWrapper.InnerHtml.Clear();
            bodyWrapper.InnerHtml.AppendHtml(content);
            return this;
        }

        public virtual ModalDialog Footer(string content)
        {
            footerWrapper.InnerHtml.Clear();
            footerWrapper.InnerHtml.AppendHtml(content);
            return this;
        }

        public virtual ModalDialog ConfirmedActionUrl(string url)
        {
            return this;
        }

        public virtual ModalDialog ConfirmButtonId(string id)
        {
            return this;
        }

        protected TagBuilder BuildFooterCloseButton(string text)
        {
            var button = new TagBuilder("button");
            button.Attributes["type"] = "button";
            button.Attributes["data-dismiss"] = "modal";
            button.AddCssClass("btn btn-default");
            button.InnerHtml.Append(text);
            return button;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var content = new StringBuilder();

            if (IsHeaderVisible)
                content.Append(GetString(headerWrapper));

            content.Append(GetString(bodyWrapper));
            content.Append(GetString(footerWrapper));

            contentWrapper.InnerHtml.Clear();
            contentWrapper.InnerHtml.AppendHtml(content.ToString());

            dialogWrapper.InnerHtml.Clear();
            dialogWrapper.InnerHtml.AppendHtml(contentWrapper);

            mainWrapper.InnerHtml.Clear();
            mainWrapper.InnerHtml.AppendHtml(dialogWrapper);

            mainWrapper.WriteTo(writer, encoder);
        }

        private static string GetString(TagBuilder tagBuilder)
        {
            using var writer = new StringWriter();
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
