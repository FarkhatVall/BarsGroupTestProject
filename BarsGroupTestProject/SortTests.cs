using System.Diagnostics;
using BarsGroupTestProject.Pages;

namespace BarsGroupTestProject;

public class SortTests : UiTestBase
{
    private const string LowToHighSort = "low to high";
    private const string HighToLowSort = "high to low";
    
    [Test]
    public void LowToHighSortTesting()
    {
        var loginPage = new LoginPage(Driver);
        
        Driver.Navigate().GoToUrl(Url);
        Driver.Manage().Window.Maximize();

        Debug.Assert(PerformanceGlitchUser != null, nameof(PerformanceGlitchUser) + " != null");
        var inventoryPage = loginPage.Login(PerformanceGlitchUser.Login, PerformanceGlitchUser.Password);

        Assert.Multiple(() =>
        {
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo(null), "Ошибка входа: " + loginPage.GetErrorMessage());
            Assert.That(inventoryPage.IsInventoryOpened(), Is.True,
                "Ожидали открытие главной страницы приложения с товарами, однако страница не открылась");
        });
        
        inventoryPage.SortContainerClick();
        inventoryPage.SelectSortOptions(LowToHighSort);
        var priceList = inventoryPage.GetPriceList();
        Assert.That(priceList, Is.Ordered.Ascending);
    }
    
    [Test]
    public void HighToLowSortTesting()
    {
        var loginPage = new LoginPage(Driver);
        
        Driver.Navigate().GoToUrl(Url);
        Driver.Manage().Window.Maximize();

        Debug.Assert(PerformanceGlitchUser != null, nameof(PerformanceGlitchUser) + " != null");
        var inventoryPage = loginPage.Login(PerformanceGlitchUser.Login, PerformanceGlitchUser.Password);

        Assert.Multiple(() =>
        {
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo(null), "Ошибка входа: " + loginPage.GetErrorMessage());
            Assert.That(inventoryPage.IsInventoryOpened(), Is.True,
                "Ожидали открытие главной страницы приложения с товарами, однако страница не открылась");
        });
        
        inventoryPage.SortContainerClick();
        inventoryPage.SelectSortOptions(HighToLowSort);
        var priceList = inventoryPage.GetPriceList();
        Assert.That(priceList, Is.Ordered.Descending);
    }
}