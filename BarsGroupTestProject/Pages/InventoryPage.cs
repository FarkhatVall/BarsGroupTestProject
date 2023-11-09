using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BarsGroupTestProject.Pages;

public class InventoryPage
{
    private readonly IWebDriver _driver;
    private const string Products = "Products";

    public InventoryPage(IWebDriver driver)
    {
        _driver = driver;
    }

    private By ProductsPage => By.XPath($"//span[text()[contains(.,'{Products}')]]");
    private By SortOption(string sortType) => By.XPath($"//option[text()[contains(., '{sortType}')]]");
    private By SortContainer => By.XPath("//select[@class=\"product_sort_container\"]");
    private By AddToCartButton(string productName) => By.XPath($"//div[text()='{productName}']/ancestor::div[@class='inventory_item']//button[text()='Add to cart']");
    private By RemoveButton(string productName) => By.XPath($"//div[text()='{productName}']/ancestor::div[@class='inventory_item']//button[text()='Remove']");
    private By ProductPrice => By.XPath("//div[@class='inventory_item']//div[@class='pricebar']");
    private readonly By _shoppingCartLink = By.CssSelector(".shopping_cart_link");

    public bool IsInventoryOpened()
    {
        return _driver.FindElement(ProductsPage).Displayed;
    }

    public void AddToCart(string productName)
    {
        _driver.FindElement(AddToCartButton(productName)).Click();
    }

    public bool IsRemoveButtonDisplayed(string productName)
    {
       return _driver.FindElement(RemoveButton(productName)).Displayed;
    }

    public void GoToShoppingCart()
    {
        _driver.FindElement(_shoppingCartLink).Click();
    }

    public void SortContainerClick()
    {
        _driver.FindElement(SortContainer).Click();
    }
    
    public void SelectSortOptions(string sortType)
    {
        _driver.FindElement(SortOption(sortType)).Click();
    }

    public IEnumerable<double> GetPriceList()
    {
        var item = _driver.FindElements(ProductPrice);
        return item
            .Select(i => Regex.Replace(i.Text, "[^0-9.]", ""))
            .Select(priceText => double.Parse(priceText.Replace(".", ",")))
            .ToList();
    }
}