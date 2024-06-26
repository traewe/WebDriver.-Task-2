﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PastebinTest
{
    [TestFixture]
    public class PastebinTest
    {
        private IWebDriver driver;
        private PastebinPage pastebinPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            pastebinPage = new PastebinPage(driver);
        }

        [Test]
        public void CreateNewPaste_ParametersAreValid_ReturnsCorrectData()
        {
            pastebinPage.Open();

            pastebinPage.CreateNewPaste([ "git config --global user.name  \"New Sheriff in Town\"", 
                "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")", 
                "git push origin master --force" ],
                "how to gain dominance among developers");

            bool generalTestingResult = pastebinPage.IsPasteTextCreatedSuccessfully()
                && pastebinPage.IsPasteNameCreatedSuccessfully()
                && pastebinPage.IsSyntaxHighlightingCreatedSuccessfully();

            Assert.That(generalTestingResult, Is.EqualTo(true));
        }

        [Test]
        public void CreateNewPaste_PasteTextParameterIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => pastebinPage.CreateNewPaste(null, "string"));
        }

        [Test]
        public void CreateNewPaste_TitleParameterIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => pastebinPage.CreateNewPaste([" "], null));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
