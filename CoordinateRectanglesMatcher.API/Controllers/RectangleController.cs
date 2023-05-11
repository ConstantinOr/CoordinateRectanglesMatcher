using CoordinateRectanglesMatcher.Models;
using CoordinateRectanglesMatcher.Services;
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

    public RectangleController(ILogger<RectangleController> logger, IRectangleService rectangleService, ICustomAuthenticationManager customAuthenticationManager)
    {
        _logger = logger;
        _rectangleService = rectangleService;
        _customAuthenticationManager = customAuthenticationManager;
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
    public ActionResult GetRectanglesWithPoints([FromBody]List< Point> points)
    {
        return Ok(_rectangleService.Search(points)); 
    }
    
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] UserCred userCred)
    {
        var token = _customAuthenticationManager.Authenticate(userCred.UserName, userCred.Password);
             
        if (token == null) 
            return Unauthorized();
             
        return Ok(token);
    }
}