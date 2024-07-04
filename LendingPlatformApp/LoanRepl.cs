public class LoanRepl {

    private IReadModel[] readModels;

    public void Setup() {
    }

    public void Read() {
    }

    public void Eval() {
    }

    public void Print() {
        Console.WriteLine("Loan Decision Metrics");
        Console.WriteLine("=====================");
        foreach (var readModel in readModels) {
            Console.WriteLine(readModel.ToString());
        }

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

public class LoanDecision : Event {}