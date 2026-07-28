using Aqarak_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aqarak_WebAPI.Aqarak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IConversationService conversationService;
        private readonly IMessageService messageService;
        private readonly UserManager<AppUser> userManager;

        public ChatController(IConversationService conversationService, IMessageService messageService, UserManager<AppUser> userManager) 
        {
            this.conversationService = conversationService;
            this.messageService = messageService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send([FromBody] SendMessageDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var senderId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            await messageService.SendMessageAsync(
                model.ConversationId,
                senderId!,
                model.Content);

            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(int conversationId)
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var conversation = await conversationService
                .GetConversationAsync(conversationId, currentUserId!);

            if (conversation == null)
                return NotFound();

            return Ok (conversation);
        }

        [HttpDelete("{messageId}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var userId = userManager.GetUserId(User);
            var result = await messageService.DeleteMessageAsync(messageId, userId);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
