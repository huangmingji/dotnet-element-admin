using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace DotnetVue.ElementAdmin.Authentication
{
    public static class AjaxRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        public static bool IsAjax([NotNull]this HttpRequest request)
        {
            if (!request.Headers.ContainsKey(RequestedWithHeader))
            {
                return false;
            }

            return request.Headers[RequestedWithHeader] == XmlHttpRequest;
        }
    }
}
