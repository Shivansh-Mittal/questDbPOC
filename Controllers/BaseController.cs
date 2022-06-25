using G.Core.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using QuestDbPOC.Models;
using Microsoft.AspNetCore.Mvc;

namespace QuestDbPOC.Controllers;

[Produces("application/json")]
[Route("api/Base")]
public class BaseController : Controller
{
    protected AppDbContext db = new AppDbContext();
    protected object InputData = null;
    protected string ErrorMessage = null;
    protected IDbConnection dapper = null;
    protected IMemoryCache cache;

    AppPrincipal _User = null;

    public BaseController()
    {
        dapper = new SqlConnection(Config.ConnectionString);
    }


    

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        cache = (IMemoryCache)HttpContext.RequestServices.GetService(typeof(IMemoryCache));

        ViewBag.User = User;
        HttpContext.User = User;
    }

   

    protected string IpAddress
    {
        get
        {
            return Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }

    //protected IActionResult Success(object value)
    //{
    //    LogApi(InputData, true);
    //    return Ok(value);
    //}

    protected IActionResult Error(int statusCode, string errorMessage)
    {
        //LogApi(InputData, false, errorMessage);
        return StatusCode(statusCode, ApiResult.Error(errorMessage));
    }

    protected IActionResult NotFoundError(string errorMessage)
    {
        return Error(StatusCodes.Status404NotFound, errorMessage);
    }

    protected IActionResult BadRequestError(string errorMessage)
    {
        return Error(StatusCodes.Status400BadRequest, errorMessage);
    }

    protected IActionResult InternalServerError(string errorMessage)
    {
        return Error(StatusCodes.Status500InternalServerError, errorMessage);
    }

    

    protected string GetFullUrl(string path)
    {
        return new Uri(new Uri(Request.Scheme + "://" + Request.Host.Value), Url.Content(path)).ToString();
    }

    protected DateTime GetIsraelTime()
    {
        return DateTime.UtcNow.AddHours(3);
    }

    protected DateTime ToIsraelTime(DateTime utcTime)
    {
        return utcTime.AddHours(2);
    }

    protected double? Round(double? val)
    {
        if (val.HasValue)
        {
            return Math.Round(val.Value, 2);
        }
        return null;
    }
}
