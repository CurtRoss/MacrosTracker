using MacrosTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MacrosTracker.WebAPI.Controllers
{
    public class DayController : ApiController
    {
        public DayServices CreateDayService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var journalService = new DayServices(userId);
            return journalService;
        }

        public IHttpActionResult Get()
        {
            DayServices dayService = CreateDayService();
            var days = dayService.GetAllDays();
            return Ok(days);
        }

        [HttpGet, Route("api/GetDay")]
        public IHttpActionResult Get(int id)
        {
            DayServices dayService = CreateDayService();
            var dayEntry = dayService.GetDayById(id);
            return Ok(dayEntry);
        }
    }
}
