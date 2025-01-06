using Microsoft.AspNetCore.Components;

namespace FocusAi.Pages
{
   
    public partial class Search : ComponentBase
    {

        private string TokenId { get; set; } = string.Empty;

        public async Task SearchToken()
        {
            if (!string.IsNullOrWhiteSpace(TokenId))
            {
                // Redirect to a page with the token ID or handle the search logic
                NavigationManager.NavigateTo($"/token-details/{TokenId}");
            }
            else
            {
                // Optionally, show an alert or validation message
                Console.WriteLine("Token ID cannot be empty!");
            }
        }

        public async Task GoBack()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}