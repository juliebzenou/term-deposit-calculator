namespace CS_Basics {
    class Program {

        private enum InterestPaid {
            Monthly, 
            Quarterly, 
            Annually, 
            AtMaturity
        }

        private const double MIN_START_DEPOSIT = 1000;
        private const double MAX_START_DEPOSIT = 1500000;
        private const double MIN_INTEREST_RATE = 0;
        private const double MAX_INTEREST_RATE = 15;
        private const double MIN_INVESTMENT_TERM = 3;
        private const double MAX_INVESTMENT_TERM = 60;
        private const int MONTHS_IN_A_YEAR = 12;

        static void Main(string[] args) {
            Console.WriteLine("$"+ TermDepositCalculator(1500000, 7f, 3, InterestPaid.Quarterly));
        }

        /// <summary>   
        /// This method is a Term Deposit Calculator. It returns the final balance that would be in a term deposit 
        /// with the specified input.
        /// Assumptions: The acceptable range and edge cases for the input has been determined based on the behaviour 
        /// of the Bendigo Bank Deposit and Savings Calculator. See https://www.bendigobank.com.au/calculators/deposit-and-savings/
        /// </summary>
        /// <param name="startDepositAmount"> The amount the person deposits into their term deposit account. </param>
        /// <param name="interestRate"> The selected interest rate for the term deposit account. </param>
        /// <param name="investmentTerm"> How long the term deposit account is locked for in terms of months. </param>
        /// <param name="interestPaid"> How often interest is paid. This method assumes interest is re-invested 
        ///                             into the account. </param>
        /// <returns> Returns a double rounded to the nearest whole number of the final balance. </returns>
        static private double TermDepositCalculator(double startDepositAmount, double interestRate, int investmentTerm, InterestPaid interestPaid) {
            
            /*  First I wanted to deal with any edge cases, i.e. any invalid input
            *   The ranges are based on what the Bendigo Bank Calculator accepts
            */
            if (startDepositAmount < MIN_START_DEPOSIT || startDepositAmount > MAX_START_DEPOSIT) {
                Console.WriteLine("Your start deposit amount is outside the accepted range of $10 - $1,500,000.");
                Console.WriteLine("Please use a start deposit amount within the specified range.");
                return 0;
            }

            if (interestRate < MIN_INTEREST_RATE || interestRate > MAX_INTEREST_RATE) {
                Console.WriteLine("Your interest rate is outside the accepted range of 0% - 15%.");
                Console.WriteLine("Please use an interest rate within the specified range.");
                return 0;
            }

            if (investmentTerm < MIN_INVESTMENT_TERM || investmentTerm > MAX_INVESTMENT_TERM) {
                Console.WriteLine("Your investment term is outside the accepted range of 3 months - 5 years (60 months).");
                Console.WriteLine("Please use an investment term within the specified range.");
                return 0;
            }

            /*  I then wanted figure out the interest and how many times (iterations) this would be paid
            *   based on the interestPaid frequency chosen.
            *   E.g. if the frequency chosen is Quarterly, the interest needs to be paid four 
            *   times a year at a quarter of the per annum interest. 
            */
            double finalBalance = startDepositAmount;
            int iterations = 0;
            double interestRatePerIteration = 0;
            interestRate = interestRate/100f;
            switch (interestPaid) {
                case InterestPaid.Monthly: 
                    iterations = investmentTerm;
                    interestRatePerIteration = interestRate/MONTHS_IN_A_YEAR;
                    break;

                case InterestPaid.Quarterly:
                    iterations = investmentTerm/3;
                    interestRatePerIteration = interestRate/4;
                    break;

                case InterestPaid.Annually: 
                    if (investmentTerm < MONTHS_IN_A_YEAR) {
                        Console.WriteLine("You have selected interest to be paid annually, but your investment term is less than a year.");
                        Console.WriteLine("Please use an investment term that is at least 12 months or change the frequency your investment is paid at.");
                        return 0;
                    }

                    iterations = investmentTerm/MONTHS_IN_A_YEAR;
                    interestRatePerIteration = interestRate;
                    break;

                case InterestPaid.AtMaturity: 
                    iterations = 1;
                    interestRatePerIteration = interestRate*(investmentTerm/MONTHS_IN_A_YEAR); 
                    break;
            }

            /*  Once all the necessary values have been calculated. 
            *   I use a for loop to find the final balance, iteratively growing the balance 
            *   based on the previous balance, the interestPerIteration and the number of iterations needed.
            */
            for (int i=0; i<iterations; i++) {
                /* Rounded to two decimal places each time as these values were in line with the deposit table
                * on the Bendigo Bank Calculator.
                */
                finalBalance += Math.Round(finalBalance*interestRatePerIteration, 2); 
            }

            // Returned a whole number amount as the Bendigo Bank Calculator does.
            return Math.Round(finalBalance);
        }
    }
}

/* TESTS DONE

TESTING RANGES
Input: startDepositAmount, interestRate, investmentTerm, 
Output: finalBalance

Input: -100, 10f, 48, InterestPaid.Annually 
Output: "Your start deposit amount is outside the accepted range of $10 - $1,500,000."
"Please use a start deposit amount within the specified range."
$0

Input: 1000000000000, 10f, 48, InterestPaid.Annually 
Output: "Your start deposit amount is outside the accepted range of $10 - $1,500,000."
"Please use a start deposit amount within the specified range."
$0

Input: 1000, -10f, 48, InterestPaid.Annually 
Output: "Your interest rate is outside the accepted range of 0% - 15%."
"Please use an interest rate within the specified range."
$0

Input: 1000, 100f, 48, InterestPaid.Annually 
Output: "Your interest rate is outside the accepted range of 0% - 15%."
"Please use an interest rate within the specified range."
$0

Input: 1000, 10f, 2, InterestPaid.Annually 
Output: "Your investment term is outside the accepted range of 3 months - 5 years (60 months)."
"Please use an investment term within the specified range."
$0

Input: 1000, 10f, 100, InterestPaid.Annually 
Output: "Your investment term is outside the accepted range of 3 months - 5 years (60 months)."
"Please use an investment term within the specified range."
$0

TESTING INTEREST PAID FREQUENCY
Input: 1000, 10f, 48, InterestPaid.Monthly 
Output: $1489

Input: 1000, 10f, 48, InterestPaid.Quarterly 
Output: $1484 
* NOTE: This is a dollar off the Bendigo Bank result, as mentioned in my README, the rounding function needs 
to be finessed for the business requirements

Input: 1000, 10f, 48, InterestPaid.Annually 
Output: $1464

Input: 1000, 10f, 48, InterestPaid.AtMaturity 
Output: $1400

TESTING MIN/MAX START DEPOSIT
Input: 1000, 7f, 60, InterestPaid.Quarterly
Output: $1415

Input: 1500000, 7f, 3, InterestPaid.Quarterly
Output: $1526250 

TESTING MIN/MAX INTEREST
Input: 5000, 0f, 60, InterestPaid.Quarterly
Output: $5000

Input: 5000, 15f, 3, InterestPaid.Quarterly
Output: $5188
* NOTE: This is a dollar off the Bendigo Bank result, as mentioned in my README, the rounding function needs 
to be finessed for the business requirements

TESTING MIN/MAX INVESTMENT TERM
Input: 5000, 10f, 60, InterestPaid.Quarterly
Output: $8193

Input: 5000, 10f, 3, InterestPaid.Quarterly
Output: $5125

*/

