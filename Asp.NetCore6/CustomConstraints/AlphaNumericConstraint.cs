using System.Text.RegularExpressions;

namespace Asp.NetCore6.CustomConstraints
{
    public class AlphaNumericConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContext? httpContext, 
            IRouter? route, //name of route e.g /user/username
            string routeKey, //name of route parameter e.g username
            RouteValueDictionary values, //all paratermeters specified
            RouteDirection routeDirection) //An enum either incomming request or url
        {
            //check if values or aphanumeric
            if (!values.ContainsKey(routeKey))
            {
                return false;
            }
            //Custom route constraint
            Regex regex = new Regex("^[a-zA-Z][a-zA-Z0-9]*$");
            string? userNameValue = Convert.ToString(values[routeKey]);
            if (regex.IsMatch(userNameValue)) 
            { 
                return true;
            }
            return false;
        }
    }
}
