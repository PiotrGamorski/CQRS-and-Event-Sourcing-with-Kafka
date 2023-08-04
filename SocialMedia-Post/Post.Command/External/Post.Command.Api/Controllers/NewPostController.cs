using CQRS.Core;
using Microsoft.AspNetCore.Mvc;
using Post.Command.Application.Commands.NewPostCommand;
using Post.Command.Application.DTOs;

namespace Post.Command.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewPostController: ControllerBase
    {
        private readonly ILogger<NewPostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewPostController(ILogger<NewPostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> NewPostAsync(NewPostCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;

                await _commandDispatcher.SendAsync(command);

                var result = new NewPostResponse
                {
                    Message = "New post creation completed successfully!"
                };

                return StatusCode(StatusCodes.Status201Created, new NewPostResponse { Message = "New post creation completed successfully!" });
            }
            catch (InvalidOperationException e)
            {
                _logger.Log(LogLevel.Warning, e, "Client made a bad request!");
                return BadRequest(new NewPostResponse { Message = e.Message });
            }
            catch(Exception e) 
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new post!";
                _logger.Log(LogLevel.Error, e, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new NewPostResponse
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE,
                });
            };
        }
    }
}
