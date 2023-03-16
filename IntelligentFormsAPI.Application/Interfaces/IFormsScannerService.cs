using Microsoft.AspNetCore.Http;

namespace IntelligentFormsAPI.Application.Interfaces
{
    public interface IFormsScannerService
    {
        public Task<Dictionary<string, string>> ScanIdentityCardAsync(IFormFile file);
        public Task<Dictionary<string, string>> ScanPassportAsync(IFormFile file);
        public Task<Dictionary<string, string>> ScanVehicleIdentityCardAsync(IFormFile file);
        public Task<string> ScanAnyDocumentAsync(IFormFile file);
    }
}
