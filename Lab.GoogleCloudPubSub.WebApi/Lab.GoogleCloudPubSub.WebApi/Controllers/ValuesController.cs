using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lab.GoogleCloudPubSub.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [Route("api/v1/messages")]
        [HttpPost]
        public async Task<PublishMessageResponse> PublishMessage(PublishMessageRequest request)
        {
            var publisher = await PublisherServiceApiClient.CreateAsync();

            var topicName = new TopicName(request.ProjectId, request.TopicId);

            var message = new PubsubMessage
            {
                Data = ByteString.CopyFromUtf8(request.Message)
            };

            var result = await publisher.PublishAsync(topicName, new[] { message });

            var response = new PublishMessageResponse
            {
                MessageId = result.MessageIds[0]
            };

            return response;
        }
    }

    public class PublishMessageRequest
    {
        public string ProjectId { get; set; }
        public string TopicId { get; set; }
        public string Message { get; set; }
    }

    public class PublishMessageResponse
    {
        public string MessageId { get; set; }
    }
}
