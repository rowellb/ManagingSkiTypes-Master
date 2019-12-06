using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingSkiTypes
{
    class Skis
    {
        #region FEILDS

        private string _brand;
        private int _length;

        #endregion

        #region PROPERTIES

        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Skis()
        {

        }

        #endregion
    }
}
