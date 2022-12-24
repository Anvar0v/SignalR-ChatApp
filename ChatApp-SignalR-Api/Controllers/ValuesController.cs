using ChatApp_SignalR_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp_SignalR_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signinManager;

    public ValuesController(SignInManager<IdentityUser> signinManager)
    {
        _signinManager = signinManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username)
    {
        var user = new IdentityUser(username);

        await _signinManager.SignInAsync(user, isPersistent: true);

        return Ok();
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetProfile() =>
        Ok($"UserName: {User.FindFirst(ClaimTypes.Name)!.Value}");

    [HttpGet("groups")]
    public IActionResult GetGroups()
    {
        List<string>? GroupsList = new List<string>()
        {
            "Dotnet",
            "Blazor",
            "Javascipt",
        };

        return Ok(GroupsList);
    }

    [HttpGet("groups/{group}")]
    public IActionResult GetGroupMessages(string group,
        [FromServices] MessageService messageService) =>
        Ok(messageService.Messages[group]
            .Select(t => $"{t.Item1}: {t.Item2}").ToList());
}
