using CandidateTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateTracker.Web
{
    public class LayoutPageAttribute : ActionFilterAttribute
    {
        private string _connectionString;

        public LayoutPageAttribute(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = (Controller)context.Controller;
            var repo = new CandidatesRepository(_connectionString);
            controller.ViewBag.pending = repo.GetPendingCount();
            controller.ViewBag.confirmed = repo.GetStatusCount(true);
            controller.ViewBag.declined = repo.GetStatusCount(false);

            base.OnActionExecuted(context);
        }
    }
}
