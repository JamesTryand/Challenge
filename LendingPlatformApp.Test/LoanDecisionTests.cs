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
    [InlineData(1000000, 2000000, 950, true)]  // £1M loan, 50% LTV, 950 credit score
    [InlineData(1000000, 1500000, 950, false)] // £1M loan, 66.67% LTV, 950 credit score
    [InlineData(1000000, 2000000, 949, false)] // £1M loan, 50% LTV, 949 credit score
    [InlineData(999999, 2000000, 750, true)]   // £999,999 loan, 50% LTV, 750 credit score
    [InlineData(999999, 2000000, 749, false)]  // £999,999 loan, 50% LTV, 749 credit score
    [InlineData(999999, 1250000, 800, true)]   // £999,999 loan, 80% LTV, 800 credit score
    [InlineData(999999, 1250000, 799, false)]  // £999,999 loan, 80% LTV, 799 credit score
    [InlineData(999999, 1111111, 900, true)]   // £999,999 loan, 90% LTV, 900 credit score
    [InlineData(999999, 1111111, 899, false)]  // £999,999 loan, 90% LTV, 899 credit score
    [InlineData(999999, 1000000, 999, false)]  // £999,999 loan, 100% LTV, 999 credit score
    [InlineData(1500001, 3000000, 999, false)] // £1,500,001 loan, 50% LTV, 999 credit score
    [InlineData(99999, 200000, 999, false)]    // £99,999 loan, 50% LTV, 999 credit score
    
    public void Application_ShouldReturnCorrectDecision(int loanAmount, int assetValue, int creditScore, bool expected)
    {
        var LoanApplication = new LoanApplication(loanAmount, assetValue, creditScore);
        var decider = new LendingDecider();
        var result = decider.Decide(LoanApplication);

        Assert.Equal(expected, result.Decision);
    }
}