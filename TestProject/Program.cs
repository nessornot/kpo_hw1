namespace TestProject;

class Program
{
    static void Main()
    {
        IBankAccountRepository bankAccountRepo = new InMemoryBankAccountRepository();
        IBankAccountFactory bankAccountFactory = new BankAccountFactory();
        IOperationRepository operationRepo = new InMemoryOperationRepository();
        IOperationFactory operationFactory = new OperationFactory();
        ICategoryRepository categoryRepo = new InMemoryCategoryRepository();
        ICategoryFactory categoryFactory = new CategoryFactory();
        
        // Facades
        var bankAccountFacade = new BankAccountFacade(bankAccountRepo, bankAccountFactory);
        var operationFacade = new OperationFacade(operationRepo, bankAccountRepo, operationFactory);
        var categoryFacade = new CategoryFacade(categoryRepo, categoryFactory);
        
        var accountsId = new Dictionary<string, Guid>();
        var categoriesId = new Dictionary<string, Guid>();
        
        while (true)
        {
            string[] choices = {"Choose option (use arrow keys to navigate)", "Create account", "Create category", 
                "Create operation", "Show list of accounts", "exit"};
            var choice = Menu.Choice(choices);
            switch (choice)
            {
                // Creating account
                case 1:
                    var accountBalance = Menu.EnterBalance();
                    var accountName = Menu.EnterName("\nEnter account name: ");
                    var acc = bankAccountFacade.CreateAccount(accountName, accountBalance);
                    
                    bankAccountRepo.Add(acc);
                    accountsId.Add(accountName, acc.Id);
                    
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                
                // Creating category
                case 2:
                    var categoryName = Menu.EnterName("\nEnter category name: ");
                    string[] categoryTypeChoice = {"Enter type of category: ", "Income", "Expense"};
                    var categoryType = Menu.Choice(categoryTypeChoice) - 1;
                    OperationType catType = categoryType == 0 ? OperationType.Expense : OperationType.Income;

                    var cat = categoryFacade.CreateCategory(catType, categoryName);
                    categoryRepo.Add(cat);
                    categoriesId.Add(categoryName, cat.Id);
                    
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                
                // Creating operation
                case 3:
                    var categoryChoices = categoriesId.Keys.ToArray();
                    var accountChoices = accountsId.Keys.ToArray();
                    
                    string operationCategory = Menu.ChoiceStr("\nChoose category: ", categoryChoices);
                    string operationAccount = Menu.ChoiceStr("\nChoose account: ", accountChoices);
                    
                    var operationAmount = Menu.EnterBalance();
                    
                    var operCat = categoryFacade.GetById(categoriesId[operationCategory]);
                    var operAcc = bankAccountFacade.GetById(accountsId[operationAccount]);
                    
                    var oper = operationFacade.AddOperation(
                        type: operCat.Type,
                        accountId: operAcc.Id,
                        amount: operationAmount,
                        categoryId: operCat.Id,
                        date: DateTime.Now
                    );
                    
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                
                // Printing info about all accounts
                case 4:
                    Console.Clear();
                    
                    bankAccountFacade.PrintAllAccounts();
                    
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                
                // Program exit
                case 5:
                    Console.Clear();
                    Console.WriteLine("Program ended");
                    return;
            }
        }
    }
}