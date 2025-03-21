namespace TestProject;

/// <summary>
/// Facade for bank accounts
/// </summary>
public class BankAccountFacade
{
    private readonly IBankAccountRepository _repo;
    private readonly IBankAccountFactory _factory;

    public BankAccountFacade(IBankAccountRepository repo, IBankAccountFactory factory)
    {
        _repo = repo;
        _factory = factory;
    }

    public BankAccount GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public BankAccount CreateAccount(string name, decimal balance)
    {
        var account = _factory.Create(name, balance);
        _repo.Add(account);
        return account;
    }

    public void PrintAllAccounts()
    {
        var accounts = _repo.GetAll();
        foreach (var account in accounts)
        {
            Console.Write(account.Name);
            Console.Write(" ");
            Console.WriteLine(account.Balance);
        }
    }
}

/// <summary>
/// Facade for operations
/// </summary>
public class OperationFacade
{
    private readonly IOperationRepository _opRepo;
    private readonly IBankAccountRepository _accRepo;
    private readonly IOperationFactory _factory;

    public OperationFacade(IOperationRepository opRepo, IBankAccountRepository accRepo, IOperationFactory factory)
    {
        _opRepo = opRepo;
        _accRepo = accRepo;
        _factory = factory;
    }

    public Operation AddOperation(OperationType type, Guid accountId, decimal amount, Guid categoryId, DateTime date)
    {
        var operation = _factory.Create(type, accountId, amount, date, categoryId);
        _opRepo.Add(operation);

        var account = _accRepo.GetById(accountId);
        if (type == OperationType.Expense && account.Balance < amount)
        {
            throw new Exception("Balance cannot be negative");
        }
        account.Balance += type == OperationType.Income ? amount : -amount;
        
        return operation;
    }
}

/// <summary>
/// Facade for categories
/// </summary>
public class CategoryFacade
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly ICategoryFactory _categoryFactory;

    public CategoryFacade(ICategoryRepository categoryRepo, ICategoryFactory categoryFactory)
    {
        _categoryRepo = categoryRepo;
        _categoryFactory = categoryFactory;
    }
    
    public Category CreateCategory(OperationType type, string name)
    {
        var category = _categoryFactory.Create(type, name);
        _categoryRepo.Add(category);
        return category;
    }

    public Category GetById(Guid id)
    {
        return _categoryRepo.GetById(id);
    }
    
    public IEnumerable<Category> GetAllCategories()
    {
        return _categoryRepo.GetAll();
    }
}
