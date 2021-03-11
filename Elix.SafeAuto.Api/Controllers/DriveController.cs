using Elix.SafeAuto.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Elix.SafeAuto.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriveController : ControllerBase
    {
        private readonly ILogger<DriveController> _logger;
        private readonly IDriverService _driverService;

        public DriveController(ILogger<DriveController> logger, IDriverService driverService)
        {
            _logger = logger;
            _driverService = driverService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        [HttpPost]
        public IActionResult Post(IFormFile commandFile)
        {
            string contents = null;
            try
            {
                using (var memoryStream = commandFile.OpenReadStream())
                {
                    using (var sr = new StreamReader(memoryStream))
                    {
                        contents = sr.ReadToEnd();
                    }
                }

                return Ok(_driverService.Process(contents));
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, "Error on DriveController.Post");
                return StatusCode(StatusCodes.Status500InternalServerError, exc.Message);
            }
        }
    }
}
