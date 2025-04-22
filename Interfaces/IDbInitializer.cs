using CUT_Burger.Data;

namespace CUT_Burger.Interfaces;

public interface IDbInitializer
{
    void Initialize(SqLiteDbContext context);   
}