using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace PastebinTest
{
    public class PastebinPage
    {
        private IWebDriver driver;

        public PastebinPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Open()
        {
            driver.Navigate().GoToUrl("https://pastebin.com");
        }

        public void CreateNewPaste(string[] pasteText, string title)
        {
            if (pasteText == null)
            {
                throw new ArgumentNullException(nameof(pasteText));
            }
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var newPasteElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postform-text")));

            for (int i = 0; i < pasteText.Length; i++)
            {
                newPasteElement.SendKeys(pasteText[i]);

                if (i < pasteText.Length - 1)
                {
                    newPasteElement.SendKeys("\n");
                }
            }

            var syntaxHighlightingContainer = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("select2-postform-format-container")));
            syntaxHighlightingContainer.Click();

            var bashSyntaxHighlightOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[text()='Bash']")));
            bashSyntaxHighlightOption.Click();

            var expirationDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("select2-postform-expiration-container")));
            expirationDropdown.Click();

            var tenMinutesExpirationOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[text()='10 Minutes']")));
            tenMinutesExpirationOption.Click();

            var titleInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postform-name")));
            titleInput.SendKeys(title);

            var createButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn.-big[type='submit']")));
            createButton.Click();
        }

        public bool IsPasteNameCreatedSuccessfully()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string expectedPasteNameWebElement = "how to gain dominance among developers";

            var actualPasteName = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("info-top")));

            return actualPasteName.Text == expectedPasteNameWebElement;
        }

        public bool IsSyntaxHighlightingCreatedSuccessfully()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string expectedSyntaxHighlighting = "Bash";

            var actualSyntaxHighlightingWebElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href='/archive/bash'].btn.-small.h_800")));

            return actualSyntaxHighlightingWebElement.Text == expectedSyntaxHighlighting;
        }

        public bool IsPasteTextCreatedSuccessfully()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] expectedPasteText = ["git config --global user.name  \"New Sheriff in Town\"",
                "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")",
                "git push origin master --force"];
            
            var actualPasteTextWebElement = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("de1")));

            if (actualPasteTextWebElement.Count != expectedPasteText.Length)
            {
                return false;
            }

            for (int i = 0; i < expectedPasteText.Length; i++)
            {
                expectedPasteText[i] = Regex.Replace(expectedPasteText[i], @"\s+", " ");
                string tempActualLine = Regex.Replace(actualPasteTextWebElement[i].Text, @"\s+", " ");

                if (!expectedPasteText[i].Equals(tempActualLine))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
