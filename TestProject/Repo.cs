namespace TestProject;

public interface IBankAccountRepository
{
    void Add(BankAccount account);
    BankAccount GetById(Guid id);
    IEnumerable<BankAccount> GetAll();
}

public interface IOperationRepository
{
    void Add(Operation operation);
    Operation GetById(Guid id);
    IEnumerable<Operation> GetAll();
}

public interface ICategoryRepository
{
    void Add(Category category);
    Category GetById(Guid id);
    IEnumerable<Category> GetAll();
}
public class InMemoryBankAccountRepository : IBankAccountRepository
{
    private readonly Dictionary<Guid, BankAccount> _accounts = new();
    
    public void Add(BankAccount account) => _accounts[account.Id] = account;
    public BankAccount GetById(Guid id) => _accounts.GetValueOrDefault(id);
    public IEnumerable<BankAccount> GetAll() => _accounts.Values;
}

public class InMemoryOperationRepository : IOperationRepository
{
    private readonly List<Operation> _operations = new();

    public void Add(Operation operation) => _operations.Add(operation);
    public Operation GetById(Guid id) => _operations.FirstOrDefault(o => o.Id == id);
    public IEnumerable<Operation> GetAll() => _operations;
}

public class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = new();
    
    public void Add(Category category) => _categories.Add(category);
    public Category GetById(Guid id) => _categories.FirstOrDefault(c => c.Id == id);
    public IEnumerable<Category> GetAll() => _categories;
}