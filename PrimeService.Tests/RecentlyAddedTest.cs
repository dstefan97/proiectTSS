using System;
using System.Collections.Generic;
using Prime.Services;
using System.Linq;
using NUnit.Framework;

namespace Prime.UnitTests.Services
{
    public class RecentlyAddedTest
    {
        #region Private Methods
        private RecentlyAdded _recentlyAddedList;
        private int _size;
        #endregion

        #region Setup/TearDown
        [SetUp]
        public void Setup()
        {
            _recentlyAddedList = new RecentlyAdded();
            _size = 10;
        }
        [TearDown]
        public void TearDown()
        {
            _recentlyAddedList = null;
            _size = -1;
        }


        #endregion

        #region Test Methods

        [Test]
        public void CanAddItems()
        {
            _recentlyAddedList.Add("1");
            var listCount = _recentlyAddedList.Count();
            Assert.That(listCount, Is.GreaterThan(0), string.Format("Numarul elementelor ar trebui sa fie mai mare decat {0} dar este {1}", 0, listCount));

        }
        [Test]
        public void CanAddUniqueItems()
        {
            _recentlyAddedList.Add("0");
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("2");
            _recentlyAddedList.Add("3");
            _recentlyAddedList.Add("4");

            var expectedlist = ToList("4", "3", "2", "1", "0");
            var actuallist = _recentlyAddedList.ToList();

            Assert.That(actuallist, Is.EqualTo(expectedlist));

        }
        [Test]
        public void CanAddItemsInLifoOrder()
        {
            _recentlyAddedList.Add("0");
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("2");
            _recentlyAddedList.Add("3");
            _recentlyAddedList.Add("4");
            var expectedlist = ToList("4", "3", "2", "1", "0");
            var actuallist = _recentlyAddedList.ToList();

            Assert.That(actuallist, Is.EqualTo(expectedlist));

        }
        [Test]
        public void CanAvoidInsertionOfItemsAreBeyondListSize()
        {
            _recentlyAddedList.Add("0");
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("2");
            _recentlyAddedList.Add("3");
            _recentlyAddedList.Add("4");
            _recentlyAddedList.Add("5"); //This should not be considered
            _recentlyAddedList.Add("6"); //This should not be considered

            var expectedlist = ToList("4", "3", "2", "1", "0");
            var actuallist = _recentlyAddedList.ToList();

            Assert.That(actuallist, Is.EqualTo(expectedlist));

        }

        [Test]
        public void CanTestItemByIndex()
        {
            _recentlyAddedList.Add("0");
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("2");
            _recentlyAddedList.Add("3");
            _recentlyAddedList.Add("4");
            const string expectedlistitem = "1";
            var actuallistitem = _recentlyAddedList.GetListItem(3);

            Assert.That(actuallistitem, Is.EqualTo(expectedlistitem));
        }

        [Test]
        public void CanTestDefaultListSize()
        {
            const int expectedlistSize = 5;
            var actuallistsize = _recentlyAddedList.Size;
            Assert.That(actuallistsize, Is.EqualTo(expectedlistSize));
        }

        [Test]
        public void CanThrowArgumentExceptionWhenSuppliedIndexIsOutOfScope()
        {
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("2");
            _recentlyAddedList.Add("3");
            _recentlyAddedList.Add("4");
            _recentlyAddedList.Add("5");
            const int index = 5;
            var exception = Assert.Throws<ArgumentException>(() => _recentlyAddedList.GetListItem(index));

            Assert.That(exception.Message, Is.EqualTo(string.Format("Indexul [{0}] nu trebuie sa fie mai mare decat [{1}].", index, _recentlyAddedList.Count - 1)));
        }

        [Test]
        public void CanThrowArgumentExceptionWhenSuppliedIndexContainNegativeValue()
        {
            _recentlyAddedList.Add("0");
            _recentlyAddedList.Add("1");
            _recentlyAddedList.Add("2");
            _recentlyAddedList.Add("3");
            _recentlyAddedList.Add("4");
            const int index = -1;
            var exception = Assert.Throws<ArgumentException>(() => _recentlyAddedList.GetListItem(index));
            Assert.That(exception.Message, Is.EqualTo(string.Format("Indexul [{0}] nu ar trebui sa fie negativ sau mai mare decat [{1}].", index, _recentlyAddedList.Count - 1)));

        }

        [Test]
        public void CanThrowArgumentExceptionWhenSuppliedItemIsNullorEmpty()
        {
            var list = ToList(null, string.Empty);

            foreach (var item in list)
            {
                var item1 = item;
                var exception = Assert.Throws<ArgumentException>(() => _recentlyAddedList.Add(item1));
                Assert.That(exception.Message,
                        Is.EqualTo(string.Format("Membrii listei nu ar trebui sa fie Empty sau Null, dar a fost [{0}]", item1)));
            }

        }
        [Test]
        public void CanDefineListSize()
        {
            var sizeableList = new RecentlyAdded (_size);
            Assert.That(_size, Is.EqualTo(sizeableList.Size));
        }
        #endregion

        #region Private Methods
        private List<string> ToList(params string[] items) => items.ToList();

        #endregion

    }
}
