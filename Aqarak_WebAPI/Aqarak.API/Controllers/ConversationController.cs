using Aqarak_WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aqarak_WebAPI.Aqarak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConversationService conversationService;

        public ConversationController(UserManager<AppUser> userManager, IConversationService conversationService)
        {
            this.userManager = userManager;
            this.conversationService = conversationService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetConversations()
        {
            var UserId = userManager.GetUserId(User);

            var Conversation = await conversationService.GetUserConversationsAsync(UserId);

            return Ok(Conversation);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrGetConversation([FromBody] int propertyId)
        {
            var UserId = userManager.GetUserId(User);
            var ConversationId = await conversationService.CreateOrGetConversationAsync(propertyId, UserId);
            return Ok(ConversationId);
        }

        [HttpGet("{conversationId}")]
        [Authorize]
        public async Task<IActionResult> GetConversation(int conversationId)
        {
            var UserId = userManager.GetUserId(User);
            var Conversation = await conversationService.GetConversationAsync(conversationId, UserId);
            if (Conversation == null)
                return NotFound();
            return Ok(Conversation);
        }

        [HttpDelete("{conversationId}")]
        [Authorize]
        public async Task<IActionResult> DeleteConversation(int conversationId)
        {
            var userId = userManager.GetUserId(User);

            var result = await conversationService.DeleteConversationAsync(conversationId, userId);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
