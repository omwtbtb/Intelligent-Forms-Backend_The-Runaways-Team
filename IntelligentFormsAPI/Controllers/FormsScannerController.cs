using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(Summary = "Scan a form")]
        [HttpPost, Route("forms-scanner")]
        public async Task<IActionResult> ScanFormAsync([FromBody]string fileString, [FromQuery] string documentType)
        {
                var scan = Enum.Parse<ScannableDocumentType>(documentType);
                var formattedFileString = fileString.Split(',')[1];

                switch (scan)
                {
                    case ScannableDocumentType.Identity_Card:
                        var identityCard = await formsScannerService.ScanIdentityCardAsync(formattedFileString);
                        return Ok(identityCard);

                    case ScannableDocumentType.Passport_Card:
                        var passport = await formsScannerService.ScanPassportAsync(formattedFileString);
                        return Ok(passport);

                    case ScannableDocumentType.Vehicle_Identity_Card:
                        var vehicleIdentityCard = await formsScannerService.ScanVehicleIdentityCardAsync(formattedFileString);
                        return Ok(vehicleIdentityCard);

                    case ScannableDocumentType.Any_Document:
                        var anyDocument = await formsScannerService.ScanAnyDocumentAsync(formattedFileString);
                        return Ok(anyDocument);

                    default: return BadRequest("Unknown document type");
                }
        }
    }
    
}

