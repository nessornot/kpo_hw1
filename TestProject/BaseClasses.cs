namespace TestProject;

/// <summary>
/// Base class for bank account
/// </summary>
public class BankAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
}

/// <summary>
/// Base class for category
/// </summary>
public class Category
{
    public Guid Id { get; set; }
    public OperationType Type { get; set; }
    public string Name { get; set; }
}

/// <summary>
/// Base class for Operation
/// </summary>
public class Operation
{
    public Guid Id { get; set; }
    public OperationType Type { get; set; }
    public Guid BankAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}

public enum OperationType
{
    Income,
    Expense
}