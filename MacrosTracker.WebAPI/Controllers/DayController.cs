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
        /// <summary>
        /// Create a new Day record.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public DayServices CreateDayService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var journalService = new DayServices(userId);
            return journalService;
        }

        /// <summary>
        /// Returns a list of Day records.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            DayServices dayService = CreateDayService();
            var days = dayService.GetAllDays();
            return Ok(days);
        }

        /// <summary>
        /// Returns details for the specified Day.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("api/GetDay")]
        public IHttpActionResult Get(int id)
        {
            DayServices dayService = CreateDayService();
            var dayEntry = dayService.GetDayById(id);
            return Ok(dayEntry);
        }
    }
}
