using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FocusAi.Pages
{
    public partial class Intro : ComponentBase
    {
        [Inject]
        private IJSRuntime JS { get; set; } = default!;
        public void ToggleFAQ()
        {
            // Add logic to toggle FAQ display
        }

        public void ScrollToWhyFocus() => JS.InvokeVoidAsync("scrollToSection", "why-focus");
        public void ScrollToHowItWorks() => JS.InvokeVoidAsync("scrollToSection", "how-it-works");
        public void ScrollToProof() => JS.InvokeVoidAsync("scrollToSection", "proof");
        public void ScrollToFAQ() => JS.InvokeVoidAsync("scrollToSection", "Fa-Questions");


     
    }
}