using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentFormsAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/"), ApiController]
    public class FormsScannerController : ControllerBase
    {
        private readonly ILogger<FormsScannerController> logger;
        private readonly IFormsScannerService formsScannerService;

        public FormsScannerController(ILogger<FormsScannerController> logger, IFormsScannerService formsScannerService)
        {
            this.logger = logger;
            this.formsScannerService = formsScannerService;
        }

        [HttpPost, Route("forms-scanner")]
        public async Task<IActionResult> ScanIdentityCard(IFormFile file, [FromQuery] string documentType)
        {
            try
            {
                var scan = Enum.Parse<ScannableDocumentType>(documentType);
                
                switch(scan)
                {
                    case ScannableDocumentType.Identity_Card:
                        var identityCard = await formsScannerService.ScanIdentityCardAsync(file);
                        return Ok(identityCard);

                    case ScannableDocumentType.Passport_Card:
                        var passport = await formsScannerService.ScanPassportAsync(file);
                        return Ok(passport);

                    case ScannableDocumentType.Vehicle_Identity_Card:
                        var vehicleIdentityCard = await formsScannerService.ScanVehicleIdentityCardAsync(file);
                        return Ok(vehicleIdentityCard);

                    case ScannableDocumentType.Any_Document:
                        var anyDocument = await formsScannerService.ScanAnyDocumentAsync(file);
                        return Ok(anyDocument);

                    default: return BadRequest("Unknown document type");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}

