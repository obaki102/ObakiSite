using MediatR;
using ObakiSite.Shared.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.MyCsv.Queries
{
    public class DownloadPdfFile
    {

        public record DonwloadMyCvAsPdfFile() : IRequest<ApplicationResponse>;

        public class DonwloadMyCvAsPdfFileHandler : IRequestHandler<DonwloadMyCvAsPdfFile, ApplicationResponse>
        {
            public Task<ApplicationResponse> Handle(DonwloadMyCvAsPdfFile request, CancellationToken cancellationToken)
            {
                //TODO: Convert url input into pdf file
                throw new NotImplementedException();
            }
        }
    }
}
