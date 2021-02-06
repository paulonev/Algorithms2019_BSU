using System;
using System.Collections;

namespace src.instances
{
    public class DataBlockEnumerator : IEnumerator
    {
        DataBlock _block;
        DataBlockNode _current;
        bool _isLast;

        public object Current
        {
            get => _current;
        }
        public DataBlockEnumerator(DataBlock block)
        {
            _block = block;
            _current = null;
            _isLast = false;
        }

        /// <summary></summary>
        /// <returns>
        /// false - if reach end of collection
        /// true - otherwise
        /// </returns>
        public bool MoveNext()
        {
            if(_current == null)
            {    
                if(_isLast) // indicates position of _current when iter thru block
                    return false;
                else
                {
                    _isLast = true;
                    _current = _block.Head;
                }
            }
            else
            {
                _current = _current._next;
            }
            return (_current != null);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        
    }
}