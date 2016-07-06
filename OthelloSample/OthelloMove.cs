using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloSample
{
    /// <summary>
    /// Struct of an ordered pair consisting of both row and column values
    /// </summary>
    /// <author>Joseph Tamberino</author>
    /// <date>7/6/2016</date
    class OthelloMove
    {
        public int row { get; private set; }
        public int col { get; private set; }
        public OthelloMove(int row, Column col)
        {
            this.row = row;
            this.col = (int)col;
        }

        /// <summary>
        /// Method to determine if one OthelloMove is equal to another
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            try {
                OthelloMove compare = (OthelloMove)obj;
                return (this.row == compare.row && this.col == compare.col);
            }
            catch
            {
                return false; //returns false if the provided object isn't an OthelloMove
            }
            
        }
        /// <summary>
        /// Returns a string representation of the move.
        /// </summary>
        /// <returns>String in the format of (row number, column number).</returns>
        public override string ToString()
        {
            return "(" + (Column)col +""+ row + ")";
        }
    }
}


    