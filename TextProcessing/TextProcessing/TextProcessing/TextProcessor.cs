using System;
using System.IO;

namespace TextProcessing
{
    public sealed class TextProcessor
    {
        public readonly InputData InputData;
        private const int DefaultBufSize = 1024;
        private readonly int _bufSize;
        private char[] _buf;

        private int _letterCounter;

        public TextProcessor(InputData inputData)
        {
            InputData = inputData;
            _bufSize = InputData.WordThreshold > DefaultBufSize ? InputData.WordThreshold : DefaultBufSize;
            _buf = new char[_bufSize];
        }

        /*The file is read into the buffer where the text is processed.
        Truncated words at the border of the buffer are moved at the beginning to continue*/
        public void Run()
        {
            using (var reader = new StreamReader(InputData.InputFileName))
            using (var writer = new StreamWriter(InputData.OutputFileName, false))
            {
                while (reader.ReadBlock(_buf, _letterCounter, _bufSize - _letterCounter) != 0)
                {
                    Process();
                    writer.Write(_buf, 0, _bufSize - _letterCounter);
                    var newBuf = new char[_bufSize];
                    Array.Copy(_buf, _bufSize - _letterCounter, newBuf, 0, _letterCounter);
                    _buf = newBuf;
                }
            }
        }

        private void Process()
        {
            for (var i = _letterCounter; i < _bufSize; i++)
            {
                if (char.IsLetter(_buf[i]))
                {
                    _letterCounter++;
                }
                else
                {
                    if (_letterCounter != 0 && _letterCounter < InputData.WordThreshold)
                    {
                        for (var j = 0; j < _letterCounter; j++)
                        {
                            _buf[i - _letterCounter + j] = '\0';
                        }
                    }
                    _letterCounter = 0;
                    if (InputData.DeletePunctuation && char.IsPunctuation(_buf[i]))
                    {
                        _buf[i] = '\0';
                    }
                }
            }
        }
    }
}
