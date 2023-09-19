namespace Bulky.Models
{
    // The ErrorViewModel class represents the model that holds information related to errors.
    public class ErrorViewModel
    {
        // Represents the ID of the request that generated an error.
        // This can be useful for tracking and logging errors.
        public string? RequestId { get; set; }

        // A boolean property that checks if the RequestId has a value.
        // Returns 'true' if RequestId is not null or empty, 'false' otherwise.
        // This can be used in the view to conditionally display the RequestId to the user.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
