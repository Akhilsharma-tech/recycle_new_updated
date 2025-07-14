using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Domain;
using ElectronicRecyclers.One800Recycling.Domain.HtmlCustomControlObjects;
using ElectronicRecyclers.One800Recycling.Application.Common;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicRecyclers.One800Recycling.Web.Helpers
{
    public static class PagerBuilder
    {
        private const int VisiblePageItems = 10;

        public static Pager CreatePager(
            IPagedList list, 
            UrlHelper urlHelper, 
            RouteValueDictionary routeValues,
            FormMethod formMethod,
            string controllerAction,
            string pageIndexHiddenFieldId)
        {
            var pagerItemBuilder = new PagerItemBuilder(urlHelper, routeValues);
            var items = new List<PagerItem>();
            var pageIndex = list.PageIndex;
            var totalPages = list.TotalPages;
            
            var startIndex = 1;
            var endIndex = totalPages;

            if (totalPages > VisiblePageItems)
            {
                startIndex = pageIndex - 4;
                endIndex = pageIndex + 4;

                if (startIndex <= 0)
                {
                    startIndex = 1;
                    endIndex = VisiblePageItems;
                }

                if (endIndex > totalPages)
                {
                    endIndex = totalPages;
                    startIndex = totalPages - VisiblePageItems;
                }
            }
       
            for (var i = startIndex; i <= endIndex; i++)
                items.Add(pagerItemBuilder.Build(i, i == pageIndex));

            var totalRows = list.TotalCount;
            var pageSize = list.PageSize;

            var pager = new Pager
            {
                Items = items, 
                TotalCount = totalRows,
                TotalPages = totalPages,
                PageIndex = pageIndex,
                FormMethod = formMethod,
                PageSize = pageSize,
                ControllerAction = controllerAction,
                PageIndexHiddenFieldId = pageIndexHiddenFieldId
            };

            if (list.HasPreviousPage)
            {
                pager.PreviousPage = pagerItemBuilder.Build(pageIndex - 1);
                pager.FirstPage = pagerItemBuilder.Build(1);
            }

            if (list.HasNextPage)
            {
                pager.NextPage = pagerItemBuilder.Build(pageIndex + 1);
                pager.LastPage = pagerItemBuilder.Build(totalPages);
            }

            return pager;
        }

        public static Pager CreatePager(IPagedList list, UrlHelper urlHelper, RouteValueDictionary routeValues)
        {
            return CreatePager(list, urlHelper, routeValues, FormMethod.Get, string.Empty, string.Empty);
        }

        public static Pager CreatePager(IPagedList list, UrlHelper urlHelper, FormMethod formMethod)
        {
            return CreatePager(list, urlHelper, new RouteValueDictionary(), formMethod, string.Empty, string.Empty);
        }

        public static Pager CreatePager(IPagedList list, UrlHelper urlHelper, FormMethod formMethod, string controllerAction)
        {
            return CreatePager(list, urlHelper, new RouteValueDictionary(), formMethod, controllerAction, string.Empty);
        }

        public static Pager CreatePager(
            IPagedList list, 
            UrlHelper urlHelper, 
            FormMethod formMethod, 
            string controllerAction,
            string pageIndexHiddenFieldId)
        {
            return CreatePager(list, urlHelper, new RouteValueDictionary(), formMethod, controllerAction, pageIndexHiddenFieldId);
        }
    }

    public class PagerItemBuilder
    {
        private readonly UrlHelper urlHelper;
        private readonly string controller;
        private  readonly string action;
        private readonly RouteValueDictionary routeValues;

        public PagerItemBuilder(UrlHelper urlHelper, RouteValueDictionary routeValues)
        {
            controller = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
            action = urlHelper.ActionContext.RouteData.Values["action"].ToString();
            this.urlHelper = urlHelper;
            this.routeValues = routeValues;
        }

        public PagerItem Build(int pageIndex, bool isActive = false)
        {
            routeValues["pageIndex"] = pageIndex;

            return new PagerItem
            {
                PageIndex = pageIndex,
                Url = urlHelper.Action(action, controller, routeValues),
                IsActive = isActive
            };
        }
    }
}