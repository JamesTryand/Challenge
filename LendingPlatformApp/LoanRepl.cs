public class LoanRepl {

    // System State
    private IReadModel[] readModels;
    private LendingDecider decider;

    // Temporary (mutable) State
    private LoanApplication currentLoanApplication;
    private LoanDecision currentLoanDecision;

    public LoanRepl()
    {
        readModels = new IReadModel[] {  
            new LatestDecisionResult(),
            new TotalApplicationsToDateBySuccess(),
            new TotalValueOfLoansWrittenToDate(),
            new MeanAverageLoanToValueOfAllLoans()
        };
        decider = new LendingDecider();
    }

    public void Read() {
        Console.WriteLine("Enter Loan Amount: ");
        var loanAmount = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Asset Value: ");
        var assetValue = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Credit Score: ");
        var creditScore = int.Parse(Console.ReadLine());
        currentLoanApplication = new LoanApplication(loanAmount, assetValue, creditScore);
        
    }

    public void Eval() {
        currentLoanDecision = decider.Decide(currentLoanApplication);
        foreach (var readModel in readModels) {
            readModel.Update(currentLoanDecision);
        }
    }

    public void Print() {
        Console.WriteLine("Loan Decision Metrics");
        Console.WriteLine("=====================");
        foreach (var readModel in readModels) {
            Console.WriteLine(readModel.ToString());
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    public void Start() {
        while (true) {
            Read();
            Eval();
            Print();
        }
    }
}

public interface IReadModel {
    void Update(LoanDecision e);
}

public class LatestDecisionResult : IReadModel {
    private LoanDecision latestDecision;
    public void Update(LoanDecision e) {
        latestDecision = e;
    }

    public override string ToString() {
        return $"Loan Application Was Successful: {latestDecision.Decision}\n";
    }
}
public class TotalApplicationsToDateBySuccess : IReadModel {
    int successfulApplications = 0;
    int totalApplications = 0;
    public void Update(LoanDecision e) {
        totalApplications++;
        if (e.Decision) {
            successfulApplications++;
        }
    }
    public override string ToString() {
        return $"Total Applications: {totalApplications}\nSuccessful Applications: {successfulApplications}\nUnsuccessful Applications: {totalApplications - successfulApplications}";
    }
}

public class TotalValueOfLoansWrittenToDate : IReadModel {
    decimal totalValue = 0;
    public void Update(LoanDecision e) {
        if (e.Decision) {
            totalValue += e.LoanAmount;
        }
    }

    public override string ToString() {
        return $"Total Value of Loans Written to Date: {totalValue}";
    }
}

public class MeanAverageLoanToValueOfAllLoans : IReadModel {
    decimal totalLtv = 0;
    int totalApplications = 0;
    public void Update(LoanDecision e) {
        totalLtv += e.LoanToValue;
        totalApplications++;
    }

    public override string ToString() {
        return $"Mean Average Loan to Value of All Loans: {totalLtv / totalApplications}";
    }
}


public interface Event {}
public interface Command {}

public record LoanDecision(int LoanAmount, int AssetValue, int CreditScore, bool Decision, decimal LoanToValue) : Event {}

public record LoanApplication(int LoanAmount, int AssetValue, int CreditScore) : Command;

public class LendingDecider {
    public LoanDecision Decide(LoanApplication application) {
        var ltv = application.LoanAmount / application.AssetValue * 100;
        var decision = false;
        //boundary conditions
        if (application.LoanAmount < 1500000 && application.AssetValue < 1500000 && application.CreditScore < 1000) {
            return new LoanDecision(application.LoanAmount, application.AssetValue, application.CreditScore, decision, ltv);
        }

        if (application.LoanAmount > 1000000) {
            decision = ltv <= 60 && application.CreditScore >= 950;
            return new LoanDecision(application.LoanAmount, application.AssetValue, application.CreditScore, decision, ltv);
        }

        switch (ltv) {
            case < 60:
                decision = application.CreditScore >= 750;
                break;
            case < 80:
                decision = application.CreditScore >= 800;
                break;
            case < 90:
                decision = application.CreditScore >= 900;
                break;
            default:
                decision = false;
                break;
            }        
        
        return new LoanDecision(application.LoanAmount, application.AssetValue, application.CreditScore, decision, ltv);
    }
}