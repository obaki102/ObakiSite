using MediatR;
using ObakiSite.Application.Features.Preference.Commands;
using ObakiSite.Shared.DTO;

namespace ObakiSite.Client.Services.Settings
{
    public class MySettings : IMySettings
    {
        private readonly ISender _mediator;
        private Preferences _preferences;

        public MySettings(ISender mediator)
        {
            _mediator = mediator;
            _preferences = new Preferences();
        }

        public Preferences Data => _preferences;

        public async Task GetMyDefaultSettings()
        {
            var result = await _mediator.Send(new GetPreferences());
            if (result.IsSuccess && result.Data is not null)
            {
                _preferences = result.Data;
            }
        }
       
    }
}
