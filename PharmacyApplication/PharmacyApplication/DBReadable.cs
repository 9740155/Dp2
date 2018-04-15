﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    //Authors: Jed
    public abstract class DBReadable
    {
        //Author: Jed
        //Values in the array have been vetted and fit the mold defined by FieldTypesToRead
        protected object[] _elements;

        //Author: Jed
        //Defines the types to be read by from and array of objects
        protected abstract Type[] FieldTypesToRead
        {
            get;
        }

        //Author: Jed
        public DBReadable()
        {
            _elements = new object[FieldTypesToRead.Length];

            int i = 0;
            while(i < _elements.Length)
            {
                //NOTE from Jed, not entierly sure how this line works but it seems to. It creates a default value for the elements by just knowing the type.
                _elements[i] = FieldTypesToRead[i].IsValueType ? Activator.CreateInstance(FieldTypesToRead[i]) : null;

                i += 1;
            }
        }

        //Author: Jed        
        /// <summary>
        /// Reads a generic object array, vetts it to make sure the types of the generic object match the type expected atthat index
        /// </summary>
        /// <param name="elements">The aray of elements to be interpreted</param>
        public void ReadFromGeneric(object[] elements)
        {
            if ((elements != null) && (FieldTypesToRead != null))
            {
                //Check the elements array is the same length as the number of expected elements
                if (!(elements.Length == this.FieldTypesToRead.Length))
                {
                    bool error = false;

                    int i = 0;
                    while(i < elements.Length)
                    {
                        if(elements[i].GetType() != FieldTypesToRead[i])
                        {
                            error = true;
                            break;
                        }

                        i += 1;
                    }

                    if(!error)
                    {
                        _elements = new object[elements.Length];

                        i = 0;
                        while(i < elements.Length)
                        {
                            _elements[i] = elements[i];

                            i += 1;
                        }
                    }
                }
            }

            else
            {
                string msg = "";

                if ((elements == null))
                {
                    msg += "The Elements array was null during a call to Read Generic\n";
                }

                if ((FieldTypesToRead == null))
                {
                    msg += "The FieldTypeToRead array was null during a call to Read Generic\n";
                }

                throw new PAReadException(msg);
            }
        }
    }
}
