using FocusAi.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using Solnet.Wallet;
using static System.Net.Mime.MediaTypeNames;

namespace FocusAi.Pages
{

    public partial class Search : ComponentBase
    {

        private string TokenId { get; set; } = string.Empty;
        public bool isLoading { get; set; }
        public bool isError { get; set; } = false;
        private List<string> messages { get; set; }
        public TradingStrategy displayedMessage { get; set; } = new TradingStrategy();
        public required List<TradingStrategy> tradingStrategies { get; set; }

        [Inject]
        private IJSRuntime JS { get; set; } = default!;
        [Inject]
        private IMemoryCache MemoryCache { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                loadScenarios();
            }


        }

        private TradingStrategy GetRandomMessage()
        {
            Random random = new Random();
            int index = random.Next(tradingStrategies.Count);

            var entryPointIndex = random.Next(tradingStrategies[index].EntryPoints.Count);

            TradingStrategy ts = new TradingStrategy()
            {
                Title = tradingStrategies[index].Title,
                EntryPoints = new List<string>()
                {
                    tradingStrategies[index].EntryPoints[entryPointIndex].ToString()
                }
            };

            return ts;
        }

        public void Messageclear()
        {
            isError = false;

        }

        public async Task SearchToken()
        {

            var isValidToken = PublicKey.IsValid(TokenId);
            if (isValidToken)
            {
                displayedMessage = new TradingStrategy();
                isLoading = true;
                await Task.Delay(2000);
                isError = false;
               
                if (!MemoryCache.TryGetValue(TokenId, out TradingStrategy cachedMessage))
                {
                    cachedMessage = GetRandomMessage();
                    MemoryCache.Set(TokenId, cachedMessage); 
                }
                displayedMessage = cachedMessage;
                string messsage = displayedMessage.EntryPoints.FirstOrDefault().ToString();
                await JS.InvokeVoidAsync("typeOutMessage", messsage);

                isLoading = false;
            }
            else
            {
                isError = true;
            }


        }

        //public async Task SearchToken()
        //{
        //    //displayedMessage = null;
        //    isLoading = true;
        //    await Task.Delay(2000);
        //    if (!MemoryCache.TryGetValue(TokenId, out TradingStrategy cachedMessage))
        //    {
        //        cachedMessage = GetRandomMessage();
        //        MemoryCache.Set(TokenId, cachedMessage);
        //    }
        //    displayedMessage = cachedMessage;
        //    isLoading = false;
        //}

        public async Task GoBack()
        {
            NavigationManager.NavigateTo("/");
        }

