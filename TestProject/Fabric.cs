namespace TestProject;

// BankAccount Factory
public interface IBankAccountFactory
{
    BankAccount Create(string name, decimal initialBalance);
}

public class BankAccountFactory : IBankAccountFactory
{
    public BankAccount Create(string name, decimal initialBalance)
    {
        if (string.IsNullOrEmpty(name)) 
            throw new ArgumentException("Name is required.");
        if (initialBalance < 0) 
            throw new ArgumentException("Balance cannot be negative.");
        
        return new BankAccount { Id = Guid.NewGuid(), Name = name, Balance = initialBalance };
    }
}

// Category Factory
public interface ICategoryFactory
{
    Category Create(OperationType type, string name);
}

public class CategoryFactory : ICategoryFactory
{
    public Category Create(OperationType type, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название категории не может быть пустым.");
        
        // Дополнительная проверка: например, запрет на категории с именем "Test"
        if (name.Equals("Test", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Недопустимое название категории.");

        return new Category 
        { 
            Id = Guid.NewGuid(), 
            Type = type, 
            Name = name 
        };
    }
}

// Operation Factory
public interface IOperationFactory
{
    Operation Create(
        OperationType type,
        Guid accountId,
        decimal amount,
        DateTime date,
        Guid categoryId,
        string description = null
    );
}

public class OperationFactory : IOperationFactory
{
    public Operation Create(
        OperationType type,
        Guid accountId,
        decimal amount,
        DateTime date,
        Guid categoryId,
        string description = null
    )
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");
        
        if (accountId == Guid.Empty)
            throw new ArgumentException("No account id specified.");
        
        if (categoryId == Guid.Empty)
            throw new ArgumentException("No category id specified.");

        return new Operation
        {
            Id = Guid.NewGuid(),
            Type = type,
            BankAccountId = accountId,
            Amount = amount,
            Date = date,
            CategoryId = categoryId,
            Description = description
        };
    }
}
