using System;

namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public class ModalDialogCustom : ModalDialog
    {
        public ModalDialogCustom()
        {
            IsHeaderVisible = true;
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
    }
}