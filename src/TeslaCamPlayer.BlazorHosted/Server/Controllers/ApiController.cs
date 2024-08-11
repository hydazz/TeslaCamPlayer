using System.Web;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TeslaCamPlayer.BlazorHosted.Server.Providers.Interfaces;
using TeslaCamPlayer.BlazorHosted.Server.Services.Interfaces;
using TeslaCamPlayer.BlazorHosted.Shared.Models;

namespace TeslaCamPlayer.BlazorHosted.Server.Controllers;

[ApiController]
[Route("Api")]
public class ApiController : ControllerBase
{
	private readonly IClipsService _clipsService;
	private readonly string _rootFullPath;

	public ApiController(ISettingsProvider settingsProvider, IClipsService clipsService)
	{
		_rootFullPath = Path.GetFullPath(settingsProvider.Settings.ClipsRootPath);
		_clipsService = clipsService;
	}

	[HttpGet("GetClips.json")]
	public async Task<Clip[]> GetClips()
		=> await _clipsService.GetClipsAsync();

	[HttpPost("RefreshClips")]
	public IActionResult RefreshClips()
	{
		_clipsService.RefreshClips();
		return Ok();
	}

	[HttpGet("GetState.json")]
	public State GetState()
		=> _clipsService.State;

	private bool IsUnderRootPath(string path)
		=> path.StartsWith(_rootFullPath);

	[HttpPost("DeleteEvent")]
	public IActionResult DeleteEvent(string path)
	{
	    if (string.IsNullOrEmpty(path) || !IsUnderRootPath(path))
	        return BadRequest("Invalid path");
	
	    try
	    {
	        var fullPath = Path.Combine(_rootFullPath, path);
	        if (Directory.Exists(fullPath))
	        {
	            Directory.Delete(fullPath, true);
	            Log.Information("Deleted event folder: {Path}", fullPath);
	            return Ok();
	        }
	        return NotFound("Directory not found");
	    }
	    catch (Exception ex)
	    {
	        return StatusCode(500, $"Error deleting directory: {ex.Message}");
	    }
	}

	[HttpGet("Video/{path}.mp4")]
	public IActionResult Video(string path)
		=> ServeFile(path, ".mp4", "video/mp4", true);

	[HttpGet("Thumbnail/{path}.png")]
	public IActionResult Thumbnail(string path)
		=> ServeFile(path, ".png", "image/png");

	private IActionResult ServeFile(string path, string extension, string contentType, bool enableRangeProcessing = false)
	{
		path = HttpUtility.UrlDecode(path);
		path += extension;

		path = Path.GetFullPath(path);
		if (!IsUnderRootPath(path))
			return BadRequest($"File must be in subdirectory under \"{_rootFullPath}\", but was \"{path}\"");

		if (!System.IO.File.Exists(path))
			return NotFound();

		return PhysicalFile(path, contentType, enableRangeProcessing);
	}
}