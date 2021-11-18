namespace TaxCalculatorApi.Test.Base
{
    public class BaseTest<TInstance> where TInstance : class
    {
        protected TInstance TestedInstance;
    }
}