using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.Contracts;

namespace FocusAi.Pages
{
    public partial class Intro : ComponentBase
    {
        [Inject]
        private IJSRuntime JS { get; set; } = default!;
        public string codeText { get; set; } = "";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(500); // Optional delay
                await JS.InvokeVoidAsync("oneTwo");
                await TypeText();
            }
        }

        public void ToggleFAQ()
        {
            // Add logic to toggle FAQ display
        }

        public void ScrollToWhyFocus() => JS.InvokeVoidAsync("scrollToSection", "why-focus");
        public void ScrollToHowItWorks() => JS.InvokeVoidAsync("scrollToSection", "how-it-works");
        public async void ScrollToProof() 
        {
            await JS.InvokeVoidAsync("scrollToSection", "proof");
            await TypeText();
        }

        public void ScrollToFAQ() => JS.InvokeVoidAsync("scrollToSection", "Fa-Questions");

        public async Task TypeText() 
        {
            string text = @" def ai_trading_strategy(data):
            signals = []
            for token in data['tokens']:
                momentum = calculate_momentum(token['prices'])
                if momentum &gt; threshold:
                    signals.append({&#x27;token&#x27;: token[&#x27;name&#x27;], &#x27;action&#x27;: &#x27;BUY&#x27;, &#x27;confidence&#x27;: momentum})
                elif momentum &lt; -threshold:
                    signals.append({&#x27;token&#x27;: token[&#x27;name&#x27;], &#x27;action&#x27;: &#x27;SELL&#x27;, &#x27;confidence&#x27;: abs(momentum)})
            return signals

        data = fetch_market_data()
        trading_signals = ai_trading_strategy(data)
        print(trading_signals)";

        await JS.InvokeVoidAsync("typeOutText", text);
        }

    }
}