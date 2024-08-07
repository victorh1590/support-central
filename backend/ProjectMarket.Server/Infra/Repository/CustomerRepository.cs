using Dapper;
using ProjectMarket.Server.Data.Model.Entity;
using ProjectMarket.Server.Infra.Db;

namespace ProjectMarket.Server.Infra.Repository;

public class CustomerRepository(IUnitOfWork unitOfWork)
{
    public readonly IUnitOfWork UnitOfWork = unitOfWork;

    public IEnumerable<Customer> GetAll()
    {
        // TODO Use pagination instead.
        const string query = "SELECT * FROM Customer";
        return UnitOfWork.Connection.Query<Customer>(query);
    }

    public Customer GetCustomerById(int id)
    {
        const string query = "SELECT * FROM Customer WHERE CustomerId = @CustomerId";
        return UnitOfWork.Connection.QueryFirstOrDefault<Customer>(query, new { CustomerId = id }) 
               ?? throw new ArgumentException($"{nameof(Customer.CustomerId)} not found");
    }

    public Customer Insert(Customer customer)
    {
        const string query = "INSERT INTO Customer (Name, Email, Password, RegistrationDate) " +
                             "VALUES (@Name, @Email, @Password, @RegistrationDate) " + 
                             "RETURNING CustomerId, Name, Email, Password, RegistrationDate";

        return UnitOfWork.Connection.QuerySingle<Customer>(query, customer);
    }

    public Customer Update(Customer customer)
    {
        const string query = "UPDATE Customer " + 
                             "SET Name = @Name, Email = @Email, Password = @Password, RegistrationDate = @RegistrationDate " + 
                             "WHERE CustomerId = @CustomerId " +
                             "RETURNING CustomerId, Name, Email, Password, RegistrationDate";
        return UnitOfWork.Connection.QuerySingle<Customer>(query, customer);
    }

    public bool Delete(Customer customer)
    {
        const string query = "DELETE FROM Customer CASCADE WHERE CustomerId = @CustomerId " +
                             "RETURNING CustomerId";
        return UnitOfWork.Connection.Execute(query, customer) == 1;
    }
}
