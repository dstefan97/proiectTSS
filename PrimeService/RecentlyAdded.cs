using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Prime.Services
{
    public class RecentlyAdded : IEnumerable<string>
    {
        #region Private members

        private readonly List<string> _listofuniquestrings;
        private int _listSize = -1;
        private const int DefaultListSize = 5;

        #endregion

        #region Class Initializers

        public RecentlyAdded()
        {
            _listofuniquestrings = new List<string>();
            SetDefaultListSize();
        }
        public RecentlyAdded(int listSize)
        {
            _listofuniquestrings = new List<string>();
            _listSize = listSize;
        }

        public RecentlyAdded(IEnumerable<string> listItems)
        {
            _listofuniquestrings = listItems.ToList();
            SetDefaultListSize();
        }

        public RecentlyAdded(int listSize, IEnumerable<string> listItems)
        {
            _listofuniquestrings = listItems.ToList();
            _listSize = listSize;

            TrimListToTheSizeDefined();
        }
        #endregion

        #region Public methods
        public void Add(string listitem)
        {
            if (string.IsNullOrEmpty(listitem))
                throw new ArgumentException(string.Format("Membrii listei nu ar trebui sa fie Empty sau Null, dar a fost [{0}]",
                                                          listitem));

            AvoidDuplicateInsertion(listitem);

            _listofuniquestrings.Insert(0, listitem);

            TrimListToTheSizeDefined();

        }

        public string GetListItem(int index)
        {
            CheckForValidIndex(index);

            return _listofuniquestrings != null ? _listofuniquestrings[index] : string.Empty;
        }


        #endregion

        #region Helper Methods /members

        public int Count => _listofuniquestrings != null
            ? _listofuniquestrings.Count
            : 0;

        public int Size => _listSize;

        public List<string> ToList() => _listofuniquestrings;

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<string> GetEnumerator() => _listofuniquestrings.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Private members
        private void SetDefaultListSize()
        {

            _listSize = _listSize < 0
                            ? DefaultListSize
                            : _listSize;

        }
        private void CheckForValidIndex(int index)
        {
            if (index < 0)
                throw new ArgumentException(string.Format("Indexul [{0}] nu ar trebui sa fie negativ sau mai mare decat [{1}].", index, _listofuniquestrings.Count - 1));

            if (index > _listofuniquestrings.Count - 1)
                throw new ArgumentException(string.Format("Indexul [{0}] nu trebuie sa fie mai mare decat [{1}].", index, _listofuniquestrings.Count - 1));
        }

        private void AvoidDuplicateInsertion(string listitem)
        {
            var indexOccurenceofItem = _listofuniquestrings.IndexOf(listitem);

            if (indexOccurenceofItem > -1)
                _listofuniquestrings.RemoveAt(indexOccurenceofItem);
        }

        private void TrimListToTheSizeDefined()
        {
            if (_listSize != -1)
                while (_listofuniquestrings.Count > _listSize)
                    _listofuniquestrings.RemoveAt(0); //Remove from Top in LIFO
        }

        #endregion

    }
}
