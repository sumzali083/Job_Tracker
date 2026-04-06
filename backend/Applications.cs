namespace JobTracker;

public class Applications
{
    public int Id { get; set; }
    public string Company { get; set; }
    public string Role { get; set; }
    public string Status { get; set; } // Applied, Interview, Rejected, Offer
    public DateTime DateApplied { get; set; }
    public string Notes { get; set; }
}