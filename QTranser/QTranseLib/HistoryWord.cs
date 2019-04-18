namespace QTranser
{
    public class HistoryWord
    {
        private string _word;
        public string Word
        {
            get { return _word; }
            set
            {
                _word = value;
            }
        }
        private string _translate;
        public string Translate
        {
            get { return _translate; }
            set
            {
                _translate = value;
            }
        }
    }
}
