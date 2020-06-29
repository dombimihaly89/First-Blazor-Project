using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reusable.Components
{
    public class ConfirmBase : ComponentBase
    {
        protected bool ShowConfirmation { get; set; }
        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        public string ConfirmationTitle { get; set; }
        public string ConfirmationMessage { get; set; }

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }

    }
}
