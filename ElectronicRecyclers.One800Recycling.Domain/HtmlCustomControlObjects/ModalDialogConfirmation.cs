using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Text;
using System.Text.Encodings.Web;

namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public class ModalDialogConfirmation : ModalDialog
    {
        private void SetFooter()
        {
            var content = new StringBuilder();
            content.Append(confirmationButton);
            content.Append(BuildFooterCloseButton("No"));
            base.Footer(content.ToString());
        }

        private static TagBuilder BuildConfirmationButton()
        {
            var button = new TagBuilder("a");
            button.AddCssClass("btn red-soft");
            button.InnerHtml.Append( "Yes");
            button.Attributes.Add("href", "javascript:;");
            return button;
        }

        private readonly TagBuilder confirmationButton;

        public ModalDialogConfirmation()
        {
            IsHeaderVisible = false;
            confirmationButton = BuildConfirmationButton();
            SetFooter();
        }

        public override ModalDialog Content(string content)
        {
            var h4 = new TagBuilder("h4");
            h4.AddCssClass("text-center");
            h4.InnerHtml.Append(content);

            using (var writer = new StringWriter())
            {
                h4.WriteTo(writer, HtmlEncoder.Default);
                return base.Content(writer.ToString());
            }
        }

        public ModalDialog Footer(Func<object, IHtmlContent> content)
        {
            throw new InvalidOperationException("Footer can't be set. Please use custom modal dialog.");
        }
        public override ModalDialog Footer(string content)
        {
            throw new InvalidOperationException("Footer can't be set. Please use custom modal dialog.");
        }

        public override ModalDialog ConfirmedActionUrl(string url)
        {
            confirmationButton.Attributes["href"] = url;
            SetFooter();
            return this;
        }

        public override ModalDialog ConfirmButtonId(string id)
        {
            confirmationButton.Attributes["id"] = id;
            SetFooter();
            return base.ConfirmButtonId(id);
        }
    }
}