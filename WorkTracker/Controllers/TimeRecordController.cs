using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkTracker.Core.Endpoints;
using WorkTracker.Core.Records;

namespace WorkTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeRecordController : ControllerBase
    {
        private readonly IEndpoint<CreateRecord, Record> _createRecordEndpoint;

        public TimeRecordController(IEndpoint<CreateRecord, Record> createRecordEndpoint)
        {
            _createRecordEndpoint = createRecordEndpoint;
        }

        [HttpPost]
        public Task<Record?> Create(CreateRecord rec) => _createRecordEndpoint.Invoke(rec)
                .BiFoldAsync(
                    (Record?)null,
                    (u, record) => record,
                    (u, error) => null
                );
    }
}
