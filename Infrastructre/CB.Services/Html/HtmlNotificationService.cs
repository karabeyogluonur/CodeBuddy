using CB.Application.Abstractions.Services.Html;
using CB.Domain.Enums.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Services.Html
{
    public class HtmlNotificationService : IHtmlNotificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        public HtmlNotificationService(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        protected virtual void PrepareTempData(HtmlNotificationType type, string message)
        {
            var context = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(context);

            var messageDictionary = new Dictionary<string, string>(){
                {"Message", message},
                {"Type", type.ToString()},
            };

            tempData["Notification"] = messageDictionary;
        }
        public void ErrorNotification(string message)
        {
            PrepareTempData(HtmlNotificationType.Error, message);
        }

        public void Notification(HtmlNotificationType type, string message)
        {
            PrepareTempData(type, message);
        }

        public void SuccessNotification(string message)
        {
            PrepareTempData(HtmlNotificationType.Success, message);
        }

        public void WarningNotification(string message)
        {
            PrepareTempData(HtmlNotificationType.Warning, message);
        }
    }
}
