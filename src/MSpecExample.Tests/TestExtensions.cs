using System.Web.Mvc;

namespace MSpecExample.Tests
{
    public static class TestExtensions
    {
        public static ViewResult is_a_view_and(this ActionResult result)
        {
            return result as ViewResult;
        }
    }
}