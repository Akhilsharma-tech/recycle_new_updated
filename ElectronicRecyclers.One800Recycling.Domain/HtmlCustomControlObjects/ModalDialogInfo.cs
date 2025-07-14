using Microsoft.AspNetCore.Html;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public class ModalDialogInfo : ModalDialog
    {
        private void SetFooter()
        {
            base.Footer(BuildFooterCloseButton("Close").ToString());
        }

        public ModalDialogInfo()
        {
            IsHeaderVisible = false;
            SetFooter();
        }

        public override ModalDialog ConfirmedActionUrl(string url)
        {
            throw new InvalidOperationException("Confirmed action url is only used for " +
                                                "confirmation modal dialog.");
        }

        public override ModalDialog ConfirmButtonId(string id)
        {
            throw new InvalidOperationException("Confirmation button id is only used for " +
                                                "confirmation modal dialog.");
        }


        public ModalDialog Footer(Func<object, IHtmlContent> content)
        {
            throw new InvalidOperationException("Footer can't be set. Please use custom modal dialog.");
        }
        public override ModalDialog Footer(string content)
        {
            throw new InvalidOperationException("Footer can't be set. Please use custom modal dialog.");
        }
    }
}