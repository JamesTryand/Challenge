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

    [Theory]
    [InlineData(1000, 10000, 700, false)]
    [InlineData(1500001, 1500001, 999, false)]
    [InlineData(1000000, 10000000, 999, true)]
    public void Application_ShouldReturnCorrectDecision(int loanAmount, int assetValue, int creditScore, bool expected)
    {
        var LoanApplication = new LoanApplication(loanAmount, assetValue, creditScore);
        var decider = new LendingDecider();
        var result = decider.Decide(LoanApplication);

        Assert.Equal(expected, result.Decision);
    }
}