namespace BarsGroupTestProject.Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    private readonly By _usernameField = By.Id("user-name");
    private readonly By _passwordField = By.Id("password");
    private readonly By _loginButton = By.Id("login-button");
    private readonly By _errorMessage = By.CssSelector("[data-test='error']");

    private void EnterUsername(string? username)
    {
        _driver.FindElement(_usernameField).SendKeys(username);
    }

    private void EnterPassword(string? password)
    {
        _driver.FindElement(_passwordField).SendKeys(password);
    }

    private void ClickLogin()
    {
        _driver.FindElement(_loginButton).Click();
    }

    public string? GetErrorMessage()
    {
        try
        {
            return _driver.FindElement(_errorMessage).Text;
        }
        catch (NoSuchElementException)
        {
            return null;
        }
    }

    public void Login(string? username, string? password)
    {
        EnterUsername(username);
        EnterPassword(password);
        ClickLogin();
    }
}