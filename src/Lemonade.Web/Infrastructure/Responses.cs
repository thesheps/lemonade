using System;
using System.Text.RegularExpressions;
using Nancy;

namespace Lemonade.Web.Infrastructure
{
    public class Responses
    {
        public static Response BadRequest(Exception ex)
        {
            return new Response
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = Regex.Replace(ex.StackTrace, @"\t|\n|\r", "")
            };
        }
    }
}