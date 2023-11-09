namespace BarsGroupTestProject.Pages;

public class ShoppingCartPage
{
    private readonly IWebDriver _driver;
    private const string Cart = "Your Cart";

    public ShoppingCartPage(IWebDriver driver)
    {
        _driver = driver;
    }
    
    private readonly By _continueShoppingButton = By.Id("continue-shopping");
    private By CartHeader => By.XPath($"//span[text()[contains(.,'{Cart}')]]");
    private By Product(string productName) => By.XPath($"//div[@class='inventory_item_name' and text()='{productName}']");

    internal void ContinueShopping()
    {
        _driver.FindElement(_continueShoppingButton).Click();
    }
    
    public bool IsShoppingCartPageOpened()
    {
        return _driver.FindElement(CartHeader).Displayed;
    }
    
    public bool IsProductInCart(string productName)
    {
        return _driver.FindElement(Product(productName)).Displayed;
    }
}