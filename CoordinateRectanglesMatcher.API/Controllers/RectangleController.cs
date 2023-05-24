using CoordinateRectanglesMatcher.Models;
using CoordinateRectanglesMatcher.Services;
using CoordinateRectanglesMatcher.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoordinateRectanglesMatcher.Controllers;

[ApiController]
[Route("rectangle/v1")]
public class RectangleController : ControllerBase
{
    private readonly ILogger<RectangleController> _logger;
    private readonly IRectangleService _rectangleService;
    private readonly ICustomAuthenticationManager _customAuthenticationManager;
    
    private readonly AuthModelValidator _authModelValidator;

    public RectangleController(
        ILogger<RectangleController> logger,
        IRectangleService rectangleService,
        ICustomAuthenticationManager customAuthenticationManager,
        AuthModelValidator authModelValidator
        )
    {
        _logger = logger;
        _rectangleService = rectangleService;
        _customAuthenticationManager = customAuthenticationManager;
        _authModelValidator = authModelValidator;
    }
    [AllowAnonymous]
    [HttpGet("seed")]
    public async Task<ActionResult> Seed()
    {
        await _rectangleService.Seed();
        
        return Ok();
    }
    [Authorize]    
    [HttpPost("search")]
    public async Task<IActionResult> GetRectanglesWithPoints([FromBody]List<Point> points)
    {
        if (!points.Any())
        {
            throw new MatcherInvalidParameterException();
        }
        return Ok(_rectangleService.Search(points)); 
    }
    
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] User user)
    {
        var validationResult = await _authModelValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            throw new MatcherInvalidParameterException();
        }
        
        var token = _customAuthenticationManager.Authenticate(user.UserName, user.Password);
        if (token == null) 
            return Unauthorized();
             
        return Ok(token);
    }
}