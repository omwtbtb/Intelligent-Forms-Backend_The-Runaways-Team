using Microsoft.AspNetCore.Http;

namespace IntelligentFormsAPI.Application.Interfaces
{
    public interface IFormsScannerService
    {
        public Task<Dictionary<string, string>> ScanIdentityCardAsync(string file);
        public Task<Dictionary<string, string>> ScanPassportAsync(string file);
        public Task<Dictionary<string, string>> ScanVehicleIdentityCardAsync(string file);
        public Task<string> ScanAnyDocumentAsync(string file);
    }
}
