using CB.Domain.Enums.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Abstractions.Services.Html
{
    public interface IHtmlNotificationService
    {
        void Notification(HtmlNotificationType type, string message);

        void SuccessNotification(string message);

        void WarningNotification(string message);

        void ErrorNotification(string message);
    }
}
