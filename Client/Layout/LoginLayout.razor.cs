using Microsoft.AspNetCore.Components;
using Radzen;

namespace TestClaudeLMS01.Client.Layout
{
    public partial class LoginLayout
    {
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
    }
}
