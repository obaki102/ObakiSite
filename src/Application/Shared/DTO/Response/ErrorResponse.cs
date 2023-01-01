using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Shared.DTO.Response
{
    public record  ErrorResponse
    {
        public string Title { get; init; } = string.Empty; 
        public int StatusCode { get; init; }
        public string ExceptionMessage { get; init; } = string.Empty;
        public IReadOnlyList<string> Errors { get; init; } = new List<string>();
    }
}