        private void loadScenarios()
        {
            tradingStrategies = new List<TradingStrategy>
            {
                new TradingStrategy
                {
                Title = "Momentum Reversal",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price drops 10%. Take Profit: 100%. Stop Loss: 8%. Risk-to-Reward Ratio: 12.5:1. AI anticipates a quick recovery from oversold levels.",
                        "Entry: Price drops 12%. Take Profit: 115%. Stop Loss: 9%. Risk-to-Reward Ratio: 12.78:1. AI signals reversal from key technical levels.",
                        "Entry: Price drops 15%. Take Profit: 120%. Stop Loss: 10%. Risk-to-Reward Ratio: 12:1. AI detects strong recovery potential.",
                        "Entry: Price drops 8%. Take Profit: 85%. Stop Loss: 6%. Risk-to-Reward Ratio: 14.17:1. AI foresees mean reversion.",
                        "Entry: Price drops 20%. Take Profit: 150%. Stop Loss: 12%. Risk-to-Reward Ratio: 12.5:1. AI predicts a sharp bounce."
                    }
                },
                 new TradingStrategy
                {
                    Title = "Breakout Opportunity",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price rises 5% above resistance. Take Profit: 85%. Stop Loss: 5%. Risk-to-Reward Ratio: 17:1. AI detects strong breakout momentum.",
                        "Entry: Price rises 7% above resistance. Take Profit: 100%. Stop Loss: 7%. Risk-to-Reward Ratio: 14.29:1. AI flags a continuation breakout.",
                        "Entry: Price rises 10%. Take Profit: 125%. Stop Loss: 9%. Risk-to-Reward Ratio: 13.89:1. AI highlights extended breakout potential.",
                        "Entry: Price rises 3%. Take Profit: 50%. Stop Loss: 3%. Risk-to-Reward Ratio: 16.67:1. AI identifies low-risk breakout opportunity.",
                        "Entry: Price rises 6%. Take Profit: 70%. Stop Loss: 4%. Risk-to-Reward Ratio: 17.5:1. AI suggests breakout consolidation."
                    }
                },
                new TradingStrategy
                {
                    Title = "Trend Continuation",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price retraces 12%. Take Profit: 150%. Stop Loss: 10%. Risk-to-Reward Ratio: 15:1. AI predicts continuation of an uptrend.",
                        "Entry: Price retraces 8%. Take Profit: 110%. Stop Loss: 7%. Risk-to-Reward Ratio: 15.71:1. AI sees pullback opportunity in trend.",
                        "Entry: Price retraces 6%. Take Profit: 75%. Stop Loss: 4%. Risk-to-Reward Ratio: 18.75:1. AI identifies trend momentum resuming.",
                        "Entry: Price retraces 10%. Take Profit: 120%. Stop Loss: 8%. Risk-to-Reward Ratio: 15:1. AI highlights potential for higher highs.",
                        "Entry: Price retraces 15%. Take Profit: 200%. Stop Loss: 12%. Risk-to-Reward Ratio: 16.67:1. AI foresees a strong continuation rally."
                    }
                },
                new TradingStrategy
                {
                    Title = "Volatility Compression",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price drops 6%. Take Profit: 75%. Stop Loss: 4%. Risk-to-Reward Ratio: 18.75:1. AI predicts volatility expansion post-squeeze.",
                        "Entry: Price drops 8%. Take Profit: 85%. Stop Loss: 5%. Risk-to-Reward Ratio: 17:1. AI signals upcoming breakout after compression.",
                        "Entry: Price drops 10%. Take Profit: 100%. Stop Loss: 6%. Risk-to-Reward Ratio: 16.67:1. AI highlights tight consolidation leading to movement.",
                        "Entry: Price drops 15%. Take Profit: 150%. Stop Loss: 10%. Risk-to-Reward Ratio: 15:1. AI expects a sharp price breakout.",
                        "Entry: Price drops 12%. Take Profit: 140%. Stop Loss: 9%. Risk-to-Reward Ratio: 15.56:1. AI identifies breakout from a compressed range."
                    }
                },
                new TradingStrategy
                {
                    Title = "Support Bounce",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price drops 10%. Take Profit: 120%. Stop Loss: 8%. Risk-to-Reward Ratio: 15:1. AI detects bounce off strong support.",
                        "Entry: Price drops 8%. Take Profit: 85%. Stop Loss: 5%. Risk-to-Reward Ratio: 17:1. AI confirms support level holding.",
                        "Entry: Price drops 12%. Take Profit: 140%. Stop Loss: 9%. Risk-to-Reward Ratio: 15.56:1. AI flags rebound opportunity at support.",
                        "Entry: Price drops 6%. Take Profit: 70%. Stop Loss: 4%. Risk-to-Reward Ratio: 17.5:1. AI highlights low-risk bounce play.",
                        "Entry: Price drops 14%. Take Profit: 160%. Stop Loss: 10%. Risk-to-Reward Ratio: 16:1. AI sees significant upside from support."
                    }
                },
                new TradingStrategy
                {
                    Title = "News Catalyst",
                    EntryPoints = new List<string>
                    {
                      "Entry: Price drops 6%. Take Profit: 75%. Stop Loss: 3%. Risk-to-Reward Ratio: 25:1. AI identifies overreaction to negative news.",
                      "Entry: Price drops 8%. Take Profit: 95%. Stop Loss: 5%. Risk-to-Reward Ratio: 19:1. AI anticipates recovery as sentiment stabilises.",
                      "Entry: Price drops 10%. Take Profit: 120%. Stop Loss: 8%. Risk-to-Reward Ratio: 15:1. AI highlights market recovery potential.",
                      "Entry: Price drops 5%. Take Profit: 65%. Stop Loss: 4%. Risk-to-Reward Ratio: 16.25:1. AI signals short-term recovery.",
                      "Entry: Price drops 12%. Take Profit: 140%. Stop Loss: 9%. Risk-to-Reward Ratio: 15.56:1. AI foresees sentiment reversal."
                    }
                },
                new TradingStrategy
                {
                    Title = "Breakout Retest",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price retraces 4%. Take Profit: 65%. Stop Loss: 3%. Risk-to-Reward Ratio: 21.67:1. AI confirms breakout retest for low-risk entry.",
                        "Entry: Price retraces 7%. Take Profit: 90%. Stop Loss: 6%. Risk-to-Reward Ratio: 15:1. AI identifies retest holding support.",
                        "Entry: Price retraces 6%. Take Profit: 80%. Stop Loss: 5%. Risk-to-Reward Ratio: 16:1. AI signals breakout continuation.",
                        "Entry: Price retraces 10%. Take Profit: 120%. Stop Loss: 8%. Risk-to-Reward Ratio: 15:1. AI detects a strong breakout setup.",
                        "Entry: Price retraces 12%. Take Profit: 140%. Stop Loss: 9%. Risk-to-Reward Ratio: 15.56:1. AI highlights retest for extended gains."
                    }
                },
                new TradingStrategy
                {
                    Title = "Double Bottom Formation",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price drops 14%. Take Profit: 130%. Stop Loss: 9%. Risk-to-Reward Ratio: 14.44:1. AI confirms bullish reversal.",
                        "Entry: Price drops 12%. Take Profit: 120%. Stop Loss: 8%. Risk-to-Reward Ratio: 15:1. AI signals recovery from double bottom.",
                        "Entry: Price drops 10%. Take Profit: 100%. Stop Loss: 6%. Risk-to-Reward Ratio: 16.67:1. AI detects classic double bottom.",
                        "Entry: Price drops 8%. Take Profit: 85%. Stop Loss: 5%. Risk-to-Reward Ratio: 17:1. AI highlights smaller reversal.",
                        "Entry: Price drops 15%. Take Profit: 150%. Stop Loss: 10%. Risk-to-Reward Ratio: 15:1. AI expects strong recovery."
                    }
                },
                new TradingStrategy
                {
                    Title = "Gap Fill Trade",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price drops 10%. Take Profit: 100%. Stop Loss: 8%. Risk-to-Reward Ratio: 12.5:1. AI identifies opportunity post-gap closure.",
                        "Entry: Price drops 8%. Take Profit: 85%. Stop Loss: 6%. Risk-to-Reward Ratio: 14.17:1. AI highlights immediate recovery potential.",
                        "Entry: Price drops 12%. Take Profit: 120%. Stop Loss: 9%. Risk-to-Reward Ratio: 13.33:1. AI expects reversal after gap fills.",
                        "Entry: Price drops 15%. Take Profit: 150%. Stop Loss: 12%. Risk-to-Reward Ratio: 12.5:1. AI anticipates recovery post-gap.",
                        "Entry: Price drops 6%. Take Profit: 75%. Stop Loss: 4%. Risk-to-Reward Ratio: 18.75:1. AI flags gap closure."
                    }
                },
                 new TradingStrategy
                {
                    Title = "Mean Reversion",
                    EntryPoints = new List<string>
                    {
                        "Entry: Price deviates 20% from mean. Take Profit: 150%. Stop Loss: 12%. Risk-to-Reward Ratio: 12.5:1. AI detects strong reversion.",
                        "Entry: Price deviates 15% from mean. Take Profit: 120%. Stop Loss: 9%. Risk-to-Reward Ratio: 13.33:1. AI predicts quick mean alignment.",
                        "Entry: Price deviates 12% from mean. Take Profit: 100%. Stop Loss: 8%. Risk-to-Reward Ratio: 12.5:1. AI flags reversion opportunity.",
                        "Entry: Price deviates 10% from mean. Take Profit: 85%. Stop Loss: 6%. Risk-to-Reward Ratio: 14.17:1. AI sees smaller reversion potential.",
                        "Entry: Price deviates 8% from mean. Take Profit: 75%. Stop Loss: 5%. Risk-to-Reward Ratio: 15:1. AI anticipates minor reversion."
                    }
                 }
            };
        }
    }
}



    