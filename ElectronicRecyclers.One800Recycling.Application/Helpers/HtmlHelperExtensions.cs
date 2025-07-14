using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NHibernate.Mapping;
using static System.Net.Mime.MediaTypeNames;


namespace ElectronicRecyclers.One800Recycling.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent Link(this HtmlHelper helper, string text, string href, object htmlAttributes)
        {
            var tag = new TagBuilder("a");
                tag.InnerHtml.Append(text);

            if (string.IsNullOrEmpty(href) == false)
                tag.Attributes["href"] = href;

            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            foreach (var attribute in attributes)
            {
                var val = attribute.Value.ToString();
                if (string.IsNullOrEmpty(val) == false)
                    tag.Attributes[attribute.Key] = val;
            }

            return tag;
        }

        public static IHtmlContent Link(this HtmlHelper helper, string text, string href)
        {
            return Link(helper, text, href, new {});
        }

        public static MvcForm BeginForm(this HtmlHelper helper, FormMethod method, object htmlAttributes)
        {
            return helper.BeginForm(null, null, method, htmlAttributes);
        }

        public static ModalDialogHelper ModalDialog(this HtmlHelper helper)
        {
            return new ModalDialogHelper();
        }

        public static Panel Panel(this HtmlHelper helper)
        {
            return new Panel();
        }

        private static string ListItemToOption(GroupedSelectListItem item)
        {
            var option = new TagBuilder("option");
             option.InnerHtml.Append(item.Text);


            if (string.IsNullOrEmpty(item.Value) == false)
                option.Attributes["value"] = item.Value;

            if (item.Selected)
                option.Attributes["selected"] = "selected";

            var writer = new System.IO.StringWriter();
            option.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return writer.ToString();
        }

        public static IHtmlContent ListBoxFor<TModel, TProperty>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<GroupedSelectListItem> items)
        {
            Guard.Against<ArgumentNullException>(expression == null, "Expression is null.");

            var optGroups = new StringBuilder();

            items.GroupBy(item => item.Group)
                .ForEach(groupedItems =>
                {
                    var options = new StringBuilder();

                    foreach (var item in groupedItems)
                        options.AppendLine(ListItemToOption(item));

                    var optGroup = new TagBuilder("optGroup");
                    optGroup.InnerHtml.Append(optGroups.ToString());


                    optGroup.Attributes["label"] = groupedItems.Key;
                    optGroups.Append(optGroup);
                });

            var select = new TagBuilder("select");
            select.InnerHtml.Append(optGroups.ToString());


            var provider = new ModelExpressionProvider(new EmptyModelMetadataProvider());
            var name = provider.GetExpressionText(expression);
            var fullName = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            select.Attributes["name"] = fullName;
            select.GenerateId(fullName, "_");

            return select;
        }   
    }
}