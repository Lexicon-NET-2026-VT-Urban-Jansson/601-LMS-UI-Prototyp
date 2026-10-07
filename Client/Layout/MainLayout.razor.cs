using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using TestClaudeLMS01.Client.Services;

namespace TestClaudeLMS01.Client.Layout
{
    public enum UserRole
    {
        Elev,
        Larare
    }

    public class NavItem
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;
    }

    public partial class MainLayout
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected AuthState AuthState { get; set; }

        private bool sidebarExpanded = true;

        private static readonly List<NavItem> ElevMenuItems = new()
        {
            new NavItem { Text = "Dashboard", Icon = "home", Path = "/", Match = NavLinkMatch.All },
            new NavItem { Text = "Min kurs", Icon = "menu_book", Path = "/min-kurs", Match = NavLinkMatch.Prefix },
            new NavItem { Text = "Inlämningar", Icon = "description", Path = "/inlamningar", Match = NavLinkMatch.Prefix },
            new NavItem { Text = "Kursdeltagare", Icon = "group", Path = "/kursdeltagare", Match = NavLinkMatch.Prefix }
        };

        private static readonly List<NavItem> LarareMenuItems = new()
        {
            new NavItem { Text = "Dashboard", Icon = "home", Path = "/", Match = NavLinkMatch.All },
            new NavItem { Text = "Kurser", Icon = "menu_book", Path = "/kurser", Match = NavLinkMatch.Prefix },
            new NavItem { Text = "Skapa ny kurs", Icon = "add_circle_outline", Path = "/skapa-kurs", Match = NavLinkMatch.Prefix },
            new NavItem { Text = "Användare", Icon = "group", Path = "/anvandare", Match = NavLinkMatch.Prefix },
            new NavItem { Text = "Inlämningar", Icon = "format_list_bulleted", Path = "/inlamningar", Match = NavLinkMatch.Prefix }
        };

        public string GetRoleLabel()
        {
            if (!AuthState.IsAuthenticated)
                return "";
            return AuthState.CurrentRole == UserRole.Elev ? "Elev" : "Lärare";
        }

        public List<NavItem> GetMenuItems()
        {
            if (!AuthState.IsAuthenticated)
                return new();
            return AuthState.CurrentRole == UserRole.Elev ? ElevMenuItems : LarareMenuItems;
        }

        void SidebarToggleClick()
        {
            sidebarExpanded = !sidebarExpanded;
        }

        void OnLogout()
        {
            AuthState.Logout();
            NavigationManager.NavigateTo("/login", forceLoad: true);
        }
    }
}
