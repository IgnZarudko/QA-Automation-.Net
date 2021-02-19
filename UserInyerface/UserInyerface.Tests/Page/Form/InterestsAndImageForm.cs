using System.Collections.Generic;
using Aquality.Selenium.Elements;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace UserInyerface.Tests.Page.Form
{
    public class InterestsAndImageForm : Aquality.Selenium.Forms.Form
    {
        private readonly By _imageUploadButtonLocator = By.XPath("//a[@class='avatar-and-interests__upload-button']");
        public Label ImageUploadButton =>
            FormElement.FindChildElement<Label>(_imageUploadButtonLocator, "Image upload button");
        
        
        private readonly By _uploadedImageLocator = By.XPath("//div[@class='avatar-and-interests__avatar-image']");
        public Label UploadedImage => FormElement.FindChildElement<Label>(_uploadedImageLocator, "Uploaded image");
        
        
        private readonly By _interestCheckboxesLocator = By.XPath("//label[contains(@for, 'interest') and not(contains(@for,'selectall'))]");
        public IList<Button> InterestCheckboxes =>
            FormElement.FindChildElements<Button>(_interestCheckboxesLocator, "Interest checkbox");
        
        
        private readonly By _unselectAllCheckboxLocator = By.XPath("//label[@for='interest_unselectall']");
        public Button UnselectAllCheckbox =>
            FormElement.FindChildElement<Button>(_unselectAllCheckboxLocator, "Unselect all checkbox");
        
        
        private readonly By _nextStepButtonLocator = By.XPath("//button[contains(@class,'button--stroked')]");
        public Button NextStepButton =>
            FormElement.FindChildElement<Button>(_nextStepButtonLocator, "Confirm interests and image button");


        public InterestsAndImageForm() : base(By.XPath("//div[@class='avatar-and-interests-page']"), "Interests and image form")
        {
            
        }

        public void ChooseInterests(int amount)
        {
            Randomizer randomizer = new Randomizer();
            int size = InterestCheckboxes.Count;
            HashSet<int> checkedIndexes = new HashSet<int>();
            for (int i = 0; i < 3;)
            {
                int nextIndex = randomizer.Next() % size;
                if (!checkedIndexes.Contains(nextIndex))
                {
                    InterestCheckboxes[nextIndex].Click();
                    checkedIndexes.Add(nextIndex);
                    i++;
                } 
            }
        }
    }
}