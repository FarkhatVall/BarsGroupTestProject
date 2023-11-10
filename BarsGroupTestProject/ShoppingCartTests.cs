using System.Diagnostics;
using BarsGroupTestProject.Pages;
namespace BarsGroupTestProject;

[TestFixture]
public class ShoppingCartTests : UiTestBase
{
    private const string ProductJacketName = "Sauce Labs Fleece Jacket";
    private const string ProductTShirtName = "Sauce Labs Bolt T-Shirt";

    [Test]
    public void AddProductsInShoppingCartTesting()
    {
        StepNotificationInConsole("Шаг 1:  Авторизоваться в приложении под указанной учетной записью");
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

        StepNotificationInConsole($"Шаг 2: Добавить товар {ProductJacketName} в корзину  (нажать кнопку ADD TO CART)");
        
        inventoryPage.AddToCart(ProductJacketName);

        Assert.That(inventoryPage.IsRemoveButtonDisplayed(ProductJacketName), Is.True, 
            $"Ожидали, что у товра {ProductJacketName} кнопка ADD TO CART изменяется на REMOVE, однако кнопка REMOVE не найдена");

        StepNotificationInConsole("Шаг 3: Перейти в корзину нажатием кнопки сверху в правом углу в виде корзины");
        
        var shoppingCartPage = inventoryPage.GoToShoppingCart();
        
        Assert.Multiple(() =>
        {
            Assert.That(shoppingCartPage.IsShoppingCartPageOpened(), Is.True, 
                "Ожидали открытие окна корзины, однако страница не открылась");
            Assert.That(shoppingCartPage.IsProductInCart(ProductJacketName), Is.True, 
                $"Ожидали в окне корзины наличие добавленного товара {ProductJacketName}, однако {ProductJacketName} не найдена");
        });

        StepNotificationInConsole("Шаг 4: Нажать кнопку CONTINUE SHOPPING");
        shoppingCartPage.ContinueShopping();

        Assert.That(inventoryPage.IsInventoryOpened(), Is.True, 
            "Ожидали открытие главной страницы с товарами, однако страница не открылась");

        StepNotificationInConsole($"Шаг 5: Добавить товара {ProductTShirtName} в корзину (нажать кнопку ADD TO CART) ");
        inventoryPage.AddToCart(ProductTShirtName);

        Assert.That(inventoryPage.IsRemoveButtonDisplayed(ProductTShirtName), Is.True, 
            $"Ожидали, что у товра {ProductTShirtName} кнопка ADD TO CART изменяется на REMOVE, однако кнопка REMOVE не найдена");

        StepNotificationInConsole("Шаг 6: Перейти в корзину нажатием кнопки сверху в правом углу в виде корзины");
        var shoppingCartPageNew = inventoryPage.GoToShoppingCart();
        Assert.Multiple(() =>
        {
            Assert.That(shoppingCartPageNew.IsShoppingCartPageOpened(), Is.True, 
                "Ожидали открытие окна корзины, однако страница не открылась");
            Assert.That(shoppingCartPageNew.IsProductInCart(ProductJacketName), Is.True, 
                $"Ожидали в окне корзины наличие добавленного товара {ProductJacketName}, однако {ProductJacketName} не найдена");
            Assert.That(shoppingCartPageNew.IsProductInCart(ProductTShirtName), Is.True, 
                $"Ожидали в окне корзины наличие добавленного товара {ProductTShirtName}, однако {ProductTShirtName} не найдена");
        });
    }
}