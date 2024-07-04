public class LoanRepl {

    // System State
    private IReadModel[] readModels;
    private LendingDecider decider;

    // Temporary (mutable) State
    private LoanApplication currentLoanApplication;
    private LoanDecision currentLoanDecision;

    public LoanRepl()
    {
        readModels = new IReadModel[] {  };
        decider = new LendingDecider();
    }

    public void Read() {

        
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

public interface Event {}
public interface Command {}

public class LoanDecision(int LoanAmount, int AssetValue, int CreditScore, bool Decision, decimal LoanToValue) : Event {}

public record LoanApplication(int LoanAmount, int AssetValue, int CreditScore) : Command;

public class LendingDecider {
    public LoanDecision Decide(LoanApplication application) {
        throw new NotImplementedException();
    }
}