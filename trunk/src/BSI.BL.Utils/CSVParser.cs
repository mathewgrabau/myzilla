using System;
using System.Collections;
using System.Text;

namespace MyZilla.BL.Utils
{
    public class CsvParser
    {
        public  static ArrayList Parse(string inputValue, char separator)
        {
            int intCounter = 0, intLenght;
            StringBuilder strElem = new StringBuilder();
            ArrayList alParsedCsv = new ArrayList();
            intLenght = inputValue.Length;
            strElem = strElem.Append(String.Empty);
            int intCurrState = 0;
            int[][] aActionDecider = new int[9][];

            //Build the state array
            aActionDecider[0] = new int[4] { 2, 0, 1, 5 };
            aActionDecider[1] = new int[4] { 6, 0, 1, 5 };
            aActionDecider[2] = new int[4] { 4, 3, 3, 6 };
            aActionDecider[3] = new int[4] { 4, 3, 3, 6 };
            aActionDecider[4] = new int[4] { 2, 8, 6, 7 };
            aActionDecider[5] = new int[4] { 5, 5, 5, 5 };
            aActionDecider[6] = new int[4] { 6, 6, 6, 6 };
            aActionDecider[7] = new int[4] { 5, 5, 5, 5 };
            aActionDecider[8] = new int[4] { 0, 0, 0, 0 };

            for (intCounter = 0; intCounter < intLenght; intCounter++)
            {
                intCurrState = aActionDecider[intCurrState][GetInputId(inputValue[intCounter], separator)];
                //take the necessary action depending upon the state returned
                PerformAction(ref intCurrState, inputValue[intCounter], ref strElem, ref alParsedCsv);
            }
            intCurrState = aActionDecider[intCurrState][3];
            PerformAction(ref intCurrState, '\0', ref strElem, ref alParsedCsv);
            return alParsedCsv;
        }

        private static int GetInputId(char inputValue, char separator)
        {
            if (inputValue == '"')
            {
                return 0;
            }
            else if (inputValue == separator)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private static void PerformAction(ref int currentState, char inputValue, ref StringBuilder sbElem, ref ArrayList outputCsv)
        {
            string strTemp = null;
            switch (currentState)
            {
                case 0:
                    //Seperate out value to array list
                    strTemp = sbElem.ToString();
                    outputCsv.Add(strTemp);
                    sbElem = new StringBuilder();
                    break;
                case 1:
                case 3:
                case 4:
                    //accumulate the character
                    sbElem.Append(inputValue);
                    break;
                case 5:
                    //End of line reached. Seperate out value to array list
                    strTemp = sbElem.ToString();
                    outputCsv.Add(strTemp);
                    break;
                case 6:
                    //Erroneous input. Reject line.
                    outputCsv.Clear();
                    break;
                case 7:
                    //wipe ending " and Seperate out value to array list
                    sbElem.Remove(sbElem.Length - 1, 1);
                    strTemp = sbElem.ToString();
                    outputCsv.Add(strTemp);
                    sbElem = new StringBuilder();
                    currentState = 5;
                    break;
                case 8:
                    //wipe ending " and Seperate out value to array list
                    sbElem.Remove(sbElem.Length - 1, 1);
                    strTemp = sbElem.ToString();
                    outputCsv.Add(strTemp);
                    sbElem = new StringBuilder();
                    //goto state 0
                    currentState = 0;
                    break;
            }
        }

    }
}
