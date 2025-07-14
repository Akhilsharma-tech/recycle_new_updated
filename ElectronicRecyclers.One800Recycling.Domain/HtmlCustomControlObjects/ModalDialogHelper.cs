namespace ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects
{
    public class ModalDialogHelper
    {
        public ModalDialogConfirmation Confirmation()
        {
            return new ModalDialogConfirmation();
        }

        public ModalDialogInfo Info()
        {
            return new ModalDialogInfo();
        }

        public ModalDialogCustom Custom()
        {
            return new ModalDialogCustom();
        }
    }
}