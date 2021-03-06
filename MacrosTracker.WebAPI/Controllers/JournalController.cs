using MacrosTracker.Models.JournaleEntrymodels;
using MacrosTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MacrosTracker.WebAPI.Controllers
{
    [Authorize]
    public class JournalController : ApiController
    {
        private JournalServices CreateJournalService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var journalService = new JournalServices(userId);
            return journalService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            JournalServices journalService = CreateJournalService();
            var journals = journalService.GetJournalEntries();
            return Ok(journals);
        }

        [HttpPost]
        public IHttpActionResult Post(JournalEntryCreate journal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateJournalService();

            if (!service.CreateJournalEntry(journal))
                return InternalServerError();

            return Ok();
        }

        [HttpGet, Route("api/GetJournal")]
        public IHttpActionResult Get(int id)
        {
            JournalServices journalService = CreateJournalService();
            var journalEntry = journalService.GetJournalEntryById(id);
            return Ok(journalEntry);
        }

        public IHttpActionResult Put(JournalEntryEdit journal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateJournalService();

            if (!service.UpdateJournalEntry(journal))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateJournalService();
            if (!service.DeleteJournalEntry(id))
                return InternalServerError();

            return Ok();
        }
    }
}
