namespace LendingPlatformApp.Test;

public class LoanDecisionTests
{
    [Fact]
    public void CheckForTooLow()
    {

        var LoanApplication = new LoanApplication(1000, 10000, 700);
        var decider = new LendingDecider();
        var result = decider.Decide(LoanApplication);

        Assert.False(result.Decision);

    }
}