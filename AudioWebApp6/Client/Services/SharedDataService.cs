﻿namespace AudioWebApp.Client.Services
{
    public class SharedDataService
    {
        public string? AudioLink { get; set; }
        public string? AudioTitle { get; set; }
        public event Action OnPlayerToggle;

        public void TogglePlayer()
        {
            OnPlayerToggle?.Invoke();
        }
    }
}