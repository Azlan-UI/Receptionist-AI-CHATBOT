using Microsoft.AspNetCore.Mvc;
using ReceptionistChatBot.Api.Contracts.Chat;
using ReceptionistChatBot.Application.DTOs.Chat;
using ReceptionistChatBot.Application.Interfaces.Services;

namespace ReceptionistChatBot.Api.Controllers;

[ApiController]
[Route("api/chat")]
public sealed class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly ILogger<ChatController> _logger;

    public ChatController(
        IChatService chatService,
        ILogger<ChatController> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    [HttpPost("send")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
    public async Task<ActionResult<ChatResponseDto>> SendAsync(
        [FromBody] SendChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received chat message for session {SessionId}.",
            request.SessionId);

        var response = await _chatService.SendMessageAsync(
            new ChatRequestDto(
                request.SessionId,
                request.Message,
                request.VisitorName,
                request.VisitorContact),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet("history/{sessionId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<ChatMessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ChatMessageDto>>> GetHistoryAsync(
        Guid sessionId,
        CancellationToken cancellationToken)
    {
        var messages = await _chatService.GetConversationMessagesAsync(sessionId, cancellationToken);

        return Ok(messages);
    }

    [HttpPost("session")]
    [ProducesResponseType(typeof(CreateChatSessionResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateChatSessionResponse>> CreateSessionAsync(
        CancellationToken cancellationToken)
    {
        var sessionId = await _chatService.CreateChatSessionAsync(cancellationToken);
        var response = new CreateChatSessionResponse(sessionId);

        return Created($"/api/chat/history/{sessionId}", response);
    }

    [HttpGet("sessions")]
    [ProducesResponseType(typeof(IReadOnlyList<ChatSessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ChatSessionDto>>> GetSessionsAsync(
        CancellationToken cancellationToken)
    {
        var sessions = await _chatService.GetChatSessionsAsync(cancellationToken);

        return Ok(sessions);
    }
}
