namespace Parser.Core
{
    public abstract class IParser
    {
        private bool _init = false;

        private ParserStatus _status = ParserStatus.Uninit;

        public byte[] OriginalData { get; private set; }

        public bool IsPEFile { get; private set; }

        private void SetInit()
        {
            _init = true;
            IsPEFile = OriginalData[0] == 0x4D && OriginalData[1] == 0x5A;
            if (!IsPEFile)
                _status = ParserStatus.Failed;
            else
                _status = ParserStatus.Init;
        }

        protected IParser(byte[] data)
        {
            OriginalData = data;
            SetInit();
        }

        protected IParser(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                ms.Position = 0;
                OriginalData = ms.ToArray();
            }
            SetInit();

        }

        protected IParser(string filename) : this(File.ReadAllBytes(filename))
        {
        }

        public enum ParserStatus
        {
            None,
            Uninit,
            Init,
            Success,
            Failed
        }
    }
}