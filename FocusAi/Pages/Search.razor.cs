using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;

namespace FocusAi.Pages
{
   
    public partial class Search : ComponentBase
    {

        private string TokenId { get; set; } = string.Empty;
        public bool isLoading { get; set; }
        private List<string> messages { get; set; }
        private string displayedMessage = string.Empty;

        [Inject]
        private IMemoryCache MemoryCache { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                loadScenarios();
            }


        }

        private string GetRandomMessage()
        {
            Random random = new Random();
            int index = random.Next(messages.Count); return messages[index];
        }

        public void Messageclear()
        {
            displayedMessage = string.Empty;
        }

        public async Task SearchToken()
        {
            displayedMessage = string.Empty;
            isLoading = true;
            await Task.Delay(2000);
            if (!MemoryCache.TryGetValue(TokenId, out string cachedMessage))
            {
                cachedMessage = GetRandomMessage();
                MemoryCache.Set(TokenId, cachedMessage);
            }
            displayedMessage = cachedMessage;
            isLoading = false;
        }

        public async Task GoBack()
        {
            NavigationManager.NavigateTo("/");
        }

        private void loadScenarios()
        {
            messages = new List<string>()
            {
            "Entry Point: Price drops 15%. Take Profit: 110%. Stop Loss: 8%. RRR: 13.75:1. AI expects a swift reversal driven by oversold signals and mean reversion.",
            "Entry Point: Price increases by 5% beyond a key resistance. Take Profit: 85%. Stop Loss: 5%. RRR: 17:1. Advanced AI detects a high-probability breakout supported by increasing volume.",
            "Entry Point: Price retraces by 12% in an uptrend. Take Profit: 150%. Stop Loss: 10%. RRR: 15:1. AI predicts a continuation based on historical trend analysis and momentum indicators.",
            "Entry Point: Price declines 8% during a low-volatility squeeze. Take Profit: 95%. Stop Loss: 6%. RRR: 15.83:1. The system anticipates a breakout after volatility expansion.",
            "Entry Point: Price touches a historical support level after a 10% drop. Take Profit: 120%. Stop Loss: 7%. RRR: 17.14:1. AI identifies strong confluence at support for a high-probability bounce.",
            "Entry Point: Price drops 6% due to negative news. Take Profit: 75%. Stop Loss: 3%. RRR: 25:1. AI identifies the dip as an overreaction with potential for recovery as sentiment stabilises.",
            "Entry Point: Price retraces 4% after breaking a key resistance. Take Profit: 70%. Stop Loss: 4%. RRR: 17.5:1. AI suggests the retest offers an ideal entry for continuation.",
            "Entry Point: Price declines 14% to form a double bottom. Take Profit: 130%. Stop Loss: 9%. RRR: 14.44:1. AI confirms a strong reversal pattern backed by volume divergence.",
            "Entry Point: Price drops 10% to close a previous gap. Take Profit: 100%. Stop Loss: 8%. RRR: 12.5:1. AI predicts high probability for a reversal after the gap fills.",
            "Entry Point: Price deviates 20% from its mean value (moving average). Take Profit: 150%. Stop Loss: 12%. RRR: 12.5:1."
            };
        }
    }
}